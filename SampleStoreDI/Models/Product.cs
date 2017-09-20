using System.ComponentModel.DataAnnotations;

namespace SampleStoreDI.Models
{
    /// <summary>
    /// 
    /// Classe do modelo de domínio que representa um produto.
    /// 
    /// </summary>
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(minimum: 0.0, maximum: 99999.9)]
        public decimal Price { get; set; }

        [Url]
        [FileExtensions(Extensions = "jpg,png")]
        public string ImageUrl { get; set; }
    }
}
