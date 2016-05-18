using CqrsRestService.Core;
using CqrsRestService.CorePortable;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CqrsRestService.Test
{
    
    [TestFixture]
    public class RestServiceTest
    {
        private RestService _restService;

        [SetUp]
        public void Setup()
        {
            var serviceHost = new ServiceHost("http://demo5732750.mockable.io");
            _restService = new RestService(serviceHost);
        }

        [Test]
        public void Construct_RestService()
        {
            var serviceHost = new ServiceHost("http://demo5732750.mockable.io");
            var restService = new RestService(serviceHost);

            Assert.IsNotNull(restService);
        }                

        [Test]
        public async Task Hello_GetResultFromDemoService()
        {
            var getHelloQuery = new GetHelloQuery();
            await _restService.ExecuteQuery(getHelloQuery);

            Assert.IsNotNull(getHelloQuery.Result);
            Assert.AreEqual("hello world", getHelloQuery.Result.Msg);
        }
    }
}
