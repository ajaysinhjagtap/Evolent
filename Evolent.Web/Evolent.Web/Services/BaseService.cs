using System.Configuration;

namespace Evolent.Web.Services
{
    /// <summary>
    /// <see cref="BaseService"/> class.
    /// </summary>
    public class BaseService
    {
        #region Variables

        public static string baseUrl = ConfigurationManager.AppSettings["EvolentServiceApiUrl"];

        #endregion

    }
}