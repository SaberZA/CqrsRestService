using System;
using CqrsRestService.Core;

namespace CqrsRestService.Test
{
    public class GetHelloIdQuery : RestApiQuery<GetHelloQueryResult>
    {
        public GetHelloIdQuery(int id)
        {
            Id = id;
            RequestBody = new { Test = 1 };
        }

        public int Id { get; private set; }

        public override string GetResource()
        {
            return "hello/@Id";
        }
    }
}