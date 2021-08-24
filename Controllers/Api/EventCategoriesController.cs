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
    [Route("api/EventCategories")]
    public class EventCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCategories
        /// <summary>
        /// Get all event categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EventCategories()
        {
            List<EventCategories> Items = await _context.EventCategories.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add an event category
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategories),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<EventCategories> payload)
        {
            EventCategories eventCategories = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            eventCategories.InsertDateTime = DateTimeOffset.Now; ;
            eventCategories.InsertUserId = insuserId;
            _context.EventCategories.Add(eventCategories);
            _context.SaveChanges();
            return Ok(eventCategories);
        }

        /// <summary>
        /// Update an event category
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategories),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<EventCategories> payload)
        {
            EventCategories eventCategories = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbEventCategory = _context.EventCategories.Find(eventCategories.EventCategoryId);
            if (dbEventCategory != null)
            {
                dbEventCategory.UpdateDateTime = DateTimeOffset.Now;
                dbEventCategory.UpdateUserId = insuserId;
                dbEventCategory.Name = eventCategories.Name;
                dbEventCategory.Description = eventCategories.Description;
                dbEventCategory.IsActive = eventCategories.IsActive;
                dbEventCategory.Image = eventCategories.Image;
                dbEventCategory.RecordOrder = eventCategories.RecordOrder;
                _context.EventCategories.Update(dbEventCategory);
                _context.SaveChanges();
                return Ok(dbEventCategory);
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete an event categories
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(EventCategories),StatusCodes.Status200OK)]
        public IActionResult Remove([FromBody]CrudViewModel<EventCategories> payload)
        {
            EventCategories eventCategories = _context.EventCategories
                .Where(x => x.EventCategoryId == (int)payload.key)
                .FirstOrDefault();
            _context.EventCategories.Remove(eventCategories);
            _context.SaveChanges();
            return Ok(eventCategories);

        }
    }
}