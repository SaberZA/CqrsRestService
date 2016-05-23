using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Serializers;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Linq;

namespace CqrsRestService.CorePortable
{
    public class RestRequestAdapter<T> : RestRequest
    {
        private RestServiceQuery<T> _contextQuery;

        public RestRequestAdapter(RestServiceQuery<T> getHelloQuery)
        {
            _contextQuery = getHelloQuery;

            var queryType = _contextQuery.GetType();
            var queryName = queryType.Name;
            var queryProperties = queryType.GetProperties().Where(p => p.Name != "Result" && p.Name != "Body");

            var substitutedQueryName = queryName;
            foreach (var prop in queryProperties)
            {
                if (substitutedQueryName.Contains(prop.Name))
                {
                    substitutedQueryName = substitutedQueryName.Replace(prop.Name, string.Format("/{0}/",prop.GetValue(_contextQuery).ToString()));
                }
            }

            substitutedQueryName = substitutedQueryName.Replace("_", "");            
            
            var restParameters = SplitCamelCase(substitutedQueryName).Split(new[] { " " },StringSplitOptions.RemoveEmptyEntries);

            var httpVerb = "";
            var resource = "";
            var queryFormat = "";

            try
            {
                httpVerb = restParameters[0];

                if (restParameters.Length >= 4)
                {

                    for (int i = 1; i < restParameters.Length - 1; i++)
                    {
                        resource += restParameters[i];
                    }
                }
                else
                {
                    resource = restParameters[1];
                }             
                
                


                if (httpVerb.Length == 0)
                {
                    throw new Exception("Could not create HTTP Verb from ClassName");
                }
                
            }
            catch (Exception ex)
            {
                throw new RestFormatException<T>(getHelloQuery, ex.Message);
            }

            Resource = GetFormattedResource(resource);
            queryFormat = restParameters[restParameters.Length - 1];

            if (string.Compare(Resource, queryFormat, true) == 0)
            {
                Resource = "/";
            }

            Method = GetHttpMethod(httpVerb);
            if (_contextQuery.Body != null)
            {
                AddBody(_contextQuery.Body);
            }            
        }

        private string GetFormattedResource(string resource)
        {
            var tempResource = resource;

            tempResource = tempResource.ToLower();

            tempResource = tempResource.EndsWith("/") ? tempResource.Substring(0, tempResource.Length - 1) : tempResource;

            return tempResource;
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

        public string SplitCamelCase(string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }       
        
    }

    [Serializable]
    public class RestFormatException<T> : Exception
    {
        private string _message;
        private RestServiceQuery<T> _restQuery;
        
        public RestFormatException(RestServiceQuery<T> restQuery, string message)
        {
            _restQuery = restQuery;
            _message = message;
        }
        
    }
}