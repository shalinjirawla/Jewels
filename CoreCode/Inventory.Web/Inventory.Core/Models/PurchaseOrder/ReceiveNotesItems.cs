using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.PurchaseOrder
{
  public  class ReceiveNotesItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReceiveNoteItemId { get; set; }
        [ForeignKey("ReceiveNoteId")]
        public long? ReceiveNoteId { get; set; }
        [ForeignKey("PurchaseOrdersId")]
        public long? PurchaseOrdersId { get; set; }
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        [ForeignKey("WarehouseId")]
        public long WarehouseId { get; set; }
        public long ProductQTY { get; set; }
        public PurchaseOrders PurchaseOrders { get; set; }
        public Warehouse Warehouse { get; set; }
        public ReceiveNotes ReceiveNotes { get; set; }
        public Products.Product Product { get; set; }
    }
}
