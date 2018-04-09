using Evolent.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Evolent.WebApi.Tests
{
    [TestClass]
    public class EvolentWebApiUnitTest
    {
        #region Variables

        HttpClient _httpClient = null;
        string _baseAddress = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Evolent WebApi Unit Test
        /// </summary>
        public EvolentWebApiUnitTest()
        {
            _baseAddress = "http://localhost:60252/api/";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        #endregion

        #region Functions and Methods

        [TestMethod]
        public void GetAllContacts()
        {
            string apiCall = "Contacts/Get";
            HttpResponseMessage httpResponseMessage =  _httpClient.GetAsync(apiCall).Result;
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }

        [TestMethod]
        public void GetContactById()
        {
            string apiCall = "Contacts/GetContactById?contactId=1";
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(apiCall).Result;
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }

            #endregion
        }
}
