using AnnouncementsAPI.Dtos;
using AnnouncementsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        static List<Announcement> _announcements = new List<Announcement> {
            new Announcement { Id = Guid.NewGuid(), CategoryId = "1", Title = "First Announcement", Description = "First Announcement Description" , Author = "Author_1"},
            new Announcement { Id = Guid.NewGuid(), CategoryId = "1", Title = "Second Announcement", Description = "Second Announcement Description", Author = "Author_1" },
            new Announcement { Id = Guid.NewGuid(), CategoryId = "1", Title = "Third Announcement", Description = "Third Announcement Description", Author = "Author_2"  },
            new Announcement { Id = Guid.NewGuid(), CategoryId = "1", Title = "Fourth Announcement", Description = "Fourth Announcement Description", Author = "Author_3"  },
            new Announcement { Id = Guid.NewGuid(), CategoryId = "1", Title = "Fifth Announcement", Description = "Fifth Announcement Description", Author = "Author_4"  }
        };

        /// <summary>
        /// Get all announcements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAnnouncements()
        {
            return Ok(_announcements);
        }

        /// <summary>
        /// Create a new announcements 
        /// </summary>
        /// <param name="announcement">Announcements to be added</param>
        /// <returns>BadRequest if the given announcement is null ,otherwise returns Ok</returns>
        [HttpPost]
        public IActionResult CreateAnnouncement([FromBody] AnnouncementDto announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }

            _announcements.Add(new Announcement
            {
                Id = Guid.NewGuid(),
                CategoryId = announcement.CategoryId,
                Author = announcement.Author,
                Description = announcement.Description,
                Title = announcement.Title
            });

            return Ok();
        }

        /// <summary>
        /// Update an Exisetent announcement
        /// </summary>
        /// <param name="announcement">Announcement to update</param>
        /// <returns>BadRequest if the announcement is null or already exists, otherwise it returns Ok</returns>
        [HttpPut]
        public IActionResult UpdateAnnouncement([FromBody] Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }
            if (!_announcements.Any(a => a.Id == announcement.Id))
            {
                return NotFound("Announcement not found");
            }
            Announcement announcementToUpdate = _announcements.First(a => a.Id == announcement.Id);
            announcementToUpdate.Title = announcement.Title;
            announcementToUpdate.Description = announcement.Description;
            announcementToUpdate.CategoryId = announcement.CategoryId;
            announcementToUpdate.Author = announcement.Author;

            return Ok();
        }

        /// <summary>
        /// Delete an announcement by it's Id
        /// </summary>
        /// <param name="announcementId">Announcement Id to be deleted</param>
        /// <returns>Ok if the announcement was successfully deleted, otherwise returns BadRequest</returns>
        [HttpDelete]
        public IActionResult DeleteAnnouncement([FromBody] string announcementId)
        {
            Guid Id = new();
            if(!Guid.TryParse(announcementId,out Id))
            {
                return BadRequest("Invalid Id format");
            }
            if (_announcements.Any(a => a.Id == Id))
            {
                _announcements.Remove(_announcements.First(a => a.Id == Guid.Parse(announcementId)));
                return Ok();
            }
            return NotFound("Announcement not found");

        }
    }
}
