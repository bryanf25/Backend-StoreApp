using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreApi.Models
{
    public class ResponseModel
    {
        public string MessageResponse { get; set; }
        public dynamic BodyResponse { get; set; }
    }
}