using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Evolent.Entities;
using System.Net.Http;
using Evolent.Web.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace Evolent.Web.Services
{
    public class ContactService : BaseService, IContactService
    {
        #region Variables

        readonly string uri = string.Format("{0}{1}", baseUrl, "Contacts");

        #endregion

        
        public async Task<IEnumerable<ContactEntity>> GetAllContacts()
        {
            using (HttpClient objHttpClient = HttpClientHelper.CreateHttpClient(string.Empty))
            {
                return JsonConvert.DeserializeObject<List<ContactEntity>>(
                await objHttpClient.GetStringAsync(string.Format("{0}/Get", uri)));
            }
        }

        public async Task<ContactEntity> GetContactById(int contactId)
        {
            using (HttpClient objHttpClient = HttpClientHelper.CreateHttpClient(string.Empty))
            {
                return JsonConvert.DeserializeObject<ContactEntity>(await objHttpClient.GetStringAsync(
                    string.Format("{0}/GetContactById?contactId={1}", uri, contactId)));
            }
        }

        public async Task<int> CreateContact(ContactEntity contactEntity)
        {
            using (HttpClient objHttpClient = HttpClientHelper.CreateHttpClient(string.Empty))
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(contactEntity), Encoding.UTF8, "application/json");

                int result = await objHttpClient.PostAsync(uri + "/Post", content).ContinueWith((postTask) =>
                {
                    return JsonConvert.DeserializeObject<int>(postTask.Result.Content.ReadAsStringAsync().Result);
                });
                return result;
            }
        }

        public async Task<bool> UpdateContact(ContactEntity contactEntity)
        {
            using (HttpClient objHttpClient = HttpClientHelper.CreateHttpClient(string.Empty))
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(contactEntity), Encoding.UTF8, "application/json");

                bool result = await objHttpClient.PutAsync(uri + "/Put", content).ContinueWith((postTask) =>
                {
                    return JsonConvert.DeserializeObject<bool>(postTask.Result.Content.ReadAsStringAsync().Result);
                });
                return result;
            }
        }

        public async Task<bool> DeleteContact(int contactId)
        {
            using (HttpClient objHttpClient = HttpClientHelper.CreateHttpClient(string.Empty))
            {
                HttpResponseMessage reponse= await objHttpClient.DeleteAsync(string.Format("{0}/Delete?contactId={1}", uri, contactId));
                return JsonConvert.DeserializeObject<bool>(reponse.Content.ReadAsStringAsync().Result);
            }
        }

    }
}