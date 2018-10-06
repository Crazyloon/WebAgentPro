﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_agent_pro.Data;
using web_agent_pro.Models;

namespace web_agent_pro.Controllers
{
    [Produces("application/json")]
    [Route("api/drivers")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DriversController : ControllerBase
    {
        private readonly WebAgentProDbContext _context;

        public DriversController(WebAgentProDbContext context)
        {
            _context = context;
        }

        // GET: api/Drivers
        [HttpGet]
        public IEnumerable<Driver> GetDrivers()
        {
            return _context.Drivers;
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/Drivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver([FromRoute] long id, [FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.Id)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        [HttpPost]
        public async Task<IActionResult> PostDriver([FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return Ok(driver);
        }

        private bool DriverExists(long id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}