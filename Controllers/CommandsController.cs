using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;

        // Get api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        //GET: api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItems(int id)
        {
            var commandItem = _context.CommandItems.Find(id);

            if (commandItem == null) return NotFound();

            return commandItem;
        }

        // POST: api/commands
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItems", new Command { id = command.id }, command);
        }

        //PUT: api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult<Command> PutCommandItem(int id, Command command)
        {
            if (id != command.id) return BadRequest();

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItems", new Command { id = id }, command);
            // return NoContent();
        }

        //DELETE: api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);

            if(commandItem == null) return NotFound();

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            // return commandItem;
            return "Item deleted successfully";
        }
    }
}