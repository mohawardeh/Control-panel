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
    [Route("api/EventCategoryTranslations")]
    public class EventCategoryTranslationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCategoryTranslationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCategoryTranslations
        /// <summary>
        /// Get all event category translations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EventCategoryTranslations()
        {
            List<EventCategoryTranslations> Items = await _context.EventCategoryTranslations.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add an event category translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategoryTranslations),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<EventCategoryTranslations> payload)
        {
            EventCategoryTranslations eventCategoryTranslations = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            eventCategoryTranslations.InsertDateTime = DateTimeOffset.Now; ;
            eventCategoryTranslations.InsertUserId = insuserId;
            _context.EventCategoryTranslations.Add(eventCategoryTranslations);
            _context.SaveChanges();
            return Ok(eventCategoryTranslations);
        }

        /// <summary>
        /// Update an event category translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategoryTranslations), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<EventCategoryTranslations> payload)
        {
            EventCategoryTranslations eventCategoryTranslations = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbeventCategoryTranslation = _context.EventCategoryTranslations.Find(eventCategoryTranslations.EventCategoryTranslationId);
            if (dbeventCategoryTranslation != null)
            {
                dbeventCategoryTranslation.UpdateDateTime = DateTimeOffset.Now;
                dbeventCategoryTranslation.UpdateUserId = insuserId;
                dbeventCategoryTranslation.Name = eventCategoryTranslations.Name;
                dbeventCategoryTranslation.Description = eventCategoryTranslations.Description;
                dbeventCategoryTranslation.CategoryId = eventCategoryTranslations.CategoryId;
                dbeventCategoryTranslation.IsActive = eventCategoryTranslations.IsActive;
                dbeventCategoryTranslation.Language = eventCategoryTranslations.Language;
                dbeventCategoryTranslation.Slug = eventCategoryTranslations.Slug;
                _context.EventCategoryTranslations.Update(dbeventCategoryTranslation);
                _context.SaveChanges();
                return Ok(dbeventCategoryTranslation);
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete an event category translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategoryTranslations), StatusCodes.Status200OK)]
        public IActionResult Remove([FromBody]CrudViewModel<EventCategoryTranslations> payload)
        {
            EventCategoryTranslations eventCategoryTranslations = _context.EventCategoryTranslations
                .Where(x => x.EventCategoryTranslationId == (int)payload.key)
                .FirstOrDefault();
            _context.EventCategoryTranslations.Remove(eventCategoryTranslations);
            _context.SaveChanges();
            return Ok(eventCategoryTranslations);

        }

        /// <summary>
        /// Get a list of translations that belong to certain event category
        /// </summary>
        /// <param name="eventCategoryId">The id of the events category</param>
        /// <returns></returns>
        [HttpGet("EventCategory/{eventCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTranslationsForEventCategory(int eventCategoryId) {
            var translations = _context.EventCategoryTranslations.Where(e => e.CategoryId == eventCategoryId).ToList();
            int count = translations.Count;
            return Ok(new { translations,count});
        }
    }
}