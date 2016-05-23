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
            HttpMethod = SplitCamelCase(GetType().Name).Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
        }        

        public string HttpMethod { get; protected set; }

        public object RequestBody { get; protected set; }

        public abstract string GetResource();

        public T Result { get; set; }

        public IRestResponse<T> RestResponse { get; set; }

        #region Helpers        

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