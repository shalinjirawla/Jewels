﻿using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel.CommonsVm;
using Inventory.Core.Models.Commons;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Application.Services.CommonsServices
{
    public class WarehouseService : IWarehouse
    {
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status = false;
        public WarehouseService(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public bool DeleteWarehouseAsync(long id)
        {
            var warehouse = false;
            try
            {
                var ware = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (ware != null)
                {
                    _DbContext.Warehouses.Remove(ware);
                    _DbContext.SaveChanges();
                    warehouse = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouse;
        }

        public List<WarehouseVm> GetActiveWarehouseListAsync()
        {
            List<WarehouseVm> warehouseList = new List<WarehouseVm>();
            try
            {
                var list = _DbContext.Warehouses.Where(x => x.IsActive == true).ToList();
                foreach (var a in list)
                {
                    WarehouseVm warehouse = new WarehouseVm();
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;

                    warehouseList.Add(warehouse);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouseList;
        }

        public WarehouseVm GetWarehouseAsync(long id)
        {
            WarehouseVm warehouse = new WarehouseVm();
            try
            {
                var a = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (a != null)
                {
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouse;
        }

        public List<WarehouseVm> GetWarehouseListAsync()
        {
            List<WarehouseVm> warehouseList = new List<WarehouseVm>();
            try
            {
                var list = _DbContext.Warehouses.ToList();
                foreach (var a in list)
                {
                    WarehouseVm warehouse = new WarehouseVm();
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;

                    warehouseList.Add(warehouse);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouseList;
        }

        public string SaveWarehouseListAsync(WarehouseVm model)
        {
            string WarehouseId = "";
            try
            {
                if (model.WarehouseId == 0)
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.Name = model.WarehouseName;
                    warehouse.code = model.Warehousecode;
                    warehouse.CreationTime = DateTime.Now;
                    warehouse.LastModificationTime = DateTime.Now;
                    warehouse.CreatorUserId = "18584764-e39b-4633-8682-4821b967a34c";
                    warehouse.LastModifierUserId = "18584764-e39b-4633-8682-4821b967a34c";
                    warehouse.IsActive = true;

                    _DbContext.Warehouses.Add(warehouse);
                    _DbContext.SaveChanges();
                    WarehouseId = "Warehouse is Added Successfully";

                }
                else
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == model.WarehouseId);
                    if (warehouse != null)
                    {
                        warehouse.Name = model.WarehouseName;
                        warehouse.code = model.Warehousecode;
                        warehouse.LastModificationTime = DateTime.Now;
                        warehouse.LastModifierUserId = "18584764-e39b-4633-8682-4821b967a34c";

                        _DbContext.Update(warehouse);
                        _DbContext.SaveChanges();
                        WarehouseId = "Warehouse is Updates Successfully";
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return WarehouseId;
        }

        public bool UpdateWarehouseStatusAsync(long id, bool status)
        {
           
            try
            {
                Warehouse warehouse = new Warehouse();
                warehouse = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (warehouse != null)
                {
                    warehouse.IsActive = status;
                    warehouse.LastModificationTime = DateTime.Now;
                    warehouse.LastModifierUserId = "18584764-e39b-4633-8682-4821b967a34c";

                    _DbContext.Update(warehouse);
                    _DbContext.SaveChanges();
                    Status = true;
                }
                else { Status = false; }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Status;
        }
    }
}
