using Inventory.Application.Interface.RawMaterails;
using Inventory.Application.ViewModel.RawMaterails;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class RawMaterailsService: IRawMaterails
    {
        private readonly ApplicationDbContext _DbContext;

        public RawMaterailsService(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public bool SaveRawMaterails(RawMaterailsVm model, string GetUserId, long GetTenantId)
        {
            throw new NotImplementedException();
        }
        
    }
}
