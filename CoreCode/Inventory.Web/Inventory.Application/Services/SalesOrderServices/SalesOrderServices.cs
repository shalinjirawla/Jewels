using Inventory.Application.Interface.SalesOrder;
using Inventory.Application.ViewModel.SalesOrder;
using Inventory.Core.Models.SalesOrder;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.SalesOrderServices
{
    public class SalesOrderServices : ISalesOrder
    {
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status = false;
        public string Message = "";
        public SalesOrderServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<long> SaveSalesOrder(SalesOrderMergeVM input, string UserId, long TenantId)
        {
            try
            {

                SalesOrdersVM salesOrdersVM = new SalesOrdersVM();
                salesOrdersVM = input.SalesOrdersVM;

                SalesOrderDetailsVM salesOrderDetailsVM = new SalesOrderDetailsVM();
                salesOrderDetailsVM = input.SalesOrderDetailsVM;

                List<SalesOrderItemsVM> SalesOrderItemsList = new List<SalesOrderItemsVM>();
                SalesOrderItemsList = input.SalesOrderItemsList;
                await Task.Run(async () =>
                {
                    //Sales Orders save start
                    if (salesOrdersVM.SalesOrdersId == 0)
                    {
                        SalesOrders salesOrders = new SalesOrders()
                        {
                            SalesOrderNumber = "SO-"+SalesOrderNumberRandomString(),
                            DateOrdered = DateTime.Now,
                            EstimatedDeliveryDate = salesOrdersVM.EstimatedDeliveryDate.HasValue ? salesOrdersVM.EstimatedDeliveryDate : null,
                            CustomerId = salesOrdersVM.CustomerId,
                            CustomerPurchesOrderNumber = salesOrdersVM.CustomerPurchesOrderNumber,
                            Remarks = salesOrdersVM.Remarks,
                            SalesOrderTypeId = salesOrdersVM.SalesOrderTypeId.HasValue ? salesOrdersVM.SalesOrderTypeId : null,
                            CreditTermId = salesOrdersVM.CreditTermId.HasValue ? salesOrdersVM.CreditTermId : null,
                            ShipmentMethodId = salesOrdersVM.ShipmentMethodId.HasValue ? salesOrdersVM.ShipmentMethodId : null,
                            SalesOrderRepId = salesOrdersVM.SalesOrderRepId.HasValue ? salesOrdersVM.SalesOrderRepId : null,
                            CurrencyId = salesOrdersVM.CurrencyId.HasValue ? salesOrdersVM.CurrencyId : null,
                            PaymentStatus = 0,
                            ShipmentStatus = 0,
                            CreationTime = DateTime.Now,
                            TenantsId = TenantId,
                            CreatorUserId = UserId,
                            IsActive = true,
                        };
                        _DbContext.SalesOrders.Add(salesOrders);
                        _DbContext.SaveChanges();
                        input.SalesOrdersVM.SalesOrdersId = salesOrders.SalesOrdersId;
                        //Sales Orders save end
                        //Sales Orders details save start
                        SalesOrderDetails salesOrderDetails = new SalesOrderDetails()
                        {
                            SalesOrdersId = input.SalesOrdersVM.SalesOrdersId,
                            TotalQTY = salesOrderDetailsVM.TotalQTY,
                            Total = salesOrderDetailsVM.Total,
                            FinalTotal = salesOrderDetailsVM.FinalTotal,
                            TaxInclude = salesOrderDetailsVM.TaxInclude,
                            FinalTaxTotal = salesOrderDetailsVM.FinalTaxTotal,
                            AdditionalChargeAmount = salesOrderDetailsVM.AdditionalChargeAmount,
                            IsAdditionalChargeApply = salesOrderDetailsVM.IsAdditionalChargeApply,
                            IsAdditionalChargeApplyType = salesOrderDetailsVM.IsAdditionalChargeApplyType
                        };
                        _DbContext.SalesOrderDetails.Add(salesOrderDetails);
                        _DbContext.SaveChanges();

                        //Sales Orders details save end
                        // Sales Order Items save start
                        if (SalesOrderItemsList != null && SalesOrderItemsList.Count > 0)
                        {
                            foreach (var item in SalesOrderItemsList)
                            {
                                if (item.TaxId.Value == 0)
                                    item.TaxId = null;
                                SalesOrderItems salesOrderItems = new SalesOrderItems()
                                {
                                    SalesOrdersId = input.SalesOrdersVM.SalesOrdersId,
                                    ProductId = item.ProductId.HasValue ? item.ProductId : null,
                                    Unit = item.Unit.HasValue ? item.Unit : null,
                                    UnitPrice = item.UnitPrice.HasValue ? item.UnitPrice : null,
                                    QTY = item.QTY.HasValue ? item.QTY : null,
                                    DiscountType = item.DiscountType.HasValue ? item.DiscountType : null,
                                    Discount = item.Discount.HasValue ? item.Discount : null,
                                    IsTaxble = item.IsTaxble.HasValue ? item.IsTaxble : null,

                                    TaxId = item.IsTaxble.HasValue && item.TaxId.HasValue ? item.TaxId : null,
                                    TaxTotal = item.IsTaxble.HasValue && item.TaxId.HasValue && item.TaxTotal.HasValue ? item.TaxTotal : null,
                                    Total = item.Total.HasValue ? item.Total : null,
                                };
                                _DbContext.SalesOrderItems.Add(salesOrderItems);
                                _DbContext.SaveChanges();
                            }
                        }

                        // Sales Order Items save end
                        if (salesOrderDetailsVM.IsAdditionalChargeApplyType == "All" &&
                        salesOrderDetailsVM.AdditionalChargeForAll != null && salesOrderDetailsVM.AdditionalChargeForAll.Count > 0)
                        {
                            foreach (var item in salesOrderDetailsVM.AdditionalChargeForAll)
                            {
                                if (item != null)
                                {
                                    item.SalesOrdersId = input.SalesOrdersVM.SalesOrdersId;
                                    await SaveAdditonalChargeForAll(item);
                                }
                            }
                        }
                        else if (salesOrderDetailsVM.IsAdditionalChargeApplyType == "Product" &&
                        salesOrderDetailsVM.AdditionalChargeForProduct != null && salesOrderDetailsVM.AdditionalChargeForProduct.Count > 0)
                        {
                            foreach (var item in salesOrderDetailsVM.AdditionalChargeForProduct)
                            {
                                if (item != null)
                                {
                                    item.SalesOrdersId = input.SalesOrdersVM.SalesOrdersId;
                                    await SaveAdditonalChargeForProduct(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        var salesOrders = _DbContext.SalesOrders.FirstOrDefault(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId);
                        if (salesOrders != null)
                        {
                            salesOrders.EstimatedDeliveryDate = salesOrdersVM.EstimatedDeliveryDate.HasValue ? salesOrdersVM.EstimatedDeliveryDate : null;
                            salesOrders.CustomerId = salesOrdersVM.CustomerId;
                            salesOrders.CustomerPurchesOrderNumber = salesOrdersVM.CustomerPurchesOrderNumber;
                            salesOrders.Remarks = salesOrdersVM.Remarks;
                            salesOrders.SalesOrderTypeId = salesOrdersVM.SalesOrderTypeId.HasValue ? salesOrdersVM.SalesOrderTypeId : null;
                            salesOrders.CreditTermId = salesOrdersVM.CreditTermId.HasValue ? salesOrdersVM.CreditTermId : null;
                            salesOrders.ShipmentMethodId = salesOrdersVM.ShipmentMethodId.HasValue ? salesOrdersVM.ShipmentMethodId : null;
                            salesOrders.SalesOrderRepId = salesOrdersVM.SalesOrderRepId.HasValue ? salesOrdersVM.SalesOrderRepId : null;
                            salesOrders.CurrencyId = salesOrdersVM.CurrencyId.HasValue ? salesOrdersVM.CurrencyId : null;
                            salesOrders.LastModificationTime = DateTime.Now;
                            salesOrders.LastModifierUserId = UserId;



                            _DbContext.SalesOrders.Update(salesOrders);
                            _DbContext.SaveChanges();
                        }
                        var salesOrderDetails = _DbContext.SalesOrderDetails.FirstOrDefault(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId);
                        if (salesOrderDetails != null)
                        {
                            salesOrderDetails.TotalQTY = salesOrderDetailsVM.TotalQTY;
                            salesOrderDetails.Total = salesOrderDetailsVM.Total;
                            salesOrderDetails.FinalTotal = salesOrderDetailsVM.FinalTotal;
                            salesOrderDetails.TaxInclude = salesOrderDetailsVM.TaxInclude;
                            salesOrderDetails.FinalTaxTotal = salesOrderDetailsVM.FinalTaxTotal;
                            salesOrderDetails.AdditionalChargeAmount = salesOrderDetailsVM.AdditionalChargeAmount;
                            salesOrderDetails.IsAdditionalChargeApply = salesOrderDetailsVM.IsAdditionalChargeApply;
                            salesOrderDetails.IsAdditionalChargeApplyType = salesOrderDetailsVM.IsAdditionalChargeApplyType;
                            _DbContext.SalesOrderDetails.Update(salesOrderDetails);
                            _DbContext.SaveChanges();
                        }
                        if (SalesOrderItemsList != null && SalesOrderItemsList.Count > 0)
                        {
                            var DeleteSalesOrderItems = _DbContext.SalesOrderItems.Where(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId).ToList();
                            if (DeleteSalesOrderItems != null && DeleteSalesOrderItems.Count() > 0)
                            {
                                _DbContext.SalesOrderItems.RemoveRange(DeleteSalesOrderItems);
                                _DbContext.SaveChanges();
                            }
                            var checkisdeleted = _DbContext.SalesOrderItems.Count(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId);
                            if (checkisdeleted == 0)
                            {
                                foreach (var item in SalesOrderItemsList)
                                {
                                    if (item.TaxId.Value == 0)
                                        item.TaxId = null;
                                    SalesOrderItems salesOrderItems = new SalesOrderItems()
                                    {
                                        SalesOrdersId = input.SalesOrdersVM.SalesOrdersId,
                                        ProductId = item.ProductId.HasValue ? item.ProductId : null,
                                        Unit = item.Unit.HasValue ? item.Unit : null,
                                        UnitPrice = item.UnitPrice.HasValue ? item.UnitPrice : null,
                                        QTY = item.QTY.HasValue ? item.QTY : null,
                                        DiscountType = item.DiscountType.HasValue ? item.DiscountType : null,
                                        Discount = item.Discount.HasValue ? item.Discount : null,
                                        IsTaxble = item.IsTaxble.HasValue ? item.IsTaxble : null,

                                        TaxId = item.IsTaxble.HasValue && item.TaxId.HasValue ? item.TaxId : null,
                                        TaxTotal = item.IsTaxble.HasValue && item.TaxId.HasValue && item.TaxTotal.HasValue ? item.TaxTotal : null,
                                        Total = item.Total.HasValue ? item.Total : null,
                                    };
                                    _DbContext.SalesOrderItems.Add(salesOrderItems);
                                    _DbContext.SaveChanges();
                                }
                            }
                        }
                        if (salesOrderDetailsVM.IsAdditionalChargeApply.HasValue && salesOrderDetailsVM.IsAdditionalChargeApply.Value)
                        {
                            if (salesOrderDetailsVM.IsAdditionalChargeApplyType == "All")
                            {
                                var deleteAdditionalforAll = _DbContext.SalesOrderAdditionalChargeForAll.Where(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId).ToList();
                                if (deleteAdditionalforAll != null && deleteAdditionalforAll.Count() > 0)
                                {
                                    _DbContext.SalesOrderAdditionalChargeForAll.RemoveRange(deleteAdditionalforAll);
                                    _DbContext.SaveChanges();
                                }
                                var checkIsExistAll = _DbContext.SalesOrderAdditionalChargeForAll.Count(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId);
                                if (checkIsExistAll == 0)
                                {
                                    foreach (var item in salesOrderDetailsVM.AdditionalChargeForAll)
                                    {
                                        if (item != null)
                                        {
                                            item.SalesOrdersId = input.SalesOrdersVM.SalesOrdersId;
                                            await SaveAdditonalChargeForAll(item);
                                        }
                                    }
                                }
                            }
                            else if (salesOrderDetailsVM.IsAdditionalChargeApplyType == "Product")
                            {
                                var deleteAdditionalforProduct = _DbContext.SalesOrderAdditionalChargeForProduct.Where(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId).ToList();
                                if (deleteAdditionalforProduct != null && deleteAdditionalforProduct.Count() > 0)
                                {
                                    _DbContext.SalesOrderAdditionalChargeForProduct.RemoveRange(deleteAdditionalforProduct);
                                    _DbContext.SaveChanges();
                                }
                                var checkIsExistProduct = _DbContext.SalesOrderAdditionalChargeForProduct.Count(x => x.SalesOrdersId == salesOrdersVM.SalesOrdersId);
                                if (checkIsExistProduct == 0)
                                {
                                    foreach (var item in salesOrderDetailsVM.AdditionalChargeForProduct)
                                    {
                                        if (item != null)
                                        {
                                            item.SalesOrdersId = input.SalesOrdersVM.SalesOrdersId;
                                            await SaveAdditonalChargeForProduct(item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return input.SalesOrdersVM.SalesOrdersId;
        }
        public async Task SaveAdditonalChargeForAll(SalesOrderAdditionalChargeForAllVM input)
        {
            try
            {
                await Task.Run(() =>
                {
                    SalesOrderAdditionalChargeForAll chargeForAll = new SalesOrderAdditionalChargeForAll()
                    {
                        SalesOrdersId = input.SalesOrdersId,
                        AdditionalChargeId = input.AdditionalChargeId.HasValue ? input.AdditionalChargeId : null,
                        TaxId = input.TaxId.HasValue ? input.TaxId : null,
                    };
                    _DbContext.SalesOrderAdditionalChargeForAll.Add(chargeForAll);
                    _DbContext.SaveChanges();
                });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task SaveAdditonalChargeForProduct(SalesOrderAdditionalChargeForProductVM input)
        {
            try
            {
                await Task.Run(() =>
                {

                    SalesOrderAdditionalChargeForProduct chargeForProduct = new SalesOrderAdditionalChargeForProduct()
                    {
                        SalesOrdersId = input.SalesOrdersId,
                        ProductId = input.ProductId.HasValue ? input.ProductId : null,
                        AdditionalChargeId = input.AdditionalChargeId.HasValue ? input.AdditionalChargeId : null,
                        TaxId = input.TaxId.HasValue ? input.TaxId : null,
                        IsTaxble = input.IsTaxble,
                    };
                    if (chargeForProduct.TaxId == 0)
                        chargeForProduct.TaxId = null;
                    _DbContext.SalesOrderAdditionalChargeForProduct.Add(chargeForProduct);
                    _DbContext.SaveChanges();
                });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static string SalesOrderNumberRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<List<SalesOrderListVM>> SalesOrderList(long TenantId)
        {
            List<SalesOrderListVM> List = new List<SalesOrderListVM>();
            try
            {
                if (TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Sales = (from sales in _DbContext.SalesOrders.ToList()
                                     join salesOrderDetails in _DbContext.SalesOrderDetails.ToList()
                                     on sales.SalesOrdersId equals salesOrderDetails.SalesOrdersId
                                     select new SalesOrderListVM
                                     {
                                         SalesOrdersId = sales.SalesOrdersId,
                                         SalesOrderNumber = sales.SalesOrderNumber,
                                         SalesOrderRepId = sales.SalesOrderRepId,
                                         CustomerId = sales.CustomerId,
                                         CustomerName = GetCustomerName(sales.CustomerId),
                                         PaymentStatus = sales.PaymentStatus,
                                         ShipmentStatus = sales.ShipmentStatus,
                                         Total = salesOrderDetails.FinalTotal ?? 0,
                                         DateOrdered = sales.DateOrdered,
                                         EstimatedDeliveryDate = sales.EstimatedDeliveryDate,
                                         TenantId = sales.TenantsId.HasValue ? sales.TenantsId.Value : 0,
                                         IsActive = sales.IsActive,
                                     }).Where(x => x.TenantId == TenantId && x.IsActive == true).ToList();
                        if (Sales != null)
                        {
                            if (Sales != null && Sales.Count() > 0)
                            {
                                foreach (var item in Sales)
                                {
                                    SalesOrderListVM salesOrder = new SalesOrderListVM()
                                    {
                                        SalesOrdersId = item.SalesOrdersId,
                                        SalesOrderNumber = item.SalesOrderNumber,
                                        SalesOrderRepId = item.SalesOrderRepId,
                                        CustomerId = item.CustomerId,
                                        CustomerName = GetCustomerName(item.CustomerId),
                                        PaymentStatus = item.PaymentStatus,
                                        ShipmentStatus = item.ShipmentStatus,
                                        Total = item.Total,
                                        DateOrdered = item.DateOrdered,
                                        EstimatedDeliveryDate = item.EstimatedDeliveryDate,
                                        TenantId = item.TenantId,
                                    };
                                    List.Add(salesOrder);
                                }
                            }
                        }
                    });
                }
                else
                    throw new Exception("Tenant Id Not Zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return List;
        }
        public string GetCustomerName(long CustomerId)
        {
            string Name = "";
            if (CustomerId != 0)
            {

                var customer = _DbContext.Customers.FirstOrDefault(x => x.CustomerId == CustomerId);
                if (customer != null)
                {
                    Name = customer.CustomerName;
                }

            }
            return Name;
        }

        public async Task DeleteSalesOrder(long SalesOrderId)
        {
            try
            {
                if (SalesOrderId != 0)
                {
                    await Task.Run(() =>
                    {
                        var sales = _DbContext.SalesOrders.FirstOrDefault(x => x.SalesOrdersId == SalesOrderId);
                        if (sales != null)
                        {
                            sales.IsActive = false;
                            _DbContext.SalesOrders.Update(sales);
                            _DbContext.SaveChanges();
                        }
                    });

                }
                else
                    throw new Exception("Sales Order Id not null");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SalesOrderMergeVM> GetSalesOrderDetails(long SalesOrderId)
        {
            SalesOrderMergeVM output = new SalesOrderMergeVM();
            SalesOrdersVM SalesOrdersVM = new SalesOrdersVM();
            SalesOrderDetailsVM SalesOrderDetailsVM = new SalesOrderDetailsVM();
            List<SalesOrderItemsVM> SalesOrderItemsList = new List<SalesOrderItemsVM>();
            try
            {
                if (SalesOrderId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Sales = (from sales in _DbContext.SalesOrders.ToList()
                                     join salesOrderDetails in _DbContext.SalesOrderDetails.ToList()
                                     on sales.SalesOrdersId equals salesOrderDetails.SalesOrdersId
                                     //join salesOrderItem in _DbContext.SalesOrderItems.ToList()
                                     //on sales.SalesOrdersId equals salesOrderItem.SalesOrdersId
                                     select new GetSalesOrderVM
                                     {
                                         //sales
                                         SalesOrdersId = sales.SalesOrdersId,
                                         SalesOrderNumber = sales.SalesOrderNumber,
                                         DateOrdered = sales.DateOrdered,
                                         EstimatedDeliveryDate = sales.EstimatedDeliveryDate,
                                         CustomerId = sales.CustomerId,
                                         CustomerPurchesOrderNumber = sales.CustomerPurchesOrderNumber,
                                         Remarks = sales.Remarks,
                                         SalesOrderTypeId = sales.SalesOrderTypeId,
                                         CreditTermId = sales.CreditTermId,
                                         ShipmentMethodId = sales.ShipmentMethodId,
                                         CurrencyId = sales.CurrencyId,
                                         SalesOrderRepId = sales.SalesOrderRepId,
                                         PaymentStatus = sales.PaymentStatus,
                                         ShipmentStatus = sales.ShipmentStatus,
                                         TenantId = sales.TenantsId.HasValue ? sales.TenantsId.Value : 0,
                                         IsActive = sales.IsActive,

                                         //sales details
                                         SalesOrderDetailsId = salesOrderDetails.SalesOrderDetailsId,
                                         TotalQTY = salesOrderDetails.TotalQTY,
                                         Total = salesOrderDetails.Total ?? 0,
                                         FinalTotal = salesOrderDetails.FinalTotal,
                                         TaxInclude = salesOrderDetails.TaxInclude,
                                         FinalTaxTotal = salesOrderDetails.FinalTaxTotal,
                                         AdditionalChargeAmount = salesOrderDetails.AdditionalChargeAmount,
                                         IsAdditionalChargeApply = salesOrderDetails.IsAdditionalChargeApply,
                                         IsAdditionalChargeApplyType = salesOrderDetails.IsAdditionalChargeApplyType,
                                     }).Where(x => x.SalesOrdersId == SalesOrderId && x.IsActive == true).ToList();

                        if (Sales != null)
                        {
                            var SalesOrder = Sales.FirstOrDefault(x => x.SalesOrdersId == SalesOrderId);
                            if (SalesOrder != null)
                            {
                                SalesOrdersVM.SalesOrdersId = SalesOrder.SalesOrdersId;
                                SalesOrdersVM.SalesOrderNumber = SalesOrder.SalesOrderNumber;
                                SalesOrdersVM.DateOrdered = SalesOrder.DateOrdered;
                                SalesOrdersVM.EstimatedDeliveryDate = SalesOrder.EstimatedDeliveryDate;
                                SalesOrdersVM.CustomerId = SalesOrder.CustomerId;
                                SalesOrdersVM.CustomerPurchesOrderNumber = SalesOrder.CustomerPurchesOrderNumber;
                                SalesOrdersVM.Remarks = SalesOrder.Remarks;
                                SalesOrdersVM.SalesOrderTypeId = SalesOrder.SalesOrderTypeId;
                                SalesOrdersVM.CreditTermId = SalesOrder.CreditTermId;
                                SalesOrdersVM.ShipmentMethodId = SalesOrder.ShipmentMethodId;
                                SalesOrdersVM.CurrencyId = SalesOrder.CurrencyId;
                                SalesOrdersVM.SalesOrderRepId = SalesOrder.SalesOrderRepId;
                                SalesOrdersVM.PaymentStatus = SalesOrder.PaymentStatus;
                                SalesOrdersVM.ShipmentStatus = SalesOrder.ShipmentStatus;

                                SalesOrderDetailsVM.SalesOrderDetailsId = SalesOrder.SalesOrderDetailsId;
                                SalesOrderDetailsVM.SalesOrdersId = SalesOrder.SalesOrdersId;
                                SalesOrderDetailsVM.TotalQTY = SalesOrder.TotalQTY;
                                SalesOrderDetailsVM.Total = SalesOrder.Total;
                                SalesOrderDetailsVM.FinalTotal = SalesOrder.FinalTotal;
                                SalesOrderDetailsVM.TaxInclude = SalesOrder.TaxInclude;
                                SalesOrderDetailsVM.FinalTaxTotal = SalesOrder.FinalTaxTotal;
                                SalesOrderDetailsVM.AdditionalChargeAmount = SalesOrder.AdditionalChargeAmount;
                                SalesOrderDetailsVM.IsAdditionalChargeApply = SalesOrder.IsAdditionalChargeApply;
                                SalesOrderDetailsVM.IsAdditionalChargeApplyType = SalesOrder.IsAdditionalChargeApplyType;
                                if (SalesOrderDetailsVM.IsAdditionalChargeApply.HasValue && SalesOrderDetailsVM.IsAdditionalChargeApply.Value)
                                {
                                    if (SalesOrder.IsAdditionalChargeApplyType == "All")
                                    {
                                        List<SalesOrderAdditionalChargeForAllVM> AdditionalChargeForAll = new List<SalesOrderAdditionalChargeForAllVM>();
                                        var AdditionalChargeForAllList = _DbContext.SalesOrderAdditionalChargeForAll.Where(x => x.SalesOrdersId == SalesOrder.SalesOrdersId).ToList();
                                        if (AdditionalChargeForAllList != null && AdditionalChargeForAllList.Count() > 0)
                                        {
                                            foreach (var item in AdditionalChargeForAllList)
                                            {
                                                SalesOrderAdditionalChargeForAllVM all = new SalesOrderAdditionalChargeForAllVM()
                                                {
                                                    SalesOrdersId = item.SalesOrdersId.HasValue ? item.SalesOrdersId.Value : 0,
                                                    AdditionalChargeId = item.AdditionalChargeId,
                                                    AdditionalForAllId = item.AdditionalChargeForAllId,
                                                    TaxId = item.TaxId,
                                                };
                                                AdditionalChargeForAll.Add(all);
                                            }
                                            SalesOrderDetailsVM.AdditionalChargeForAll = AdditionalChargeForAll;
                                        }
                                    }
                                    else if (SalesOrder.IsAdditionalChargeApplyType == "Product")
                                    {
                                        List<SalesOrderAdditionalChargeForProductVM> SalesOrderAdditionalChargeForProductVM = new List<SalesOrderAdditionalChargeForProductVM>();
                                        var AdditionalChargeForProductList = _DbContext.SalesOrderAdditionalChargeForProduct.Where(x => x.SalesOrdersId == SalesOrder.SalesOrdersId).ToList();
                                        if (AdditionalChargeForProductList != null && AdditionalChargeForProductList.Count() > 0)
                                        {
                                            foreach (var item in AdditionalChargeForProductList)
                                            {
                                                SalesOrderAdditionalChargeForProductVM product = new SalesOrderAdditionalChargeForProductVM()
                                                {
                                                    AdditionalChargeForProductId = item.AdditionalChargeForProductId,
                                                    AdditionalChargeId = item.AdditionalChargeId,
                                                    IsTaxble = item.IsTaxble,
                                                    ProductId = item.ProductId,
                                                    SalesOrdersId = item.SalesOrdersId,
                                                    TaxId = item.TaxId,
                                                };
                                                SalesOrderAdditionalChargeForProductVM.Add(product);
                                            }
                                            SalesOrderDetailsVM.AdditionalChargeForProduct = SalesOrderAdditionalChargeForProductVM;
                                        }
                                    }
                                }

                                var SalesOrderProductlist = _DbContext.SalesOrderItems.Where(x => x.SalesOrdersId == SalesOrder.SalesOrdersId).ToList();
                                if (SalesOrderProductlist != null && SalesOrderProductlist.Count() > 0)
                                {
                                    foreach (var item in SalesOrderProductlist)
                                    {
                                        SalesOrderItemsVM salesOrderItemsVM = new SalesOrderItemsVM()
                                        {
                                            OrderItemsId = item.OrderItemsId,
                                            SalesOrdersId = item.SalesOrdersId,
                                            ProductId = item.ProductId,
                                            Unit = item.Unit,
                                            UnitPrice = item.UnitPrice,
                                            QTY = item.QTY,
                                            DiscountType = item.DiscountType,
                                            Discount = item.Discount,
                                            IsTaxble = item.IsTaxble,
                                            TaxId = item.TaxId,
                                            TaxTotal = item.TaxTotal,
                                            Total = item.Total,
                                        };
                                        SalesOrderItemsList.Add(salesOrderItemsVM);
                                    }
                                }
                            }
                        }
                        output.SalesOrdersVM = SalesOrdersVM;
                        output.SalesOrderDetailsVM = SalesOrderDetailsVM;
                        output.SalesOrderItemsList = SalesOrderItemsList;
                    });
                }
                else
                    throw new Exception("Sales Order Id Not null");
            }
            catch (Exception e)
            {
                throw e;
            }
            return output;
        }
    }
}
