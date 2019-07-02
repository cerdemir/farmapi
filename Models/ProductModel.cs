using System.ComponentModel.DataAnnotations;

namespace farmapi.Models
{
    public class ProductModel
    {
        /// <summary>
        /// product name
        /// </summary>
        /// <value></value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// product price
        /// </summary>
        /// <value></value>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}