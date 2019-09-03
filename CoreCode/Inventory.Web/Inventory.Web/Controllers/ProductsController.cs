using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Products;
using Inventory.Application.Services.ProductsServices;
using Inventory.Application.ViewModel.Products;
using Inventory.Application.ViewModel.ProductsVm;
using Inventory.Web.share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCategories _ProductCategoriesServices;
        private readonly IProductBrand _ProductBrandServices;
        public Boolean Result = false;
        public string Message = "";
        public ProductsController(IProductCategories productCategoriesServices, IProductBrand productBrand)
        {
            _ProductCategoriesServices = productCategoriesServices;
            _ProductBrandServices = productBrand;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }

        #region Product Categories Start Apis
        [HttpPost]
        public async Task<IActionResult> SaveProductCategories(ProductCategoriesVm model)
        {
            Boolean Result=false;
            string Message = "";
            if (ModelState.IsValid) {
                Result =await _ProductCategoriesServices.SaveProductCategories(model);
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
            list = await _ProductCategoriesServices.GetCategoriesList();
            return Ok(GetAjaxResponse(true, "", list));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCategorie(long CategoriesId)
        {
            ProductCategoriesVm mode = new ProductCategoriesVm();
            if (CategoriesId != 0) {
                mode =await _ProductCategoriesServices.GetCategories(CategoriesId);
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
                Result = await _ProductCategoriesServices.UpdateProductCategories(CategoriesId,model);
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
                Result = await _ProductCategoriesServices.DeleteProductCategorie(CategoriesId);
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
        #endregion Product Categories End Apis

        #region Product Brands Start Apis

        [HttpGet]
        public async Task<IActionResult> GetProductBrandList()
        {
           List<ProductBrandVm> list = new List<ProductBrandVm>();
            list = await _ProductBrandServices.GetCategoriesList();
            return Ok(GetAjaxResponse(true, "List Product Brand", list));
        }
        [HttpPost]
        public async Task<IActionResult> SaveProductBrand(ProductBrandVm model)
        {
            if (ModelState.IsValid) {
                Result= await _ProductBrandServices.SaveProductCategories(model);
                if (Result)
                {
                    Message = "Product Brand Successfully Saved..!";
                }
                else
                {
                    Message = "Error Occurs";
                }
            }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductBrand(long BrandId) {
            ProductBrandVm productBrandVm = new ProductBrandVm();
            if (BrandId != 0) {
                productBrandVm= await _ProductBrandServices.GetCategories(BrandId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true, string.Empty, productBrandVm));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductBrand(long BrandId, ProductBrandVm model) {
            if (BrandId != 0 && ModelState.IsValid) {
                Result = await _ProductBrandServices.UpdateProductCategories(BrandId, model);
                if (Result)
                {
                    Message = "Product Successfully Updated..!";
                }
                else {
                    Message = "Error Occurs";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductBrand(long BrandId) {
            if (BrandId != 0) {
                Result = await _ProductBrandServices.DeleteProductCategorie(BrandId);
                if (Result)
                {
                    Message = "Product Successfully Deleted..!";
                }
                else {
                    Message = "Error Occurs";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        #endregion  Product Brands End Apis
    }
}