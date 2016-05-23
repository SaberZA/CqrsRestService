using CqrsRestService.CorePortable;
using System;

namespace CqrsRestService.Core
{
    public class GetHelloQuery : RestServiceQuery<GetHelloQueryResult>
    {
        //[HttpVerb][Resource]["Query"]
        public GetHelloQuery()
        {
        }
    }
}