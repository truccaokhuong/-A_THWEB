using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        public int? HotelId { get; set; } // Nullable if offer can be standalone or for travel packages

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 100)]
        public int DiscountPercentage { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DiscountAmount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? MinimumStay { get; set; }

        [StringLength(100)]
        public string PromoCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string TermsAndConditions { get; set; } = string.Empty;

        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public bool IsExclusive { get; set; } = false;

        [ForeignKey("HotelId")]
        public virtual Hotel? Hotel { get; set; }
    }
} 