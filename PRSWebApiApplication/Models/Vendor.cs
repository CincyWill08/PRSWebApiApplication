using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRSWebApiApplication.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        [MaxLength(255)]
        public string City { get; set; }
        [Required]
        [MaxLength(2)]
        public string State { get; set; }
        [Required]
        [MaxLength(5)]
        public string Zip { get; set; }
        [MaxLength(12)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public bool IsPreApproved { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedByUser { get; set; }
    }
}