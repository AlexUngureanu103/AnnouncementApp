using System.ComponentModel.DataAnnotations;

namespace AnnouncementsAPI.Dtos
{
    public class AnnouncementDto
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, MaxLength(63)]
        public string CategoryId { get; set; }

        [Required, MaxLength(255)]
        public string Author { get; set; }
    }
}
