using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Products;
using Inventory.Application.Services.ProductsServices;
using Inventory.Application.ViewModel.Products;
using Inventory.Web.share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCategories _Services;
        public ProductsController(IProductCategories productCategoriesServices)
        {
            _Services = productCategoriesServices;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public async Task<IActionResult> SaveProductCategories(ProductCategoriesVm model)
        {
            Boolean Result=false;
            string Message = "";
            if (ModelState.IsValid) {
                Result =await _Services.SaveProductCategories(model);
                if (Result) { Message = "Categories Successfully Saved...!"; }
                else { Message = model.CategoriesName + " Is Already Exist...!"; }
            }
            else {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCategoriesList()
        {
            List<ProductCategoriesVm> list = new List<ProductCategoriesVm>();
            list = await _Services.GetCategoriesList();
            return Ok(GetAjaxResponse(true, "", list));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCategorie(long CategoriesId)
        {
            ProductCategoriesVm mode = new ProductCategoriesVm();
            if (CategoriesId != 0) {
                mode =await _Services.GetCategories(CategoriesId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true,string.Empty, mode));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductCategorie(long CategoriesId, ProductCategoriesVm model)
        {
            Boolean Result = false;
            string Message = "";
            if (ModelState.IsValid)
            {
                Result = await _Services.UpdateProductCategories(CategoriesId,model);
                if (Result) { Message = "Categories Successfully Updated...!"; }
                else { Message = model.CategoriesName + " Is Already Exist...!"; }
            }
            else
            {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductCategorie(long CategoriesId)
        {
            Boolean Result = false;
            string Message = "";
            if (CategoriesId != 0) {
                Result = await _Services.DeleteProductCategorie(CategoriesId);
                if (Result)
                {
                    Message = "Categories Successfulyy Deleted..";
                    Result = true;
                }
                else { Message = "error"; Result = false; }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
    }
}