using System.Collections.Generic;
using AdgisticsMotors.Common.Resources;
using AdgisticsMotorsReport;

namespace AdgisticsMotors.Web.Business.Threading.Models
{
    public class ProcessReportResponseModel
    { 
        public string Id { get; set; }
        public string Name { get; set; } 
        public List<DealershipData> Datas { get; set; }
    }
}