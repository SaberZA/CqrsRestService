using System;
using CqrsRestService.Core;
using CqrsRestService.CorePortable;

namespace CqrsRestService.Test
{
    public class GetHello_Id_Query : RestServiceQuery<GetHelloQueryResult>
    {
        public GetHello_Id_Query()
        {
        }

        public string Id { get; set; }        
    }
}