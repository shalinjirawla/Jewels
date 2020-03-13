using Inventory.Application.Interface.PurchaseOrder;
using Inventory.Application.Interface.SalesOrder;
using Inventory.Application.ViewModel.PurchaseOrder;
using Inventory.Application.ViewModel.SalesOrder;
using Inventory.Core.Models.PurchaseOrder;
using Inventory.Core.Models.SalesOrder;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.SalesOrderServices
{
    public class PurchaseOrderServices : IPurchaseOrder
    {
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status = false;
        public string Message = "";
        public PurchaseOrderServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task DeletePurchaseOrder(long PurchaseOrderId)
        {
            try
            {
                if (PurchaseOrderId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Purchase = _DbContext.PurchaseOrders.FirstOrDefault(x => x.PurchaseOrdersId == PurchaseOrderId);
                        if (Purchase != null)
                        {
                            Purchase.IsActive = false;
                            _DbContext.PurchaseOrders.Update(Purchase);
                            _DbContext.SaveChanges();
                        }
                    });

                }
                else
                    throw new Exception("Purchase Order Id not null");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PurchaseOrderMergeVM> GetPurchaseOrderDetails(long PurchaseOrderId)
        {
            PurchaseOrderMergeVM output = new PurchaseOrderMergeVM();
            PurchaseOrdersVM PurchaseOrdersVM = new PurchaseOrdersVM();
            PurchaseOrderDetailsVM PurchaseOrderDetailsVM = new PurchaseOrderDetailsVM();
            List<PurchaseOrderItemsVM> PurchaseOrderItemsList = new List<PurchaseOrderItemsVM>();
            try
            {
                if (PurchaseOrderId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Purchase = (from purchase in _DbContext.PurchaseOrders.ToList()
                                        join purchaseOrderDetails in _DbContext.PurchaseOrderDetails.ToList()
                                        on purchase.PurchaseOrdersId equals purchaseOrderDetails.PurchaseOrdersId
                                        //join salesOrderItem in _DbContext.SalesOrderItems.ToList()
                                        //on sales.SalesOrdersId equals salesOrderItem.SalesOrdersId
                                        select new GetPurchaseOrderVM
                                        {
                                            //purchase
                                            PurchaseOrderId = purchase.PurchaseOrdersId,
                                            PurchaseOrderNumber = purchase.PurchaseOrderNumber,
                                            DateOrdered = purchase.DateOrdered,
                                            EstimatedDeliveryDate = purchase.EstimatedDeliveryDate.HasValue ? purchase.EstimatedDeliveryDate : null,
                                            SupplierId = purchase.SupplierId,
                                            ReferenceNumber = purchase.ReferenceNumber,
                                            Remarks = purchase.Remarks,
                                            PaymentTermId = purchase.PaymentTermId.HasValue ? purchase.PaymentTermId : null,
                                            CreditTermId = purchase.CreditTermId.HasValue ? purchase.CreditTermId : null,
                                            ShipmentMethodId = purchase.ShipmentMethodId.HasValue ? purchase.ShipmentMethodId : null,
                                            CurrencyId = purchase.CurrencyId.HasValue ? purchase.CurrencyId : null,
                                            PaymentStatus = purchase.PaymentStatus,
                                            ReceiveStatus = purchase.PaymentStatus,
                                            IsActive = purchase.IsActive,
                                            ExchangeRate = purchase.ExchangeRate,
                                            // Purchase details
                                            PurchaseOrderDetailsId = purchaseOrderDetails.PurchaseOrderDetailsId,
                                            TotalQTY = purchaseOrderDetails.TotalQTY,
                                            Total = purchaseOrderDetails.Total ?? 0,
                                            FinalTotal = purchaseOrderDetails.FinalTotal,
                                            TaxInclude = purchaseOrderDetails.TaxInclude,
                                            FinalTaxTotal = purchaseOrderDetails.FinalTaxTotal,
                                            AdditionalChargeAmount = purchaseOrderDetails.AdditionalChargeAmount,
                                            IsAdditionalChargeApply = purchaseOrderDetails.IsAdditionalChargeApply,
                                            IsAdditionalChargeApplyType = purchaseOrderDetails.IsAdditionalChargeApplyType,
                                        }).Where(x => x.PurchaseOrderId == PurchaseOrderId && x.IsActive == true).ToList();

                        if (Purchase != null)
                        {
                            var PurchaseOrder = Purchase.FirstOrDefault(x => x.PurchaseOrderId == PurchaseOrderId);
                            if (PurchaseOrder != null)
                            {
                                PurchaseOrdersVM.PurchaseOrdersId = PurchaseOrder.PurchaseOrderId;
                                PurchaseOrdersVM.PurchaseOrderNumber = PurchaseOrder.PurchaseOrderNumber;
                                PurchaseOrdersVM.DateOrdered = PurchaseOrder.DateOrdered;
                                PurchaseOrdersVM.EstimatedDeliveryDate = PurchaseOrder.EstimatedDeliveryDate.HasValue ? PurchaseOrder.EstimatedDeliveryDate : null;
                                PurchaseOrdersVM.SupplierId = PurchaseOrder.SupplierId;
                                PurchaseOrdersVM.ReferenceNumber = PurchaseOrder.ReferenceNumber;
                                PurchaseOrdersVM.Remarks = PurchaseOrder.Remarks;
                                PurchaseOrdersVM.PaymentTermId = PurchaseOrder.PaymentTermId.HasValue ? PurchaseOrder.PaymentTermId : null;
                                PurchaseOrdersVM.CreditTermId = PurchaseOrder.CreditTermId.HasValue ? PurchaseOrder.CreditTermId : null;
                                PurchaseOrdersVM.ShipmentMethodId = PurchaseOrder.ShipmentMethodId.HasValue ? PurchaseOrder.ShipmentMethodId : null;
                                PurchaseOrdersVM.CurrencyId = PurchaseOrder.CurrencyId.HasValue ? PurchaseOrder.CurrencyId : null;
                                PurchaseOrdersVM.PaymentStatus = PurchaseOrder.PaymentStatus;
                                PurchaseOrdersVM.RecieveStatus = PurchaseOrder.PaymentStatus;
                                PurchaseOrdersVM.ExchangeRate = PurchaseOrder.ExchangeRate;

                                PurchaseOrderDetailsVM.PurchaseOrderDetailsId = PurchaseOrder.PurchaseOrderDetailsId;
                                PurchaseOrderDetailsVM.TotalQTY = PurchaseOrder.TotalQTY;
                                PurchaseOrderDetailsVM.Total = PurchaseOrder.Total ?? 0;
                                PurchaseOrderDetailsVM.FinalTotal = PurchaseOrder.FinalTotal;
                                PurchaseOrderDetailsVM.TaxInclude = PurchaseOrder.TaxInclude;
                                PurchaseOrderDetailsVM.FinalTaxTotal = PurchaseOrder.FinalTaxTotal;
                                PurchaseOrderDetailsVM.AdditionalChargeAmount = PurchaseOrder.AdditionalChargeAmount;
                                PurchaseOrderDetailsVM.IsAdditionalChargeApply = PurchaseOrder.IsAdditionalChargeApply;
                                PurchaseOrderDetailsVM.IsAdditionalChargeApplyType = PurchaseOrder.IsAdditionalChargeApplyType;
                                if (PurchaseOrderDetailsVM.IsAdditionalChargeApply.HasValue && PurchaseOrderDetailsVM.IsAdditionalChargeApply.Value)
                                {
                                    if (PurchaseOrderDetailsVM.IsAdditionalChargeApplyType == "All")
                                    {
                                        List<PurchaseOrderAdditionalChargeForAllVM> AdditionalChargeForAll = new List<PurchaseOrderAdditionalChargeForAllVM>();
                                        var AdditionalChargeForAllList = _DbContext.PurchaseOrderAdditionalChargeForAll.Where(x => x.PurchaseOrdersId == PurchaseOrdersVM.PurchaseOrdersId).ToList();
                                        if (AdditionalChargeForAllList != null && AdditionalChargeForAllList.Count() > 0)
                                        {
                                            foreach (var item in AdditionalChargeForAllList)
                                            {
                                                PurchaseOrderAdditionalChargeForAllVM all = new PurchaseOrderAdditionalChargeForAllVM()
                                                {
                                                    PurchaseOrdersId = item.PurchaseOrdersId.HasValue ? item.PurchaseOrdersId.Value : 0,
                                                    AdditionalChargeId = item.AdditionalChargeId,
                                                    AdditionalForAllId = item.AdditionalChargeForAllId,
                                                    TaxId = item.TaxId,
                                                };
                                                AdditionalChargeForAll.Add(all);
                                            }
                                            PurchaseOrderDetailsVM.AdditionalChargeForAll = AdditionalChargeForAll;
                                        }
                                    }
                                    else if (PurchaseOrderDetailsVM.IsAdditionalChargeApplyType == "Product")
                                    {
                                        List<PurchaseOrderAdditionalChargeForProductVM> PurchaseOrderAdditionalChargeForProductVM = new List<PurchaseOrderAdditionalChargeForProductVM>();
                                        var AdditionalChargeForProductList = _DbContext.PurchaseOrderAdditionalChargeForProduct.Where(x => x.PurchaseOrdersId == PurchaseOrdersVM.PurchaseOrdersId).ToList();
                                        if (AdditionalChargeForProductList != null && AdditionalChargeForProductList.Count() > 0)
                                        {
                                            foreach (var item in AdditionalChargeForProductList)
                                            {
                                                PurchaseOrderAdditionalChargeForProductVM product = new PurchaseOrderAdditionalChargeForProductVM()
                                                {
                                                    AdditionalChargeForProductId = item.AdditionalChargeForProductId,
                                                    AdditionalChargeId = item.AdditionalChargeId,
                                                    IsTaxble = item.IsTaxble,
                                                    ProductId = item.ProductId,
                                                    PurchaseOrdersId = item.PurchaseOrdersId ?? 0,
                                                    TaxId = item.TaxId,
                                                };
                                                PurchaseOrderAdditionalChargeForProductVM.Add(product);
                                            }
                                            PurchaseOrderDetailsVM.AdditionalChargeForProduct = PurchaseOrderAdditionalChargeForProductVM;
                                        }
                                    }
                                }

                                var PurchaseOrderProductlist = _DbContext.PurchaseOrderItems.Where(x => x.PurchaseOrdersId == PurchaseOrdersVM.PurchaseOrdersId).ToList();
                                if (PurchaseOrderProductlist != null && PurchaseOrderProductlist.Count() > 0)
                                {
                                    foreach (var item in PurchaseOrderProductlist)
                                    {
                                        PurchaseOrderItemsVM purchaseOrderItemsVM = new PurchaseOrderItemsVM()
                                        {
                                            OrderItemsId = item.OrderItemsId,
                                            PurchaseOrdersId = item.PurchaseOrdersId,
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
                                        PurchaseOrderItemsList.Add(purchaseOrderItemsVM);
                                    }
                                }
                            }
                        }
                        output.PurchaseOrdersVM = PurchaseOrdersVM;
                        output.PurchaseOrderDetailsVM = PurchaseOrderDetailsVM;
                        output.PurchaseOrderItemsVM = PurchaseOrderItemsList;
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

        public async Task<List<PurchaseOrderListVM>> PurchaseOrderList(long TenantId)
        {
            List<PurchaseOrderListVM> List = new List<PurchaseOrderListVM>();
            try
            {
                if (TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Purchase = (from purchase in _DbContext.PurchaseOrders.ToList()
                                        join purchaseOrderDetails in _DbContext.PurchaseOrderDetails.ToList()
                                        on purchase.PurchaseOrdersId equals purchaseOrderDetails.PurchaseOrdersId
                                        select new PurchaseOrderListVM
                                        {
                                            PurchaseOrderId = purchase.PurchaseOrdersId,
                                            PurchaseOrderNumber = purchase.PurchaseOrderNumber,
                                            ReferenceNumber = purchase.ReferenceNumber,
                                            SupplierId = purchase.SupplierId,
                                            SupplierName = GetSupplierName(purchase.SupplierId),
                                            PaymentStatus = purchase.PaymentStatus,
                                            ReceiveStatus = purchase.ReceiveStatus,
                                            Total = purchaseOrderDetails.FinalTotal ?? 0,
                                            DateOrdered = purchase.DateOrdered,
                                            EstimatedDeliveryDate = purchase.EstimatedDeliveryDate,
                                            TenantId = purchase.TenantsId.HasValue ? purchase.TenantsId.Value : 0,
                                            IsActive = purchase.IsActive,
                                        }).Where(x => x.TenantId == TenantId && x.IsActive == true).ToList();

                        if (Purchase != null && Purchase.Count() > 0)
                        {
                            foreach (var item in Purchase)
                            {
                                PurchaseOrderListVM purchaseOrder = new PurchaseOrderListVM()
                                {
                                    PurchaseOrderId = item.PurchaseOrderId,
                                    PurchaseOrderNumber = item.PurchaseOrderNumber,
                                    ReferenceNumber = item.ReferenceNumber,
                                    SupplierId = item.SupplierId,
                                    SupplierName = GetSupplierName(item.SupplierId),
                                    PaymentStatus = item.PaymentStatus,
                                    ReceiveStatus = item.ReceiveStatus,
                                    Total = item.Total,
                                    DateOrdered = item.DateOrdered,
                                    EstimatedDeliveryDate = item.EstimatedDeliveryDate,
                                    TenantId = item.TenantId,
                                    IsActive = item.IsActive,
                                };
                                List.Add(purchaseOrder);
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
        public string GetSupplierName(long SupplierId)
        {
            string Name = "";
            if (SupplierId != 0)
            {

                var Supplier = _DbContext.Suppliers.FirstOrDefault(x => x.SupplierId == SupplierId);
                if (Supplier != null)
                {
                    Name = Supplier.CompanyName + " (" + Supplier.SupplierCode + ")";
                }

            }
            return Name;
        }
        public async Task<long> SavePurchaseOrder(PurchaseOrderMergeVM input, string UserId, long TenantId)
        {

            try
            {

                PurchaseOrdersVM purchaseOrdersVM = new PurchaseOrdersVM();
                purchaseOrdersVM = input.PurchaseOrdersVM;

                PurchaseOrderDetailsVM purchaseOrderDetailsVM = new PurchaseOrderDetailsVM();
                purchaseOrderDetailsVM = input.PurchaseOrderDetailsVM;

                List<PurchaseOrderItemsVM> PurchaseOrderItemsList = new List<PurchaseOrderItemsVM>();
                PurchaseOrderItemsList = input.PurchaseOrderItemsVM;
                await Task.Run(async () =>
                {
                    //Purchase Orders save start
                    if (purchaseOrdersVM.PurchaseOrdersId == 0)
                    {
                        PurchaseOrders purchaseOrders = new PurchaseOrders()
                        {
                            PurchaseOrderNumber = "PO-" + PurchaseOrderNumberRandomString(),
                            DateOrdered = DateTime.Now,
                            EstimatedDeliveryDate = purchaseOrdersVM.EstimatedDeliveryDate.HasValue ? purchaseOrdersVM.EstimatedDeliveryDate : null,
                            SupplierId = purchaseOrdersVM.SupplierId,
                            ReferenceNumber = purchaseOrdersVM.ReferenceNumber,
                            Remarks = purchaseOrdersVM.Remarks,
                            PaymentTermId = purchaseOrdersVM.PaymentTermId.HasValue ? purchaseOrdersVM.PaymentTermId : null,
                            CreditTermId = purchaseOrdersVM.CreditTermId.HasValue ? purchaseOrdersVM.CreditTermId : null,
                            ShipmentMethodId = purchaseOrdersVM.ShipmentMethodId.HasValue ? purchaseOrdersVM.ShipmentMethodId : null,
                            CurrencyId = purchaseOrdersVM.CurrencyId.HasValue ? purchaseOrdersVM.CurrencyId : null,
                            PaymentStatus = 0,
                            ReceiveStatus = 0,
                            CreationTime = DateTime.Now,
                            TenantsId = TenantId,
                            CreatorUserId = UserId,
                            IsActive = true,
                        };
                        if (purchaseOrders.PaymentTermId == 0)
                            purchaseOrders.PaymentTermId = null;
                        if (purchaseOrders.CreditTermId == 0)
                            purchaseOrders.CreditTermId = null;
                        if (purchaseOrders.ShipmentMethodId == 0)
                            purchaseOrders.ShipmentMethodId = null;
                        if (purchaseOrders.CurrencyId == 0)
                            purchaseOrders.CurrencyId = null;
                        _DbContext.PurchaseOrders.Add(purchaseOrders);
                        _DbContext.SaveChanges();
                        input.PurchaseOrdersVM.PurchaseOrdersId = purchaseOrders.PurchaseOrdersId;
                        //Purchase Orders save end
                        //Purchase Orders details save start
                        PurchaseOrderDetails purchaseOrderDetails = new PurchaseOrderDetails()
                        {
                            PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId,
                            TotalQTY = purchaseOrderDetailsVM.TotalQTY,
                            Total = purchaseOrderDetailsVM.Total,
                            FinalTotal = purchaseOrderDetailsVM.FinalTotal,
                            TaxInclude = purchaseOrderDetailsVM.TaxInclude,
                            FinalTaxTotal = purchaseOrderDetailsVM.FinalTaxTotal,
                            AdditionalChargeAmount = purchaseOrderDetailsVM.AdditionalChargeAmount,
                            IsAdditionalChargeApply = purchaseOrderDetailsVM.IsAdditionalChargeApply,
                            IsAdditionalChargeApplyType = purchaseOrderDetailsVM.IsAdditionalChargeApplyType
                        };
                        _DbContext.PurchaseOrderDetails.Add(purchaseOrderDetails);
                        _DbContext.SaveChanges();

                        //Sales Orders details save end
                        // Sales Order Items save start
                        if (PurchaseOrderItemsList != null && PurchaseOrderItemsList.Count > 0)
                        {
                            foreach (var item in PurchaseOrderItemsList)
                            {
                                if (item.TaxId.Value == 0)
                                    item.TaxId = null;
                                PurchaseOrderItems purchaseOrderItems = new PurchaseOrderItems()
                                {
                                    PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId,
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
                                _DbContext.PurchaseOrderItems.Add(purchaseOrderItems);
                                _DbContext.SaveChanges();
                            }
                        }

                        // Sales Order Items save end
                        if (purchaseOrderDetailsVM.IsAdditionalChargeApplyType == "All" &&
                        purchaseOrderDetailsVM.AdditionalChargeForAll != null && purchaseOrderDetailsVM.AdditionalChargeForAll.Count > 0)
                        {
                            foreach (var item in purchaseOrderDetailsVM.AdditionalChargeForAll)
                            {
                                if (item != null)
                                {
                                    item.PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId;
                                    await SaveAdditonalChargeForAll(item);
                                }
                            }
                        }
                        else if (purchaseOrderDetailsVM.IsAdditionalChargeApplyType == "Product" &&
                        purchaseOrderDetailsVM.AdditionalChargeForProduct != null && purchaseOrderDetailsVM.AdditionalChargeForProduct.Count > 0)
                        {
                            foreach (var item in purchaseOrderDetailsVM.AdditionalChargeForProduct)
                            {
                                if (item != null)
                                {
                                    item.PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId;
                                    await SaveAdditonalChargeForProduct(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        var purchaseOrders = _DbContext.PurchaseOrders.FirstOrDefault(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId);
                        if (purchaseOrders != null)
                        {
                            purchaseOrders.EstimatedDeliveryDate = purchaseOrdersVM.EstimatedDeliveryDate.HasValue ? purchaseOrdersVM.EstimatedDeliveryDate : null;
                            purchaseOrders.SupplierId = purchaseOrdersVM.SupplierId;
                            purchaseOrders.PurchaseOrderNumber = purchaseOrdersVM.PurchaseOrderNumber;
                            purchaseOrders.Remarks = purchaseOrdersVM.Remarks;
                            purchaseOrders.ReferenceNumber = purchaseOrdersVM.ReferenceNumber;
                            purchaseOrders.PaymentTermId = purchaseOrdersVM.PaymentTermId;
                            purchaseOrders.CreditTermId = purchaseOrdersVM.CreditTermId.HasValue ? purchaseOrdersVM.CreditTermId : null;
                            purchaseOrders.ShipmentMethodId = purchaseOrdersVM.ShipmentMethodId.HasValue ? purchaseOrdersVM.ShipmentMethodId : null;
                            purchaseOrders.ExchangeRate = purchaseOrdersVM.ExchangeRate.HasValue ? purchaseOrdersVM.ExchangeRate : null;
                            purchaseOrders.CurrencyId = purchaseOrdersVM.CurrencyId.HasValue ? purchaseOrdersVM.CurrencyId : null;
                            purchaseOrders.LastModificationTime = DateTime.Now;
                            purchaseOrders.LastModifierUserId = UserId;
                            if (purchaseOrders.PaymentTermId == 0)
                                purchaseOrders.PaymentTermId = null;
                            if (purchaseOrders.CreditTermId == 0)
                                purchaseOrders.CreditTermId = null;
                            if (purchaseOrders.ShipmentMethodId == 0)
                                purchaseOrders.ShipmentMethodId = null;
                            if (purchaseOrders.CurrencyId == 0)
                                purchaseOrders.CurrencyId = null;
                            _DbContext.PurchaseOrders.Update(purchaseOrders);
                            _DbContext.SaveChanges();
                        }
                        var purchaseOrderDetails = _DbContext.PurchaseOrderDetails.FirstOrDefault(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId);
                        if (purchaseOrderDetails != null)
                        {
                            purchaseOrderDetails.TotalQTY = purchaseOrderDetailsVM.TotalQTY;
                            purchaseOrderDetails.Total = purchaseOrderDetailsVM.Total;
                            purchaseOrderDetails.FinalTotal = purchaseOrderDetailsVM.FinalTotal;
                            purchaseOrderDetails.TaxInclude = purchaseOrderDetailsVM.TaxInclude;
                            purchaseOrderDetails.FinalTaxTotal = purchaseOrderDetailsVM.FinalTaxTotal;
                            purchaseOrderDetails.AdditionalChargeAmount = purchaseOrderDetailsVM.AdditionalChargeAmount;
                            purchaseOrderDetails.IsAdditionalChargeApply = purchaseOrderDetailsVM.IsAdditionalChargeApply;
                            purchaseOrderDetails.IsAdditionalChargeApplyType = purchaseOrderDetailsVM.IsAdditionalChargeApplyType;
                            _DbContext.PurchaseOrderDetails.Update(purchaseOrderDetails);
                            _DbContext.SaveChanges();
                        }
                        if (PurchaseOrderItemsList != null && PurchaseOrderItemsList.Count > 0)
                        {
                            var DeletePurchaseOrderItems = _DbContext.PurchaseOrderItems.Where(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId).ToList();
                            if (DeletePurchaseOrderItems != null && DeletePurchaseOrderItems.Count() > 0)
                            {
                                _DbContext.PurchaseOrderItems.RemoveRange(DeletePurchaseOrderItems);
                                _DbContext.SaveChanges();
                            }
                            var checkisdeleted = _DbContext.PurchaseOrderItems.Count(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId);
                            if (checkisdeleted == 0)
                            {
                                foreach (var item in PurchaseOrderItemsList)
                                {
                                    if (item.TaxId.Value == 0)
                                        item.TaxId = null;
                                    PurchaseOrderItems purchaseOrderItems = new PurchaseOrderItems()
                                    {
                                        PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId,
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
                                    _DbContext.PurchaseOrderItems.Add(purchaseOrderItems);
                                    _DbContext.SaveChanges();
                                }
                            }
                        }
                        if (purchaseOrderDetailsVM.IsAdditionalChargeApply.HasValue && purchaseOrderDetailsVM.IsAdditionalChargeApply.Value)
                        {
                            if (purchaseOrderDetailsVM.IsAdditionalChargeApplyType == "All")
                            {
                                var deleteAdditionalforAll = _DbContext.PurchaseOrderAdditionalChargeForAll.Where(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId).ToList();
                                if (deleteAdditionalforAll != null && deleteAdditionalforAll.Count() > 0)
                                {
                                    _DbContext.PurchaseOrderAdditionalChargeForAll.RemoveRange(deleteAdditionalforAll);
                                    _DbContext.SaveChanges();
                                }
                                var checkIsExistAll = _DbContext.PurchaseOrderAdditionalChargeForAll.Count(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId);
                                if (checkIsExistAll == 0)
                                {
                                    foreach (var item in purchaseOrderDetailsVM.AdditionalChargeForAll)
                                    {
                                        if (item != null)
                                        {
                                            item.PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId;
                                            await SaveAdditonalChargeForAll(item);
                                        }
                                    }
                                }
                            }
                            else if (purchaseOrderDetailsVM.IsAdditionalChargeApplyType == "Product")
                            {
                                var deleteAdditionalforProduct = _DbContext.PurchaseOrderAdditionalChargeForProduct.Where(x => x.PurchaseOrdersId == purchaseOrdersVM.PurchaseOrdersId).ToList();
                                if (deleteAdditionalforProduct != null && deleteAdditionalforProduct.Count() > 0)
                                {
                                    _DbContext.PurchaseOrderAdditionalChargeForProduct.RemoveRange(deleteAdditionalforProduct);
                                    _DbContext.SaveChanges();
                                }
                                var checkIsExistProduct = _DbContext.SalesOrderAdditionalChargeForProduct.Count(x => x.SalesOrdersId == purchaseOrdersVM.PurchaseOrdersId);
                                if (checkIsExistProduct == 0)
                                {
                                    foreach (var item in purchaseOrderDetailsVM.AdditionalChargeForProduct)
                                    {
                                        if (item != null)
                                        {
                                            item.PurchaseOrdersId = input.PurchaseOrdersVM.PurchaseOrdersId;
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
            return input.PurchaseOrdersVM.PurchaseOrdersId;
        }
        public async Task SaveAdditonalChargeForAll(PurchaseOrderAdditionalChargeForAllVM input)
        {
            try
            {
                await Task.Run(() =>
                {
                    PurchaseOrderAdditionalChargeForAll chargeForAll = new PurchaseOrderAdditionalChargeForAll()
                    {
                        PurchaseOrdersId = input.PurchaseOrdersId,
                        AdditionalChargeId = input.AdditionalChargeId.HasValue ? input.AdditionalChargeId : null,
                        TaxId = input.TaxId.HasValue ? input.TaxId : null,
                    };
                    _DbContext.PurchaseOrderAdditionalChargeForAll.Add(chargeForAll);
                    _DbContext.SaveChanges();
                });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task SaveAdditonalChargeForProduct(PurchaseOrderAdditionalChargeForProductVM input)
        {
            try
            {
                await Task.Run(() =>
                {

                    PurchaseOrderAdditionalChargeForProduct chargeForProduct = new PurchaseOrderAdditionalChargeForProduct()
                    {
                        PurchaseOrdersId = input.PurchaseOrdersId,
                        ProductId = input.ProductId.HasValue ? input.ProductId : null,
                        AdditionalChargeId = input.AdditionalChargeId.HasValue ? input.AdditionalChargeId : null,
                        TaxId = input.TaxId.HasValue ? input.TaxId : null,
                        IsTaxble = input.IsTaxble,
                    };
                    if (chargeForProduct.TaxId == 0)
                        chargeForProduct.TaxId = null;
                    _DbContext.PurchaseOrderAdditionalChargeForProduct.Add(chargeForProduct);
                    _DbContext.SaveChanges();
                });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static string PurchaseOrderNumberRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<List<GetPurchaseOrderListIdBySuppliersVM>> GetPucrahseOrderListIdBySupplier(long SupplierId)
        {
            List<GetPurchaseOrderListIdBySuppliersVM> list = new List<GetPurchaseOrderListIdBySuppliersVM>();
            try
            {
                if (SupplierId != 0)
                {
                    await Task.Run(() =>
                    {
                        var purchaseOrders = _DbContext.PurchaseOrders.Where(x => x.SupplierId == SupplierId && x.IsActive == true).ToList();
                        if (purchaseOrders != null && purchaseOrders.Count() > 0)
                        {
                            foreach (var item in purchaseOrders)
                            {
                                GetPurchaseOrderListIdBySuppliersVM vm = new GetPurchaseOrderListIdBySuppliersVM()
                                {
                                    PurchaseOrderId = item.PurchaseOrdersId,
                                    SupplierId = item.SupplierId,
                                    Code = item.PurchaseOrderNumber,
                                };
                                list.Add(vm);
                            }
                        }
                    });
                }
                else
                    throw new Exception("Suppliers Not Null");
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public async Task<List<ProductListIdByPurchaseOrder>> GetProductListIdByPurchasOrder(long PurchaseOrderId)
        {
            List<ProductListIdByPurchaseOrder> list = new List<ProductListIdByPurchaseOrder>();
            try
            {
                if (PurchaseOrderId != 0)
                {
                    await Task.Run(() =>
                    {
                        var purchaseorderdata = (from purchaseorder in _DbContext.PurchaseOrders.ToList()
                                                 join purchaseorderitem in _DbContext.PurchaseOrderItems.ToList()
                                                 on purchaseorder.PurchaseOrdersId equals purchaseorderitem.PurchaseOrdersId
                                                 join product in _DbContext.Product.ToList()
                                                 on purchaseorderitem.ProductId equals product.ProductId
                                                 select new ProductListIdByPurchaseOrder()
                                                 {
                                                     PurchaseOrderId = purchaseorderitem.PurchaseOrdersId,
                                                     ProductId = product.ProductId,
                                                     ProductCode = "PDO-" + product.ProductId,
                                                     ProductName = product.Name,
                                                     UOM = purchaseorderitem.Unit.ToString(),
                                                     Price = purchaseorderitem.Total,
                                                     QTY = purchaseorderitem.QTY,
                                                     IsActive = purchaseorder.IsActive,
                                                 }).Where(x => x.IsActive == true && x.PurchaseOrderId == PurchaseOrderId).ToList();
                        if(purchaseorderdata!=null)
                        {
                            foreach (var item in purchaseorderdata.ToList())
                            {
                                ProductListIdByPurchaseOrder vm = new ProductListIdByPurchaseOrder()
                                {
                                    PurchaseOrderId = item.PurchaseOrderId,
                                    ProductId = item.ProductId,
                                    ProductCode = item.ProductCode,
                                    ProductName = item.ProductName,
                                    UOM = item.UOM,
                                    Price = item.Price,
                                    QTY = item.QTY,
                                    IsActive = item.IsActive,
                                    UniquIndex = (int)item.PurchaseOrderId + (int)item.ProductId + (int)item.Price + (int)item.QTY,
                                };
                                list.Add(vm);
                            }
                        }
                    });
                }
                else
                    throw new Exception("Purchase Order Id Not Null");
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
    }
}
