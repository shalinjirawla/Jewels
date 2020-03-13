using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.PurchaseOrder
{
    public class ReceiveNotesVM
    {
        public long ReceiveNoteId { get; set; }
        public string ReceiveNoteNumber { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public int Status { get; set; }
        public Nullable<long> TenantsId { get; set; }
        public List<ReceiveNotesItemsVM> ProductList { get; set; }

    }
}
