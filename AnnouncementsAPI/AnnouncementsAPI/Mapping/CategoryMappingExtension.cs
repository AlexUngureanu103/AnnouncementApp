using AnnouncementsAPI.Dtos;
using AnnouncementsAPI.Models;

namespace AnnouncementsAPI.Mapping
{
    public static class CategoryMappingExtension
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            if (category == null) return null;

            CategoryDto categoryDto = new();
            categoryDto.Name = category.Name;

            return categoryDto;
        }
    }
}
