using Inventory.Application.ViewModel.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.PurchaseOrder
{
   public interface IReceiveNotes
    {
        Task<long> SaveReceiveNotes(ReceiveNotesVM input, string UserId, long TenantId);
        Task<List<ReceiveNotesVM>> ReceiveNotesList(long TenantId);
        Task DeleteReceiveNotes(long ReceiveNotesId);
        Task<ReceiveNotesVM> GetReceiveNotesDetails(long ReceiveNotesId);
    }
}
