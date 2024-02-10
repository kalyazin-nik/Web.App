using App.Models;
using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private DataContext _context;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        public DataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string GetTasks()
        {
                var result = _context.Tasks
                    .Join(_context.Statuses, t => t.StatusId, s => s.StatusId,
                        (t, s) => new { t.Id, t.Name, t.Description, s.StatusId, s.StatusName })
                    .Join(_context.Identifiers, s => s.StatusId, i => i.StatusId,
                        (s, i) => new { s.Id, s.StatusId, s.Name, s.Description, s.StatusName, i.CreatedAt, i.InWorking, i.ComplitedAt })
                    .ToArray();

                return JsonSerializer.Serialize(result, options: _options);
        }

        [HttpPost]
        public IActionResult CreateTask(string json)
        {
            try
            {
                var status = new Status("Создана");
                var task = JsonSerializer.Deserialize<Task>(json, options: _options);
                var identifier = new Identifier() { Status = status };
                task.Status = status;

                _context.Statuses.Add(status);
                _context.Tasks.Add(task);
                _context.Identifiers.Add(identifier);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) 
            { 
                return BadRequest(); 
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask(string id)
        {
            return Ok();
        }
    }
}
