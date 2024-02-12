using System;
using System.Linq;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc;
using Database;
using Entities;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private DataContext _context;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
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
                .OrderBy(t => t.Id)
                .ToArray();

            return JsonSerializer.Serialize(result, options: _options);
        }

        [HttpPost]
        public IActionResult CreateTask(string json)
            => PerformDatabaseOperations(json, _context.Statuses.Add, _context.Tasks.Add);

        [HttpPost]
        public IActionResult UpdateTask(string json)
            => PerformDatabaseOperations(json, _context.Statuses.Update, _context.Tasks.Update);

        [HttpPost]
        public IActionResult DeleteTask(string json)
            => PerformDatabaseOperations(json, _context.Statuses.Remove, _context.Tasks.Remove);

        private IActionResult PerformDatabaseOperations
            (string json, Func<Status, EntityEntry<Status>> contextStatusMethod, Func<Task, EntityEntry<Task>> contextTaskMethod)
        {
            try
            {
                var status = JsonSerializer.Deserialize<Status>(json, options: _options);
                var task = JsonSerializer.Deserialize<Task>(json, options: _options);
                task.Status = status;
                contextStatusMethod(status);
                contextTaskMethod(task);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
