using AnnouncementsAPI.Dtos;
using AnnouncementsAPI.Models;
using AnnouncementsAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        IAnnouncementCollectionService _announcementCollectionService;

        public AnnouncementController(IAnnouncementCollectionService announcementCollectionService)
        {
            _announcementCollectionService = announcementCollectionService ?? throw new ArgumentNullException(nameof(announcementCollectionService));
        }

        /// <summary>
        /// Get all announcements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            IEnumerable<Announcement> announcements = await _announcementCollectionService.GetAll();
            return Ok(announcements);
        }

        /// <summary>
        /// Create a new announcements 
        /// </summary>
        /// <param name="announcement">Announcements to be added</param>
        /// <returns>BadRequest if the given announcement is null ,otherwise returns Ok</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody][Required] AnnouncementDto announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }

            bool success = await _announcementCollectionService.Create(new Announcement
            {
                Id = Guid.NewGuid(),
                CategoryId = announcement.CategoryId,
                Author = announcement.Author,
                Message = announcement.Message,
                Title = announcement.Title,
                ImageUrl = announcement.ImageUrl
            });
            if (!success)
                return BadRequest("Invalid Announcement");
            return Ok();
        }

        /// <summary>
        /// Update an Exisetent announcement
        /// </summary>
        /// <param name="announcement">Announcement to update</param>
        /// <returns>BadRequest if the announcement is null or invalid, otherwise it returns Ok</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement([Required] Guid id, [FromBody] Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }
            bool isOk = await _announcementCollectionService.Update(id, announcement);
            if (isOk)
                return BadRequest("Invalid Announcement");

            return Ok();
        }

        /// <summary>
        /// Delete an announcement by it's Id
        /// </summary>
        /// <param name="announcementId">Announcement Id to be deleted</param>
        /// <returns>Ok if the announcement was successfully deleted, otherwise returns BadRequest</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            bool isOk = await _announcementCollectionService.Delete(id);
            if (isOk)
                return Ok();

            return NotFound("Announcement not found");
        }

        /// <summary>
        /// Get an announcement by it's Id
        /// </summary>
        /// <param name="id">Announcement Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement = await _announcementCollectionService.Get(id);

            if (announcement == null)
            {
                return BadRequest("Announcement not found!");
            }
            return Ok(announcement);
        }
    }
}
