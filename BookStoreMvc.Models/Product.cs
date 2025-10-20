using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMvc.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        
        [Required]
        [DisplayName("List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
        
        [Required]
        [DisplayName("Price for 1-49")]
        [Range(1, 1000)]
        public double Price { get; set; }
        
        [Required]
        [DisplayName("Price for 50-99")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        
        [Required]
        [DisplayName("Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }
        
        public int CategoryId { get; set; }
        //CategoryId is the navigation property for this Category
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
