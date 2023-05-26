using AnnouncementsAPI.Models;

namespace AnnouncementsAPI.Services.Abstractions
{
    public interface IAnnouncementCollectionService : ICollectionService<Announcement>
    {
        Task<IEnumerable<Announcement>> GetAnnouncementsByCategoryId(string categoryId);
    }
}
