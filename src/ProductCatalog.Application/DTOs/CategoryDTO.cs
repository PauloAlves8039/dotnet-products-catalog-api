using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.DTOs
{
    public class CategoryDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
