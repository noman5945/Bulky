using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author {  get; set; }
        
        [Required]
        [Range(0, 1000)]
        [DisplayName("List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(0, 1000)]
        [DisplayName("Price for 1-50")]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000)]
        [DisplayName("Price for 50+")]
        public double Price50 { get; set; }

        [Required]
        [Range(0, 1000)]
        [DisplayName("Price for 100+")]
        public double Price100 { get; set; }
    }
}
