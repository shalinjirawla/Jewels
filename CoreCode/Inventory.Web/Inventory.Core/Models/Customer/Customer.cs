using Inventory.Core.Models.Commons;
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
        public string CustomerName { get; set; }
        [ForeignKey("CustomerType")]
        public Nullable<long> CustomerTypeId { get; set; }
        public string CusromerCode { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("creditTerms")]
        public Nullable<long> CreditTermId { get; set; }
        public double DefaultCreditLimit { get; set; }
        [ForeignKey("discountType")]
        public Nullable<long> DiscountOption { get; set; }
        public double DiscountAmount { get; set; }
        [ForeignKey("Currency")]
        public Nullable<long> DefaultCurrency { get; set; }

        public long CreatorUserId { get; set; }
        public DateTime LastModificationTime { get; set; }
        public long LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }


        public CustomerType CustomerType { get; set; }
        public CreditTerms creditTerms { get; set; }
        public Currency.Currency Currency { get; set; }
        public DiscountType discountType { get; set; }
    }
}
