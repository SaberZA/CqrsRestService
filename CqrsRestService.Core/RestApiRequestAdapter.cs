using CqrsRestService.Core;
using RestSharp;

namespace CqrsRestService.CorePortable
{
    public class RestApiRequestAdapter<T> : RestRequest
    {
        private RestApiQuery<T> restApiQuery;

        public RestApiRequestAdapter(RestApiQuery<T> restApiQuery)
        {
            this.restApiQuery = restApiQuery;

            this.Method = restApiQuery.HttpMethod;
            this.Resource = restApiQuery.GetApiResource();
        }
    }
}