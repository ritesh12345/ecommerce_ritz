﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Range(0.1, double.MaxValue,ErrorMessage = "Price must be grater than 0.0")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Range(1,int.MaxValue,ErrorMessage ="At least 1 product should be available")]
        public int QuantityInStock { get; set; }
    }
}
