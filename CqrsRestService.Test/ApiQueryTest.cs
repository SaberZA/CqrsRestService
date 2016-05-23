using CqrsRestService.Core;
using CqrsRestService.CorePortable;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CqrsRestService.Test
{

    [TestFixture]
    public class ApiQueryTest
    {
        private RestService _restService;

        [SetUp]
        public void Setup()
        {
            var serviceHost = new ServiceHost("http://demo5104102.mockable.io");
            _restService = new RestService(serviceHost);
        }

        [Test]
        public void Construct_RestService()
        {
            Assert.IsNotNull(_restService);
        }

        //[Ignore("Need an API that supports root resource")]
        //[Test]
        //public async Task NoResource_GetBaseResultFromDemoService()
        //{
        //    var getQuery = new ApiGetQuery();
        //    await _restService.ExecuteQuery(getQuery);

        //    Assert.IsNotNull(getQuery.Result);
        //    Assert.AreEqual("base method", getQuery.Result.Msg);
        //}

        [Test]
        public async Task Hello_GetResultFromDemoService()
        {
            var getHelloQuery = new GetHelloQuery();
            await _restService.ExecuteQuery(getHelloQuery);

            Assert.IsNotNull(getHelloQuery.Result);
            Assert.AreEqual("hello world", getHelloQuery.Result.Msg);
        }

        [Test]
        public async Task Hello_GetResultWithIdParameter_ShouldReturn45()
        {
            var getHelloQuery = new GetHello_Id_Query();
            getHelloQuery.Id = "3";

            await _restService.ExecuteQuery(getHelloQuery);

            Assert.IsNotNull(getHelloQuery.Result);
            Assert.AreEqual("45", getHelloQuery.Result.Msg);
        }

    }
}
