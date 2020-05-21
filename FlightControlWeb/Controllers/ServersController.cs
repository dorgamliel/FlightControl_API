﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightControlWeb.Models;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly FlightDbContext _context;

        public ServersController(FlightDbContext context)
        {
            _context = context;
        }

        // GET: api/Servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServer()
        {
            return await _context.Server.ToListAsync();
        }

        // GET: api/Servers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Server>> GetServer(string id)
        {
            var server = await _context.Server.FindAsync(id);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }

        // PUT: api/Servers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServer(string id, Server server)
        {
            if (id != server.ServerID)
            {
                return BadRequest();
            }

            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(id))
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

        // POST: api/Servers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server server)
        {
            //Adding slash if in the end of url.
            if (server.ServerURL.Last() != '/')
            {
                server.ServerURL += "/";
            }
            _context.Server.Add(server);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServerExists(server.ServerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServer", new { id = server.ServerID }, server);
        }

        // DELETE: api/Servers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Server>> DeleteServer(string id)
        {
            var server = await _context.Server.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }

            _context.Server.Remove(server);
            await _context.SaveChangesAsync();

            return server;
        }

        private bool ServerExists(string id)
        {
            return _context.Server.Any(e => e.ServerID == id);
        }
    }
}
