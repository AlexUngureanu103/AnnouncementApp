using System.ComponentModel.DataAnnotations;

namespace AnnouncementsAPI.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
