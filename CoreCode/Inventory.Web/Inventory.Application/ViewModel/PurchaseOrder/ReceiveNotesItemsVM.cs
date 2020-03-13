using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.PurchaseOrder
{
   public class ReceiveNotesItemsVM
    {
        public long ReceiveNoteItemId { get; set; }
        public long? ReceiveNoteId { get; set; }
        public long? PurchaseOrdersId { get; set; }
        public long ProductId { get; set; }
        public long WarehouseId { get; set; }
        public long ProductQTY { get; set; }
    }
}
