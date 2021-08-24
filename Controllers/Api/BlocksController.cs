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
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/Blocks")]
    public class BlocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlocksController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Blocks
        /// <summary>
        /// Get all blocks
        /// </summary>
        /// <returns>Task of IActionResult</returns>
        /// <response code="200">Returns all blocks and its count in an object </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Blocks()
        {
            List<Blocks> Items = await _context.Blocks.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Get a block by its id
        /// </summary>
        /// <param name="blockId">The id of the block</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the matching block</response>
        [ProducesResponseType(typeof(Blocks), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{blockId}", Name = "GetBlock")]
        public IActionResult GetBlocksCategory(int blockId)
        {
            var block= _context.Blocks.SingleOrDefault(b => b.BlockId == blockId);
            if (block == null)
                return NotFound();
            return Ok(block);
        }

        /// <summary>
        /// Add a block
        /// </summary>
        /// <param name="payload">CrudViewModel of type Blocks</param>
        /// <returns>Blocks</returns>
        /// <response code="400"> when request body is empty or validation rules are not met</response>
        /// <response code="201"> the block added successfully and retrieval information can be found in location header</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Blocks),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]
        public IActionResult Insert([FromBody]CrudViewModel<Blocks> payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Blocks blocks = payload.value;
           
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            blocks.InsertDateTime = DateTimeOffset.Now; ;
            blocks.InsertUserId = insuserId;
            _context.Blocks.Add(blocks);
            _context.SaveChanges();
            return CreatedAtRoute("GetBlock", new { BlockId = blocks.BlockId }, blocks); ;
        }


        /// <summary>
        /// Update a block
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <response code="400"> when request body is empty or validation rules are not met</response>
        /// <response code="204"> if update is successful</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Blocks> payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Blocks blocks = payload.value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int insuserId = _context.UserProfile.Where(s => s.ApplicationUserId == userId).Select(s => s.UserProfileId).FirstOrDefault();
            var dbBlock = _context.Blocks.Find(blocks.BlockId);
            if (dbBlock != null)
            {
                dbBlock.UpdateDateTime = DateTimeOffset.Now;
                dbBlock.UpdateUserId = insuserId;
                dbBlock.Name = blocks.Name;
                dbBlock.Description = blocks.Description;
                dbBlock.CategoryId = blocks.CategoryId;
                dbBlock.IsVisible = blocks.IsVisible;
                dbBlock.IsActive = blocks.IsActive;
                dbBlock.Image = blocks.Image;
                dbBlock.Url = blocks.Url;
                dbBlock.File = blocks.File;
                dbBlock.RecordOrder = blocks.RecordOrder;
                _context.Blocks.Update(dbBlock);
                _context.SaveChanges();
                return NoContent();
            }
            return (NotFound());
        }

        /// <summary>
        /// Delete a block
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>IActionResult</returns>
        /// <response code="204"> if delete is successful</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Blocks> payload)
        {
            Blocks blocks = _context.Blocks
                .Where(x => x.BlockId == (int)payload.key)
                .FirstOrDefault();
            if (blocks == null)
                return NotFound();
            _context.Blocks.Remove(blocks);
            _context.SaveChanges();            
            return NoContent();

        }

        //[Route("api/BlocksCategories/{blocksCategoryId}/BlocksCategoryBlocks")]
        /// <summary>
        /// Get a list of blocks that belong to certain blocksCategory
        /// </summary>
        /// <param name="blocksCategoryId">The id of the blocks category</param>
        /// <returns></returns>
        [HttpGet("BlocksCategory/{blocksCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBlocksForBlockCategory(int blocksCategoryId)
        {
            var blocks = _context.Blocks.Where(e => e.CategoryId == blocksCategoryId).ToList();
            int count = blocks.Count;
            return Ok(new { blocks, count });
        }
    }
}