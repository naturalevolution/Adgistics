using System;
using System.IO;
using System.Reflection;
using AdgisticsMotorsReport;

namespace AdgisticsMotors.Web.Business
{
    public class FileService
    {
        public string[] RetrieveLinesFromFile()
        {
            var filename = Path.Combine(
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(DealershipService)).CodeBase), "DealershipsList.txt");
            return File.ReadAllLines(new Uri(filename).AbsolutePath);
        }
    }
}