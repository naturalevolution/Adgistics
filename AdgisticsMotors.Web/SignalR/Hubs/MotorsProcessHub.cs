using System.Collections.Concurrent;
using AdgisticsMotors.Common.Resources;
using AdgisticsMotors.Web.Business;
using AdgisticsMotors.Web.Business.Threading;
using AdgisticsMotors.Web.Business.Threading.Models;
using AdgisticsMotorsReport;
using AdgisticsMotorsReport.Utils.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdgisticsMotors.Web.SignalR.Hubs
{
    [HubName("motorsProcess")]
    public class MotorsProcessHub : Hub
    {
        private static readonly ConcurrentDictionary<string, ConcurrentBag<DealershipData>> DealershipDatas =
            new ConcurrentDictionary<string, ConcurrentBag<DealershipData>>();

        private ConcurrentBag<DealershipData> RetrieveDealershipDatas()
        {
            ConcurrentBag<DealershipData> clientDatas;
            if (!DealershipDatas.TryGetValue(Context.ConnectionId, out clientDatas))
            {
                clientDatas = new ConcurrentBag<DealershipData>();
                DealershipDatas.AddOrUpdate(Context.ConnectionId, clientDatas, (key, oldValue) => clientDatas);
            }
            return clientDatas;
        }

        public void StartProcess()
        {
            //Retrieve existing datas from client
            var clientDatas = RetrieveDealershipDatas();

            //Retrieve datas from files
            var files = ServiceFactory.File.RetrieveLinesFromFile();

            var worker = new BackgroundWorkerQueue(10);
            for (var i = 0; i < files.Length; i++)
            {
                var model = new ProcessDataRequestModel(Clients)
                {
                    Size = files.Length,
                    Position = i,
                    Line = files[i]
                };
                var work = new ProcessDataWork(model, clientDatas);
                worker.Enqueue(work);
            }

            //Generate report Top performing
            var topPerformingModel = new ProcessReportRequestModel(Clients, FrontMessage.ReportIdTopPerforming,
                FrontMessage.ReportTitleTopPerforming, worker)
            {
                WherePredicate = x => x.TotalSales > 0,
                OrderByPredicate = x => x.TotalSales, 
                Size = 100
            };
            worker.Enqueue(new ProcessReportWork(topPerformingModel, clientDatas));

            //Generate report Low stock
            var lowStockModel = new ProcessReportRequestModel(Clients, FrontMessage.ReportIdLowStock,
                FrontMessage.ReportTitleLowStock, worker)
            {
                WherePredicate = x => x.AvailableStock < 10,
                OrderByPredicate = x => x.AvailableStock
            };
            worker.Enqueue(new ProcessReportWork(lowStockModel, clientDatas));
        }

        public void Initialize()
        {
            Clients.Caller.onInitialize(FrontMessage.Welcome);
        }
    }
}