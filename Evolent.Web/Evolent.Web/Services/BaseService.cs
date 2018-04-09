using System.Configuration;

namespace Evolent.Web.Services
{
    public class BaseService
    {
        #region Variables

        public static string baseUrl = ConfigurationManager.AppSettings["EvolentServiceApiUrl"];

        #endregion

    }
}