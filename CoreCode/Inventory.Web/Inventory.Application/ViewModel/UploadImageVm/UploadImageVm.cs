using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.UploadImageVm
{
    public class UploadImageVm
    {
        public string PictuterId { get; set; }
        public string PictuterString { get; set; }
        public Boolean DefaultPicture { get; set; }
        public Nullable<long> RawMaterailId { get; set; }
        public Nullable<long> ProductId { get; set; }
    }
}
