using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.share
{
    //[Serializable]
    public class ApiResponse
    {
            public bool Status { get; set; } = false;

            public object Data { get; set; } = null;

            public string Message { get; set; } = string.Empty;

    }
}
