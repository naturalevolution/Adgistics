using System;
using System.Collections.Concurrent;
using System.Linq;
using AdgisticsMotors.Web.Business.Threading.Models;
using AdgisticsMotorsReport;
using AdgisticsMotorsReport.Utils.Threading;

namespace AdgisticsMotors.Web.Business.Threading
{
    /// <summary>
    ///     Work used to generate a report
    /// </summary>
    public class ProcessReportWork : IWork
    {
        public ProcessReportWork(ProcessReportRequestModel requestModel, ConcurrentBag<DealershipData> clientDatas)
        {
            ProcessReportRequestModel = requestModel;
            ClientDatas = clientDatas;
        }

        private ProcessReportRequestModel ProcessReportRequestModel { get; set; }
        private ConcurrentBag<DealershipData> ClientDatas { get; set; }

        public void Process()
        {
            CheckParams();
            while (ProcessReportRequestModel.WorkerQueue.Status().Processing.Count() != 0)
            {
                var response = new ProcessReportResponseModel
                {
                    Name = ProcessReportRequestModel.Name,
                    Id = ProcessReportRequestModel.Id
                };
                var query = ClientDatas.Where(ProcessReportRequestModel.WherePredicate).OrderBy(ProcessReportRequestModel.OrderByPredicate);

                response.Datas = ProcessReportRequestModel.Size != 0
                    ? query.Take(ProcessReportRequestModel.Size).ToList()
                    : query.ToList();

                ProcessReportRequestModel.HubCaller.Caller.onAddReport(response);
                break;
            }
        }

        private void CheckParams()
        {
            if (ClientDatas == null)
                throw new ArgumentException("Argument: 'ClientDatas' must not be null.");
            if (ProcessReportRequestModel == null)
                throw new ArgumentException("Argument: 'ProcessReportRequestModel' must not be null.");
            if (ProcessReportRequestModel.HubCaller == null)
                throw new ArgumentException("Argument: 'HubCaller' must not be null.");
            if (ProcessReportRequestModel.WorkerQueue == null)
                throw new ArgumentException("Argument: 'HubCaller' must not be null.");
            if (ProcessReportRequestModel.WherePredicate == null)
                throw new ArgumentException("Argument: 'HubCaller' must not be null.");
            if (ProcessReportRequestModel.OrderByPredicate == null)
                throw new ArgumentException("Argument: 'HubCaller' must not be null.");
        }
    }
}