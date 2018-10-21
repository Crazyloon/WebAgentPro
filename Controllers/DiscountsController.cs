﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_agent_pro.Data;
using web_agent_pro.Models;

namespace web_agent_pro.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly WebAgentProDbContext _context;

        public DiscountsController(WebAgentProDbContext context)
        {
            _context = context;
        }

        // GET: api/Discounts
        [HttpGet]
        public IEnumerable<Discount> GetDiscounts()
        {
            return _context.Discounts;
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        // PUT: api/Discounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount([FromRoute] long id, [FromBody] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discount.DiscountId)
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Discounts
        [HttpPost]
        public async Task<IActionResult> PostDiscount([FromBody] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new { id = discount.DiscountId }, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return Ok(discount);
        }

        private bool DiscountExists(long id)
        {
            return _context.Discounts.Any(e => e.DiscountId == id);
        }
    }
}