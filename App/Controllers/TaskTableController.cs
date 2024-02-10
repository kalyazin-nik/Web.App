using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using App.Models;

namespace App.Controllers
{
    public class TaskTableController : Controller
    {
        private DataController _dataController;
        private static List<TaskTableModel> _models = null!;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };


        public TaskTableController(DataController dataController)
        {
            _dataController = dataController;
        }

        public IActionResult Index()
        {
            _models ??= JsonSerializer.Deserialize<List<TaskTableModel>>(_dataController.GetTasks(), options: _options);

            return View("index", _models);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description)
        {
            var obj = new { Name = name,  Description = description };
            var json = JsonSerializer.Serialize(obj, _options);
            _dataController.CreateTask(json);
            _models = JsonSerializer.Deserialize<List<TaskTableModel>>(_dataController.GetTasks(), options: _options);

            return Redirect("Index");
        }
    }
}
