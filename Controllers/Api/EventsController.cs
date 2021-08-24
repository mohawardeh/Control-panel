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
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Events()
        {
            List<Events> Items = await _context.Events.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add an event
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Events),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<Events> payload)
        {
            Events events = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            events.InsertDateTime = DateTimeOffset.Now; ;
            events.InsertUserId = insuserId;
            _context.Events.Add(events);
            _context.SaveChanges();
            return Ok(events);
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Events), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<Events> payload)
        {
            Events events = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbEvent = _context.Events.Find(events.EventId);
            if (dbEvent != null)
            {
                dbEvent.UpdateDateTime = DateTimeOffset.Now;
                dbEvent.UpdateUserId = insuserId;
                dbEvent.Name = events.Name;
                dbEvent.CategoryId = events.CategoryId;
                dbEvent.StartDate = events.StartDate;
                dbEvent.EndDate = events.EndDate;
                dbEvent.Image = events.Image;
                dbEvent.File = events.File;
                dbEvent.IsActive = events.IsActive;
                dbEvent.RecordOrder = events.RecordOrder;
                _context.Events.Update(dbEvent);
                _context.SaveChanges();
                return Ok(dbEvent);
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Events), StatusCodes.Status200OK)]
        public IActionResult Remove([FromBody]CrudViewModel<Events> payload)
        {
            Events events = _context.Events
                .Where(x => x.EventId == (int)payload.key)
                .FirstOrDefault();
            _context.Events.Remove(events);
            _context.SaveChanges();
            return Ok(events);

        }
        /// <summary>
        /// Get a list of events that belong to certain event category
        /// </summary>
        /// <param name="eventCategoryId">The id of the events category</param>
        /// <returns></returns>
        //[Route("api/EventCategories/{eventCategoryId}/EventCategoryEvents")]
        [HttpGet("EventCategory/{eventCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEventsForEventCategory(int eventCategoryId)
        {
            var events = _context.Events.Where(e => e.CategoryId == eventCategoryId).ToList();
            int count = events.Count;
            return Ok(new { events, count });
        }
    }
}