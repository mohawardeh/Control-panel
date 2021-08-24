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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace coderush.Controllers.Api
{
    //[Authorize]
    [AllowAnonymous]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/BlocksCategories")]
    public class BlocksCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlocksCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlocksCategories
        /// <summary>
        /// Get all bblocks categories
        /// </summary>
        /// <returns>Task of IActionResult</returns>
        /// <response code="200">Returns all blocks categories and its count in an object </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> BlocksCategories()
        {
            List<BlocksCategories> Items = await _context.BlocksCategories.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Get a blocks category by its id
        /// </summary>
        /// <param name="blocksCategoryId">The id of the blocks category</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the matching blocks category </response>
        [ProducesResponseType(typeof(BlocksCategories), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{blocksCategoryId}", Name = "GetBlocksCategory")]
        public IActionResult GetBlocksCategory(int blocksCategoryId)
        {
            var blocksCategory = _context.BlocksCategories.SingleOrDefault(b => b.BlocksCategoryId == blocksCategoryId);
            if (blocksCategory == null)
                return NotFound();
            return Ok(blocksCategory);
        }

        /// <summary>
        /// Add a blocks category
        /// </summary>
        /// <param name="payload">The request payload</param>
        /// <returns>IActionResult</returns>
        /// <response code="400"> when request body is empty or validation rules are not met</response>
        /// <response code="201"> the block added successfully and retrieval information can be found in location header</response>
        [ProducesResponseType(typeof(BlocksCategories),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<BlocksCategories> payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BlocksCategories blocksCategories = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            blocksCategories.InsertDateTime = DateTimeOffset.Now; 
            blocksCategories.InsertUserId = insuserId;
            _context.BlocksCategories.Add(blocksCategories);
            _context.SaveChanges();
            return CreatedAtRoute("GetBlocksCategory",new { blocksCategoryId= blocksCategories .BlocksCategoryId}, blocksCategories);
        }

        /// <summary>
        /// Update a block category
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="400"> when request body is empty or validation rules are not met</response>
        /// <response code="204"> if update is successful</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<BlocksCategories> payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BlocksCategories blocksCategories = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbBlockCategory = _context.BlocksCategories.Find(blocksCategories.BlocksCategoryId);
            if (dbBlockCategory != null)
            {
                dbBlockCategory.UpdateDateTime = DateTimeOffset.Now;
                dbBlockCategory.UpdateUserId = insuserId;
                dbBlockCategory.Name = blocksCategories.Name;
                dbBlockCategory.Description = blocksCategories.Description;
                dbBlockCategory.IsActive = blocksCategories.IsActive;
                dbBlockCategory.BlockType = blocksCategories.BlockType;
                _context.BlocksCategories.Update(dbBlockCategory);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Delete a block category
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>IActionResult</returns>
        /// <response code="204"> if delete is successful</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<BlocksCategories> payload)
        {
            BlocksCategories blocksCategories = _context.BlocksCategories
                .Where(x => x.BlocksCategoryId == (int)payload.key)
                .FirstOrDefault();
            if (blocksCategories == null)
                return NotFound();
            _context.BlocksCategories.Remove(blocksCategories);
            _context.SaveChanges();
            return NoContent();

        }
    }
}