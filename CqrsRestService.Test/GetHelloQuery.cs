using CqrsRestService.Core;
using System;

namespace CqrsRestService.Test
{
    public class GetHelloQuery : RestApiQuery<GetQueryResult>
    {
        public GetHelloQuery()
        {
        }

        public override string GetApiResource()
        {
            return @"hello";
        }
    }
}