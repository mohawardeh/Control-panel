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
    [Route("api/BlockTranslations")]
    public class BlockTranslationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlockTranslationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlockTranslations
        /// <summary>
        /// Get all block translations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BlockTranslations()
        {
            List<BlockTranslations> Items = await _context.BlockTranslations.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Add a block translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful add</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BlockTranslations),StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody]CrudViewModel<BlockTranslations> payload)
        {
            BlockTranslations blockTranslations = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            blockTranslations.InsertDateTime = DateTimeOffset.Now; ;
            blockTranslations.InsertUserId = insuserId;
            _context.BlockTranslations.Add(blockTranslations);
            _context.SaveChanges();
            return Ok(blockTranslations);
        }

        /// <summary>
        /// Update a block translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful update</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BlockTranslations),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody]CrudViewModel<BlockTranslations> payload)
        {
            BlockTranslations blockTranslations = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbBlockTranslations = _context.BlockTranslations.Find(blockTranslations.BlockId);
            if (dbBlockTranslations != null)
            {
                dbBlockTranslations.UpdateDateTime = DateTimeOffset.Now;
                dbBlockTranslations.UpdateUserId = insuserId;
                dbBlockTranslations.Name = blockTranslations.Name;
                dbBlockTranslations.Description = blockTranslations.Description;
                dbBlockTranslations.BlockId = blockTranslations.BlockId;
                dbBlockTranslations.IsActive = blockTranslations.IsActive;
                dbBlockTranslations.Language = blockTranslations.Language;                
                dbBlockTranslations.Slug = blockTranslations.Slug;
                _context.BlockTranslations.Update(dbBlockTranslations);
                _context.SaveChanges();
                return Ok(dbBlockTranslations);
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete a block translation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="200"> Successful delete</response>
        [ProducesResponseType(typeof(BlockTranslations),StatusCodes.Status200OK)]
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<BlockTranslations> payload)
        {
            BlockTranslations blockTranslations = _context.BlockTranslations
                .Where(x => x.BlockId == (int)payload.key)
                .FirstOrDefault();
            _context.BlockTranslations.Remove(blockTranslations);
            _context.SaveChanges();
            return Ok(blockTranslations);

        }

        /// <summary>
        /// Get a list of translations that belong to certain block
        /// </summary>
        /// <param name="blocksId"> The id of the block</param>
        /// <returns></returns>
        [HttpGet("Block/{blocksId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTranslationsForBlock(int blocksId)
        {
            var translations = _context.BlockTranslations.Where(e => e.BlockId == blocksId).ToList();
            int count = translations.Count;
            return Ok(new { translations, count });
        }
    }
}