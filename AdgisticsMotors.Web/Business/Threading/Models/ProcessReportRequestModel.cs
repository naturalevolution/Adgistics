using System;
using AdgisticsMotorsReport;
using AdgisticsMotorsReport.Utils.Threading;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdgisticsMotors.Web.Business.Threading.Models
{
    public class ProcessReportRequestModel : BaseInformation
    { 
        public ProcessReportRequestModel(IHubCallerConnectionContext<dynamic> clients, string id, string name, BackgroundWorkerQueue worker)
            : base(clients)
        {
            Id = id;
            Name = name;
            WorkerQueue = worker; 
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public BackgroundWorkerQueue WorkerQueue { get; set; }
         
        public Func<DealershipData, bool> WherePredicate  { get; set; }
        public Func<DealershipData, object> OrderByPredicate  { get; set; }
        public int Size  { get; set; }

    }
}