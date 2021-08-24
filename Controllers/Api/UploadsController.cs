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
    [Route("api/Uploads")]
    public class UploadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UploadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UploadsOfMeasure
        /// <summary>
        /// Get all uploads
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Uploads()
        {
            List<Uploads> Items = await _context.Uploads.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add an upload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Uploads),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<Uploads> payload)
        {
            Uploads uploads = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            uploads.InsertDateTime = DateTimeOffset.Now; ;
            uploads.InsertUserId = insuserId;
            _context.Uploads.Add(uploads);
            _context.SaveChanges();
            return Ok(uploads);
        }

        /// <summary>
        /// update an upload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Uploads), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<Uploads> payload)
        {
            Uploads uploads = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
             int insuserId=  _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s=> s.UserProfileId).FirstOrDefault();
            var dbUpload = _context.Uploads.Find(uploads.UploadId);
            if (dbUpload != null)
            {
                dbUpload.UpdateDateTime = DateTimeOffset.Now;
                dbUpload.UpdateUserId = insuserId;
                dbUpload.Name = uploads.Name;
                dbUpload.Description = uploads.Description;
                dbUpload.Extension = uploads.Extension;
                dbUpload.FileUrl = uploads.FileUrl;
                dbUpload.ReferenceId = uploads.ReferenceId;
                dbUpload.Reference = uploads.Reference;
                dbUpload.Url = uploads.Url;
                dbUpload.Type = uploads.Type;
                dbUpload.RecordOrder = uploads.RecordOrder;
                _context.Uploads.Update(dbUpload);
                _context.SaveChanges();
                return Ok(dbUpload);
            }
            return (NotFound());
        }

        /// <summary>
        /// update an upload
        /// Delete 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Uploads), StatusCodes.Status200OK)]
        public IActionResult Remove([FromBody]CrudViewModel<Uploads> payload)
        {
            Uploads uploads = _context.Uploads
                .Where(x => x.UploadId == (int)payload.key)
                .FirstOrDefault();
            _context.Uploads.Remove(uploads);
            _context.SaveChanges();
            return Ok(uploads);

        }
    }
}