using System;
using System.Collections.Concurrent;
using System.Web;
using AdgisticsMotors.Web.Business.Threading.Models;
using AdgisticsMotors.Web.Models;
using AdgisticsMotorsReport;
using AdgisticsMotorsReport.Utils.Threading;

namespace AdgisticsMotors.Web.Business.Threading
{
    /// <summary>
    /// Work used to retrieve datas from the service and push them to the client
    /// </summary>
    public class ProcessDataWork : IWork
    {
        public ProcessDataWork(ProcessDataRequestModel information, ConcurrentBag<DealershipData> clientDatas)
        {
            ClientDatas = clientDatas;
            ProcessDataRequestModel = information;
        }

        private ProcessDataRequestModel ProcessDataRequestModel { get; set; }
        private ConcurrentBag<DealershipData> ClientDatas { get; set; }

        public void Process()
        {
            CheckParams();
            var dealershipMessage = new DealershipMessage
            {
                Position = ProcessDataRequestModel.GetProgressBarPosition()
            };
            var identifier = ProcessDataRequestModel.GetIdentifier();
            var endPoint = ProcessDataRequestModel.GetUri();
            try
            {
                var data = ServiceFactory.Dealership.GetDealershipData(identifier, endPoint);
                if (ClientDatas != null)
                {
                    ClientDatas.Add(data);
                }
                dealershipMessage.AddSuccess(data);
            }
            catch (HttpException httpException)
            {
                dealershipMessage.AddError(identifier, httpException.Message);
            }
            catch (ArgumentException argumentException)
            {
                dealershipMessage.AddError(identifier, argumentException.Message);
            }
            ProcessDataRequestModel.HubCaller.Caller.onAddDealershipMessage(dealershipMessage);
        }

        private void CheckParams()
        {
            if (ProcessDataRequestModel == null)
                throw new ArgumentException("Argument: 'ProcessDataRequestModel' must not be null.");
            if (ProcessDataRequestModel.HubCaller == null)
                throw new ArgumentException("Argument: 'HubCaller' must not be null.");
            if (ClientDatas == null)
                throw new ArgumentException("Argument: 'ClientDatas' must not be null.");
        }
    }
}