using AdgisticsMotorsReport;

namespace AdgisticsMotors.Web.Business
{
    public class ServiceFactory
    {
        private static DealershipService _dealership;
        private static FileService _file;

        public static DealershipService Dealership
        {
            get { return _dealership ?? (_dealership = new DealershipService()); }
        }
        public static FileService File
        {
            get { return _file ?? (_file = new FileService()); }
        }
    }
}