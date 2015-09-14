using AdgisticsMotors.Common.Resources;
using AdgisticsMotorsReport;

namespace AdgisticsMotors.Web.Models
{
    public class DealershipMessage : RequestMessage<DealershipData>
    {
        public override void AddSuccess(DealershipData data)
        {
            IsError = false;
            Message = string.Format(FrontMessage.ProcessInformation, data.DealershipIdentifier,
                FrontMessage.ProcessInformationSuccess);
            DealershipData = data;
        }

        public override void AddError(string identifier, string exceptionMessage)
        {
            IsError = true;
            Message = string.Format(FrontMessage.ProcessInformation, identifier,
                string.Format(FrontMessage.ProcessInformationError, exceptionMessage));
        }

        public int Position { get; set; }
        public DealershipData DealershipData { get; set; }
    }
}