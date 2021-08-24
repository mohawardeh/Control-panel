using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using coderush.Pages;

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/EventTranslations")]
    public class EventTranslationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventTranslationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventTranslations
        /// <summary>
        /// Get all event translations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EventTranslations()
        {
            List<EventTranslations> Items = await _context.EventTranslations.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add an event translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventTranslations),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<EventTranslations> payload)
        {
            EventTranslations eventTranslations = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            eventTranslations.InsertDateTime = DateTimeOffset.Now; ;
            eventTranslations.InsertUserId = insuserId;
            _context.EventTranslations.Add(eventTranslations);
            _context.SaveChanges();
            return Ok(eventTranslations);
        }

        /// <summary>
        /// Update an event translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventTranslations), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<EventTranslations> payload)
        {
            EventTranslations eventTranslations = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbEventTranslations = _context.EventTranslations.Find(eventTranslations.EventId);
            if (dbEventTranslations != null)
            {
                dbEventTranslations.UpdateDateTime = DateTimeOffset.Now;
                dbEventTranslations.UpdateUserId = insuserId;
                dbEventTranslations.Name = eventTranslations.Name;
                dbEventTranslations.Description = eventTranslations.Description;
                dbEventTranslations.EventId = eventTranslations.EventId;
                dbEventTranslations.IsActive = eventTranslations.IsActive;
                dbEventTranslations.Language = eventTranslations.Language;                
                dbEventTranslations.Slug = eventTranslations.Slug;
                _context.EventTranslations.Update(dbEventTranslations);
                _context.SaveChanges();
                return Ok(dbEventTranslations);
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete an event translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventTranslations), StatusCodes.Status200OK)]
        public IActionResult Remove([FromBody]CrudViewModel<EventTranslations> payload)
        {
            EventTranslations eventTranslations = _context.EventTranslations
                .Where(x => x.EventId == (int)payload.key)
                .FirstOrDefault();
            _context.EventTranslations.Remove(eventTranslations);
            _context.SaveChanges();
            return Ok(eventTranslations);

        }

        /// <summary>
        /// Get a list of translations that belong to certain event
        /// </summary>
        /// <param name="eventId">The id of the event</param>
        /// <returns></returns>
        //[Route("api/Events/{eventId}/EventTranslations")]
        [HttpGet("Event/{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTranslationsForEvent(int eventId)
        {
            var translations = _context.EventTranslations.Where(e => e.EventId == eventId).ToList();
            int count = translations.Count;
            return Ok(new { translations, count });
        }
    }
}