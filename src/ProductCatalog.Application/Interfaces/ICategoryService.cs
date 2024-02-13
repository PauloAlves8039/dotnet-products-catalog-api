using ProductCatalog.Application.DTOs;

namespace ProductCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetById(int? id);

        Task Add(CategoryDTO categoryDto);

        Task Update(CategoryDTO categoryDto);

        Task Remove(int? id);
    }
}
