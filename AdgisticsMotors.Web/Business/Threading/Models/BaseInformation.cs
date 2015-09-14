using Microsoft.AspNet.SignalR.Hubs;

namespace AdgisticsMotors.Web.Business.Threading.Models
{
    public abstract class BaseInformation
    {
        protected BaseInformation(IHubCallerConnectionContext<dynamic> clients)
        {
            HubCaller = clients;
        }
        public IHubCallerConnectionContext<dynamic> HubCaller { get; set; }
    }
}