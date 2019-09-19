using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.UploadImage
{
    public class UploadImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UploadImageId { get; set; }
        public string UploadImageString { get; set; }
        public Boolean DefaultImage { get; set; }
        public Nullable<long> RawMaterailId { get; set; }
        public Nullable<long> ProductId { get; set; }


        public Nullable<DateTime> CreationTime { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        [ForeignKey("Tenants")]
        public Nullable<long> TenantsId { get; set; }

        public Tenants.Tenants Tenants { get; set; }
        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }


    }
}
