using Inventory.Application.Interface;
using Inventory.Application.ViewModel;
using Inventory.Core.Models.Commons;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class DiscountTypeService : IDiscountType
    {
        private readonly ApplicationDbContext _DbContext;
        public DiscountTypeService(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<long> SaveDiscountType(DiscountTypeVm discountTypeVm)
        {
            long DiscountTypeId = 0;
            try
            {
                await Task.Run(() =>
                {
                    if (discountTypeVm != null)
                    {
                        DiscountType discountType = new DiscountType
                        {
                            DiscountName = discountTypeVm.DiscountName,
                            CreatorUserId = 001,
                            CreationTime = DateTime.Now,
                            IsActive = true,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = 001,
                        };
                        _DbContext.discountTypes.Add(discountType);
                        _DbContext.SaveChanges();
                        DiscountTypeId = discountType.DsicounttTypeId;
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return DiscountTypeId;
        }
        public async Task<DiscountTypeVm> GetDiscountType(long DiscountTypeId)
        {
            DiscountTypeVm discountTypeVm = new DiscountTypeVm();
            try
            {
                await Task.Run(() =>
                {
                    if (DiscountTypeId != 0)
                    {
                        var data = _DbContext.discountTypes.FirstOrDefault(x => x.DsicounttTypeId == DiscountTypeId);
                        if (data != null)
                        {
                            discountTypeVm.DiscounttTypeId = data.DsicounttTypeId;
                            discountTypeVm.DiscountName = data.DiscountName;
                        }
                    }

                });

            }
            catch (Exception e)
            {

                throw e;
            }
            return discountTypeVm;
        }
        public async Task<List<DiscountTypeVm>> GetDiscounytTypeList()
        {
            List<DiscountTypeVm> DiscountTypeVmList = new List<DiscountTypeVm>();
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.discountTypes.Where(x => x.IsActive == true).ToList();
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            DiscountTypeVm discountTypeVm = new DiscountTypeVm();
                            discountTypeVm.DiscounttTypeId = item.DsicounttTypeId;
                            discountTypeVm.DiscountName = item.DiscountName;
                            DiscountTypeVmList.Add(discountTypeVm);
                        }
                    }
                });

            }
            catch (Exception e)
            {

                throw e;
            }
            return DiscountTypeVmList;
        }
        public async Task<long> UpdateDiscountType(long DiscountTypeId, DiscountTypeVm discountTypeVm)
        {

            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.discountTypes.FirstOrDefault(x => x.DsicounttTypeId == DiscountTypeId);
                    if (data != null)
                    {
                        data.DiscountName = discountTypeVm.DiscountName;
                        data.LastModificationTime = DateTime.Now;
                        data.LastModifierUserId = 001;
                    }
                    _DbContext.Update(data);
                    _DbContext.SaveChanges();

                });

            }
            catch (Exception e)
            {

                throw e;
            }
            return DiscountTypeId;
        }
        public async Task<long> DeleteDiscountType(long DiscountTypeId)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (DiscountTypeId != 0)
                    {
                        var data = _DbContext.discountTypes.FirstOrDefault(x => x.DsicounttTypeId == DiscountTypeId);
                        if (data != null) {
                            _DbContext.Remove(data);
                            _DbContext.SaveChanges();
                        }
                    }
                   
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return DiscountTypeId;
        }
    }
}
