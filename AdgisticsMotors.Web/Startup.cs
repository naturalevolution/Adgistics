using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AdgisticsMotors.Web.Startup))]

namespace AdgisticsMotors.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Mapping the SignalR host
            app.MapSignalR();
        }
    }
}
