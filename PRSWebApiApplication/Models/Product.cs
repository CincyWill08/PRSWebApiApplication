using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRSWebApiApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string PartNumber { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        //Price DECIMAL(10,2)
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(255)]
        public string Unit { get; set; }
        [MaxLength(255)]
        public string PhotoPath { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedByUser { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}