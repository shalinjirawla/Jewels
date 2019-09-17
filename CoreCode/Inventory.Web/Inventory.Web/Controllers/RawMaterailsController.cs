using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.Application.Interface.RawMaterails;
using Inventory.Application.ViewModel.RawMaterails;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RawMaterailsController : ControllerBase
    {
        public bool Status = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        private readonly SessionHanlderController _SessionHanlderController;
        private readonly IRawMaterails _irawMaterails;

        public RawMaterailsController(SessionHanlderController SessionHanlderController, IRawMaterails irawMaterails)
        {
            _SessionHanlderController = SessionHanlderController;
            _irawMaterails = irawMaterails;
        }

        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }

        [HttpPost]
        public IActionResult AddRawMaterails(RawMaterailsVm model)
        {
            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
             Status =  _irawMaterails.SaveRawMaterails(model, GetUserId, GetTenantId);
            if (Status)
                Message = "Raw Materails Successfully Saved..";
            else
                Message = "Raw Materails not Saved..";
            return Ok(GetAjaxResponse(Status, Message, Data));

        }


    }
}