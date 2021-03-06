﻿using CqrsRestService.Core;
using CqrsRestService.CorePortable;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsRestService.CorePortable
{
    public class RestService : IRestService
    {
        private RestClient _restClient;
        private IServiceHost _serviceHost;
        private ILogger _logger = new NullLogger();

        public RestService(IServiceHost serviceHost)
        {
            _serviceHost = serviceHost;
            _restClient = new RestClient(serviceHost.HostName);
        }

        public RestService(IServiceHost serviceHost, ILogger logger) : this(serviceHost)
        {
            _logger = logger;
        }

        public async Task ExecuteQuery<T>(RestServiceQuery<T> getHelloQuery)
        {
            var restRequest = new RestRequestAdapter<T>(getHelloQuery);
            var restResponse = await _restClient.ExecuteTaskAsync<T>(restRequest);
            getHelloQuery.Result = restResponse.Data;
        }

        public async Task ExecuteQuery<T>(RestApiQuery<T> restApiQuery)
        {
            var restRequest = new RestApiRequestAdapter<T>(restApiQuery);
            _logger.Log(string.Format("RestRequest: {0}, {1}, {2}",restRequest.Method, restRequest.Resource, _serviceHost.HostName));
            var restResponse = await _restClient.ExecuteTaskAsync<T>(restRequest);
            restApiQuery.RestResponse = restResponse;
            restApiQuery.Result = restResponse.Data;
        }
    }

    

}
