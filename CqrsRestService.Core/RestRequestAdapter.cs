using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Serializers;

namespace CqrsRestService.CorePortable
{
    public class RestRequestAdapter<T> : RestRequest
    {
        private IRestServiceQuery<T> _contextQuery;

        public RestRequestAdapter(IRestServiceQuery<T> getHelloQuery)
        {
            _contextQuery = getHelloQuery;

            Resource = "Hello".ToLower();
            Method = RestMethod;
        }        

        public Method RestMethod
        {
            get
            {
                return Method.GET;
            }
        }
        
    }
}