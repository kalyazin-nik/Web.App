using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using System.Linq;

namespace App.Controllers
{
    public class TaskTableController : Controller
    {
        private DataController _dataController;
        private static List<TaskTableModel> _models = null!;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
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

            return View(_models);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description, string statusName)
        {
            var obj = new { Name = name, Description = description, StatusName = statusName };
            var json = JsonSerializer.Serialize(obj, _options);
            _dataController.CreateTask(json);
            _models = JsonSerializer.Deserialize<List<TaskTableModel>>(_dataController.GetTasks(), options: _options);

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Update(string dataUpdate)
        {
            if (dataUpdate != null)
            {
                var data = dataUpdate.Split('\n').Select(c => c.Trim()).Where(c => c.Length > 0).ToList();
                return View(data);
            }
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult UpdateTask(string id, string name, string description, string statusName, string statusId)
        {
            var obj = new { Id = int.Parse(id), Name = name, Description = description, StatusName = statusName, StatusId = int.Parse(statusId) };
            var json = JsonSerializer.Serialize(obj, _options);
            _dataController.UpdateTask(json);
            _models = JsonSerializer.Deserialize<List<TaskTableModel>>(_dataController.GetTasks(), options: _options);

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Delete(string dataDelete)
        {
            if (dataDelete != null)
            {
                var data = dataDelete.Split('\n').Select(c => c.Trim()).Where(c => c.Length > 0).ToList();
                var obj = new { Id = int.Parse(data[0]), Name = data[1], Description = data[2], StatusName = data[3], StatusId = int.Parse(data[4]) };
                var json = JsonSerializer.Serialize(obj, _options);
                _dataController.DeleteTask(json);
                _models = JsonSerializer.Deserialize<List<TaskTableModel>>(_dataController.GetTasks(), options: _options);
            }

            return Redirect("Index");
        }
    }
}
