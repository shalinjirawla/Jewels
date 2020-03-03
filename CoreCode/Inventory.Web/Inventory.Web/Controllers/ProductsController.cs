using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.Application.Interface.Products;
using Inventory.Application.Services.ProductsServices;
using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.Products;
using Inventory.Application.ViewModel.ProductsVm;
using Inventory.Core.Models.ApplicationUser;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCategories _ProductCategoriesServices;
        private readonly IProductBrand _ProductBrandServices;
        // private readonly IApplicationUser _applicationUser;
        public Boolean Result = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        private readonly SessionHanlderController _SessionHanlderController;
        private readonly IProduct _Product;
        private readonly IProductService _IProductService;
        public ProductsController(IProductCategories productCategoriesServices, IProductBrand productBrand,
             SessionHanlderController SessionHanlderController, IProduct Product, IProductService IProductService)
        {
            _ProductCategoriesServices = productCategoriesServices;
            _ProductBrandServices = productBrand;
            //_applicationUser = applicationUser;
            _SessionHanlderController = SessionHanlderController;
            _Product = Product;
            _IProductService = IProductService;
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
            if (ModelState.IsValid)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _ProductCategoriesServices.SaveProductCategories(model, GetUserId, GetTenantId);
                if (Result) { Message = "Categories Successfully Saved...!"; }
                else { Message = model.CategoriesName + " Is Already Exist...!"; }
            }
            else
            {
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
            if (CategoriesId != 0)
            {
                mode = await _ProductCategoriesServices.GetCategories(CategoriesId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true, string.Empty, mode));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductCategorie(long CategoriesId, ProductCategoriesVm model)
        {
            Boolean Result = false;
            string Message = "";
            if (ModelState.IsValid)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _ProductCategoriesServices.UpdateProductCategories(CategoriesId, model, GetUserId);
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
            if (CategoriesId != 0)
            {
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
            if (ModelState.IsValid)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _ProductBrandServices.SaveProductCategories(model, GetUserId, GetTenantId);
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
        public async Task<IActionResult> GetProductBrand(long BrandId)
        {
            ProductBrandVm productBrandVm = new ProductBrandVm();
            if (BrandId != 0)
            {
                productBrandVm = await _ProductBrandServices.GetCategories(BrandId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true, string.Empty, productBrandVm));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductBrand(long BrandId, ProductBrandVm model)
        {
            if (BrandId != 0 && ModelState.IsValid)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _ProductBrandServices.UpdateProductCategories(BrandId, model, GetUserId);
                if (Result)
                {
                    Message = "Product Successfully Updated..!";
                }
                else
                {
                    Message = "Error Occurs";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductBrand(long BrandId)
        {
            if (BrandId != 0)
            {
                Result = await _ProductBrandServices.DeleteProductCategorie(BrandId);
                if (Result)
                {
                    Message = "Product Successfully Deleted..!";
                }
                else
                {
                    Message = "Error Occurs";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        #endregion  Product Brands End Apis
        #region Product  Start Apis
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductVariantMergeVM model)
        {
            if (model != null)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _Product.SaveProduct(model, GetUserId, GetTenantId);
                if (Result) Message = "Product Successfully Saved..";
                else Message = "error";
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            Data = await _Product.GetProductList(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(long ProductId)
        {
            Data = await _Product.GetProduct(ProductId);
            if (Data != null)
                Result = true;
            else
                Result = false;
            return Ok(GetAjaxResponse(Result, string.Empty, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(long ProductId)
        {
            Result = await _Product.DeleteProduct(ProductId);
            if (Result)
                Message = "Product Delete Successfully";
            else
                Message = "error";
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        #endregion  Product  End Apis
        #region  Product Serives  End Apis

        [HttpPost]
        public async Task<IActionResult> SaveProductService(ProductServiceVM input)
        {
            if (input != null)
            {
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Result = await _IProductService.SaveService(input, GetUserId, GetTenantId);
                if (Result) Message = "Product Service Successfully Saved..";
                else Message = "error";
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceList()
        {
            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            Data = await _IProductService.GetServiceList(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpGet]
        public async Task<IActionResult> GetService(long ServiceId)
        {
            Data = await _IProductService.GetService(ServiceId);
            if (Data != null)
                Result = true;
            else
                Result = false;
            return Ok(GetAjaxResponse(Result, string.Empty, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteService(long ServiceId)
        {
            Result = await _IProductService.DeleteService(ServiceId);
            if (Result)
                Message = "Product Service Delete Successfully";
            else
                Message = "error";
            return Ok(GetAjaxResponse(Result, Message, null));
        }
        #endregion  Product Serives  End Apis
    }
}