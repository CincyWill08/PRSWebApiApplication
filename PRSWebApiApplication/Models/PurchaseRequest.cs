using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRSWebApiApplication.Models
{
    public class PurchaseRequest
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [MaxLength(255)]
        public string Justification { get; set; }
        [Required]
        [MaxLength(25)]
        public string DeliveryMode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        public decimal Total { get; set; }
        [Required]
        public bool Active { get; set; }
        [MaxLength(100)]
        public string ReasonForRejection { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedByUser { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}