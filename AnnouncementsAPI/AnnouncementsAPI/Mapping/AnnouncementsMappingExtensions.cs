using AnnouncementsAPI.Dtos;
using AnnouncementsAPI.Models;

namespace AnnouncementsAPI.Mapping
{
    public static class AnnouncementsMappingExtensions
    {
        public static AnnouncementDto ToAnnouncement(this Announcement announcement)
        {
            if (announcement == null) return null;

            AnnouncementDto announcementDto = new();
            announcementDto.Title = announcement.Title;
            announcementDto.Author = announcement.Author;
            announcementDto.Description = announcement.Description;
            announcementDto.CategoryId = announcement.CategoryId;

            return announcementDto;
        }
    }
}
