using CqrsRestService.CorePortable;
using RestSharp;
using System;
using System.Text.RegularExpressions;

namespace CqrsRestService.Core
{
    public abstract class RestApiQuery<T> : IRestApiQuery
    {
        public RestApiQuery()
        {
            HttpMethod = GetHttpMethod(SplitCamelCase(GetType().Name).Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]);
        }

        public Method HttpMethod { get; protected set; }

        public object Body { get; protected set; }

        public abstract string GetApiResource();

        #region Helpers
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

        private string SplitCamelCase(string str)
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

        #endregion

    }
}