using Inventory.Application.Interface.Customer;
using Inventory.Application.ViewModel;
using Inventory.Core.Models.Customer;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Application.Services.CustomersServices
{
    public class CustomerTypeServices : IcustomerType
    {
        private readonly ApplicationDbContext _DbContext;

        public CustomerTypeServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public int AddCustomerTypeAsyc(CustomerTypeVm model)
        {
            int CustomerTypeId = 0;
            try
            {
                if (model != null)
                {
                    if (model.CustomerTypeId == 0)
                    {
                        CustomerType customerType = new CustomerType();
                        customerType.CustomerTypeName = model.CustomerTypeName;
                        customerType.CreationTime = DateTime.Now;
                        customerType.CreatorUserId = "1";
                        customerType.LastModificationTime = DateTime.Now;
                        customerType.LastModifierUserId = "1";
                        customerType.IsActive = true;

                        _DbContext.CustomerTypes.Add(customerType);
                        _DbContext.SaveChanges();
                        CustomerTypeId = int.Parse(customerType.CustomerTypeId.ToString());
                    }
                    else
                    {
                        CustomerType customerType = new CustomerType();
                        customerType.CustomerTypeId = model.CustomerTypeId;
                        customerType.CustomerTypeName = model.CustomerTypeName;
                        customerType.CreationTime = DateTime.Now;
                        customerType.CreatorUserId = "1";
                        customerType.LastModificationTime = DateTime.Now;
                        customerType.LastModifierUserId = "1";
                        customerType.IsActive = true;

                        _DbContext.Update(customerType);
                        _DbContext.SaveChanges();
                        CustomerTypeId = int.Parse(customerType.CustomerTypeId.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CustomerTypeId;
        }

        public int DeleteCustomerTypeAsyc(int Id)
        {
            int CustomerTypeId = 0;
            try
            {
                var customertype = _DbContext.CustomerTypes.Where(x => x.CustomerTypeId == Id).FirstOrDefault();
                if (customertype != null)
                {
                    CustomerTypeId = int.Parse(customertype.CustomerTypeId.ToString());
                    _DbContext.CustomerTypes.Remove(customertype);
                    _DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CustomerTypeId;
        }

        public CustomerTypeVm GetCustomerTypeById(int id)
        {
            CustomerTypeVm customerType = new CustomerTypeVm();
            try
            {
                var a = _DbContext.CustomerTypes.Where(x => x.CustomerTypeId == id).FirstOrDefault();
                if (a != null)
                {
                    customerType.CustomerTypeId = a.CustomerTypeId;
                    customerType.CustomerTypeName = a.CustomerTypeName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerType;
        }

        public List<CustomerTypeVm> GetCustomerTypeList()
        {
            List<CustomerTypeVm> customerTypeList = new List<CustomerTypeVm>();
            try
            {
                var a = _DbContext.CustomerTypes.ToList();
                foreach (var h in a)
                {
                    CustomerTypeVm customerType = new CustomerTypeVm();
                    customerType.CustomerTypeId = h.CustomerTypeId;
                    customerType.CustomerTypeName = h.CustomerTypeName;
                    customerTypeList.Add(customerType);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return customerTypeList;
        }
    }
}
