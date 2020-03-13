using Inventory.Application.Interface.PurchaseOrder;
using Inventory.Application.ViewModel.PurchaseOrder;
using Inventory.Core.Models.PurchaseOrder;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.PurchaseOrderServices
{
    public class ReceiveNotesServices : IReceiveNotes
    {
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status = false;
        public string Message = "";
        public ReceiveNotesServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task DeleteReceiveNotes(long ReceiveNotesId)
        {
            try
            {
                if (ReceiveNotesId != 0)
                {
                    await Task.Run(() =>
                    {
                        var receivenotes = _DbContext.ReceiveNotes.FirstOrDefault(x => x.ReceiveNoteId == ReceiveNotesId);
                        if (receivenotes != null)
                        {
                            receivenotes.IsActive = false;
                            _DbContext.ReceiveNotes.Update(receivenotes);
                            _DbContext.SaveChanges();
                        }
                    });
                }
                else
                    throw new Exception("ReceiveNotesId Not Zero");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ReceiveNotesVM> GetReceiveNotesDetails(long ReceiveNotesId)
        {
            ReceiveNotesVM receiveNotes = new ReceiveNotesVM();
            try
            {
                if (ReceiveNotesId != 0)
                {
                    await Task.Run(() =>
                    {
                        var receive = _DbContext.ReceiveNotes.FirstOrDefault(x => x.ReceiveNoteId == ReceiveNotesId);
                        if (receive != null)
                        {
                            receiveNotes.ReceiveNoteId = receive.ReceiveNoteId;
                            receiveNotes.ReceiveNoteNumber = receive.ReceiveNoteNumber;
                            receiveNotes.SupplierId = receive.SupplierId;
                            receiveNotes.ReceiveDate = receive.ReceiveDate;
                            receiveNotes.Status = receive.Status;
                            receiveNotes.Remarks = receive.Remarks;
                            var receiveitems = _DbContext.ReceiveNotesItems.Where(x => x.ReceiveNoteId == receive.ReceiveNoteId).ToList();
                            if (receiveitems != null && receiveitems.Count() > 0)
                            {
                                List<ReceiveNotesItemsVM> ProductList = new List<ReceiveNotesItemsVM>();
                                foreach (var item in receiveitems)
                                {
                                    ReceiveNotesItemsVM vm = new ReceiveNotesItemsVM()
                                    {
                                        ReceiveNoteItemId=item.ReceiveNoteItemId,
                                        ReceiveNoteId=item.ReceiveNoteId,
                                        ProductId=item.ProductId,
                                        PurchaseOrdersId=item.PurchaseOrdersId,
                                        ProductQTY=item.ProductQTY,
                                        WarehouseId=item.WarehouseId,
                                    };
                                    ProductList.Add(vm);
                                }
                                receiveNotes.ProductList = ProductList;
                            }
                        }
                    });
                }
                else
                    throw new Exception("ReceiveNotesId Not Zero");
            }
            catch (Exception e)
            {

                throw e;
            }
            return receiveNotes;
        }

        public async Task<List<ReceiveNotesVM>> ReceiveNotesList(long TenantId)
        {
            List<ReceiveNotesVM> list = new List<ReceiveNotesVM>();
            try
            {
                if (TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        var Notes = (from receiveNotes in _DbContext.ReceiveNotes.ToList()
                                     join supplier in _DbContext.Suppliers.ToList()
                                     on receiveNotes.SupplierId equals supplier.SupplierId
                                     select new ReceiveNotesVM()
                                     {
                                         ReceiveNoteId = receiveNotes.ReceiveNoteId,
                                         ReceiveNoteNumber = receiveNotes.ReceiveNoteNumber,
                                         SupplierId = receiveNotes.SupplierId,
                                         SupplierName = supplier.CompanyName + "[" + supplier.SupplierCode + "]",
                                         ReceiveDate = receiveNotes.ReceiveDate,
                                         Status = receiveNotes.Status,
                                         CreationTime = receiveNotes.CreationTime,
                                         TenantsId = receiveNotes.TenantsId,
                                         IsActive = receiveNotes.IsActive,
                                     }).Where(x => x.TenantsId == TenantId && x.IsActive == true).ToList();
                        if (Notes != null && Notes.Count() > 0)
                        {
                            foreach (var item in Notes)
                            {
                                ReceiveNotesVM vm = new ReceiveNotesVM()
                                {
                                    ReceiveNoteId = item.ReceiveNoteId,
                                    ReceiveNoteNumber = item.ReceiveNoteNumber,
                                    SupplierId = item.SupplierId,
                                    SupplierName = item.SupplierName,
                                    ReceiveDate = item.ReceiveDate,
                                    Status = item.Status,
                                    CreationTime = item.CreationTime,
                                    TenantsId = item.TenantsId,
                                };
                                list.Add(vm);
                            }
                        }
                    });
                }
                else
                    throw new Exception("Tenant Id Not Null");
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public async Task<long> SaveReceiveNotes(ReceiveNotesVM input, string UserId, long TenantId)
        {
            try
            {
                if (input != null)
                {
                    await Task.Run(async () =>
                    {
                        if (input.ReceiveNoteId == 0)
                        {
                            ReceiveNotes receiveNotes = new ReceiveNotes()
                            {
                                ReceiveNoteId = input.ReceiveNoteId,
                                ReceiveNoteNumber = "RN-" + ReceiveNotesNumberRandomString(),
                                SupplierId = input.SupplierId,
                                ReceiveDate = DateTime.Now,
                                Remarks = input.Remarks,
                                CreationTime = DateTime.Now,
                                TenantsId = TenantId,
                                CreatorUserId = UserId,
                                Status = 0,
                                IsActive=true,
                            };
                            _DbContext.ReceiveNotes.Add(receiveNotes);
                            _DbContext.SaveChanges();
                            input.ReceiveNoteId = receiveNotes.ReceiveNoteId;
                            if (input.ProductList != null && input.ProductList.Count() > 0)
                            {
                                foreach (var item in input.ProductList)
                                {
                                    item.ReceiveNoteId = receiveNotes.ReceiveNoteId;
                                    await SaveReceiveNoteProductItems(item);
                                }
                            }
                        }
                        else
                        {
                            var receiveNotes = _DbContext.ReceiveNotes.FirstOrDefault(x => x.ReceiveNoteId == input.ReceiveNoteId);
                            if (receiveNotes != null)
                            {
                                receiveNotes.SupplierId = input.SupplierId;
                                receiveNotes.Remarks = input.Remarks;
                                receiveNotes.LastModificationTime = DateTime.Now;
                                receiveNotes.LastModifierUserId = UserId;
                            };
                            _DbContext.ReceiveNotes.Update(receiveNotes);
                            _DbContext.SaveChanges();
                            var deleteReceiveNotes = _DbContext.ReceiveNotesItems.Where(x => x.ReceiveNoteId == input.ReceiveNoteId).ToList();
                            if (deleteReceiveNotes != null && deleteReceiveNotes.Count() > 0)
                            {
                                _DbContext.ReceiveNotesItems.RemoveRange(deleteReceiveNotes);
                                _DbContext.SaveChanges();
                            }
                            var IsexistProductItems = _DbContext.ReceiveNotesItems.Where(x => x.ReceiveNoteId == input.ReceiveNoteId).ToList();
                            if (IsexistProductItems == null || IsexistProductItems.Count() == 0)
                            {
                                if (input.ProductList != null && input.ProductList.Count() > 0)
                                {
                                    foreach (var item in input.ProductList)
                                    {
                                        item.ReceiveNoteId = receiveNotes.ReceiveNoteId;
                                        await SaveReceiveNoteProductItems(item);
                                    }
                                }
                            }
                        }
                    });
                }
                else
                    throw new Exception("Input Not Null Allow");
            }
            catch (Exception e)
            {
                throw e;
            }
            return input.ReceiveNoteId;
        }
        public async Task SaveReceiveNoteProductItems(ReceiveNotesItemsVM items)
        {
            try
            {
                if (items != null)
                {
                    await Task.Run(() =>
                    {
                        if (items.ReceiveNoteItemId == 0)
                        {
                            ReceiveNotesItems receiveNotesItems = new ReceiveNotesItems()
                            {
                                ReceiveNoteId = items.ReceiveNoteId,
                                ProductId = items.ProductId,
                                WarehouseId = items.WarehouseId,
                                ProductQTY = items.ProductQTY,
                                PurchaseOrdersId = items.PurchaseOrdersId,
                            };
                            _DbContext.ReceiveNotesItems.Add(receiveNotesItems);
                            _DbContext.SaveChanges();
                        }

                    });
                }
                else
                    throw new Exception("Item Not Null Allows");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string ReceiveNotesNumberRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
