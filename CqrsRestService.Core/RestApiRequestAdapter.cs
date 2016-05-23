using CqrsRestService.Core;
using RestSharp;
using System.Linq;
using System;

namespace CqrsRestService.CorePortable
{
    public class RestApiRequestAdapter<T> : RestRequest
    {
        private RestApiQuery<T> _restApiQuery;

        public RestApiRequestAdapter(RestApiQuery<T> restApiQuery)
        {
            _restApiQuery = restApiQuery;            

            BindBaseRequestToAdapter();

            Method = GetHttpMethod(restApiQuery.HttpMethod);
            Resource = ParameterizeApiQuery(_restApiQuery);

            if (restApiQuery.RequestBody != null)
            {
                AddJsonBody(restApiQuery.RequestBody);
            }            
        }

        private string ParameterizeApiQuery(IRestApiQuery _restApiQuery)
        {
            var resourceString = _restApiQuery.GetResource();

            foreach (var property in _restApiQuery.GetType().GetProperties(
                        System.Reflection.BindingFlags.Public
                        | System.Reflection.BindingFlags.Instance
                        | System.Reflection.BindingFlags.DeclaredOnly))
            {
                var resourceParameter = "@" + property.Name;
                resourceString = resourceString.Replace(resourceParameter, property.GetValue(_restApiQuery).ToString());
            }

            return resourceString;
        }

        private void BindBaseRequestToAdapter()
        {
            var restProperties = GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in restProperties)
            {
                var queryProperty = _restApiQuery.GetType().GetProperty(property.Name);
                if (queryProperty == null) continue;

                var queryValue = queryProperty.GetValue(_restApiQuery);
                if (queryValue == null) continue;

                property.SetValue(this, queryValue);                                
            }
        }

        private Method GetHttpMethod(string httpVerb)
        {
            Method restMethod;
            switch (httpVerb.ToLower())
            {
                case "get": restMethod = Method.GET; break;
                case "post": restMethod = Method.POST; break;
                case "put": restMethod = Method.PUT; break;
                case "delete": restMethod = Method.DELETE; break;
                case "head": restMethod = Method.HEAD; break;
                case "options": restMethod = Method.OPTIONS; break;
                case "patch": restMethod = Method.PATCH; break;
                case "merge": restMethod = Method.MERGE; break;
                default: restMethod = Method.GET; break;
            }

            return restMethod;
        }
    }
}