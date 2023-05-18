using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BagCollection.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }      
        [Required]
        [Range(0, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name = "As Low As")]
        public double LowPrice { get; set; }  
        [Required]
        public string Color { get; set; }
        [Required]
        public string Brand { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [ValidateNever]
        public string? ImageUrl1 { get; set; }
        [ValidateNever]
        public string? ImageUrl2 { get; set; }
        [ValidateNever]
        public string? ImageUrl3 { get; set; }
        [ValidateNever]
        public string? ImageUrl4 { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } // create a foreign key property
       
    }

}
