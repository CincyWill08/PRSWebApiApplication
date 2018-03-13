using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRSWebApiApplication.Models
{
    public class PurchaseRequestLineItem
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedByUser { get; set; }
        public int PurchaseRequestId { get; set; }
        [JsonIgnore]
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}