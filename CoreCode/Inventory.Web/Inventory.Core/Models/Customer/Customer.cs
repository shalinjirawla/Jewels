using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }
        public String CustomerName { get; set; }
        [ForeignKey("CustomerType")]
        public Nullable<long> CustomerTypeId { get; set; }
        public string CusromerCode { get; set; }
        public String Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string Remarks { get; set; }
        public string DefaultCreditTerms { get; set; }
        public double DefaultCreditLimit { get; set; }
       // public string PriceList { get; set; }
        //[ForeignKey]
        public long DiscountOption { get; set; }//FK
        public double DiscountAmount { get; set; }
        public double DiscountPercentage { get; set; }
        //public string DefaultShipmentTerms { get; set; }
       // public string DefaultShipmentMethod { get; set; }
        [ForeignKey("Currency")]
        public long DefaultCurrency { get; set; }

        public long CreatorUserId { get; set; }
        public DateTime LastModificationTime { get; set; }
        public long LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }


        public CustomerType CustomerType { get; set; }
        public Currency.Currency Currency { get; set; }
    }
}
