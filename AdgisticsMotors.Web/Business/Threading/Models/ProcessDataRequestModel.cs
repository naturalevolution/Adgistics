using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdgisticsMotors.Web.Business.Threading.Models
{
    public class ProcessDataRequestModel : BaseInformation
    {
        public int Size { get; set; }
        public int Position { get; set; } 
        public string Line { get; set; }

        public int GetProgressBarPosition()
        {
            return (int) ((Position/(double) Size)*100.0);
        }

        public string GetIdentifier()
        {
            return GetValue(0);
        }

        public Uri GetUri()
        {
            var endpoint = GetValue(1);
            return new Uri(endpoint);
        }

        private string GetValue(int index)
        {
            if (!string.IsNullOrWhiteSpace(Line))
            {
                var datas = Line.Split(Convert.ToChar(","));
                if (datas.Length > index && !string.IsNullOrWhiteSpace(datas[index]))
                {
                    return datas[index];
                }
            }
            return string.Empty;
        }

        public ProcessDataRequestModel(IHubCallerConnectionContext<dynamic> clients) : base(clients)
        {
        }
    }
}