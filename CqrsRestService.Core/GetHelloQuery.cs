using CqrsRestService.CorePortable;
using System;

namespace CqrsRestService.Core
{
    public class GetHelloQuery : IRestServiceQuery<GetHelloQueryResult>
    {
        //[HttpVerb][Resource]["Query"]
        public GetHelloQuery()
        {
        }

        public GetHelloQueryResult Result { get; set; }
    }
}