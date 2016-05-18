﻿using CqrsRestService.CorePortable;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsRestService.CorePortable
{
    public class RestService : IRestService
    {
        private RestClient _restClient;
        private IServiceHost _serviceHost;

        public RestService(IServiceHost serviceHost)
        {
            _serviceHost = serviceHost;
            _restClient = new RestClient(serviceHost.HostName);
        }

        public async Task ExecuteQuery<T>(IRestServiceQuery<T> getHelloQuery)
        {
            var restRequest = new RestRequestAdapter<T>(getHelloQuery);
            var restResponse = await _restClient.ExecuteTaskAsync<T>(restRequest);
            getHelloQuery.Result = restResponse.Data;
        }
    }
}