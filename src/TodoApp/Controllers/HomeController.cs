using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var todos = _context.Todos.ToList();
        return View(todos);
    }

    [HttpPost]
    public IActionResult AddTask(string task)
    {
        var newTodo = new Todo
        {
            Task = task,
            IsCompleted = false
        };

        _context.Todos.Add(newTodo);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult RemoveTask(int id)
    {
        var todo = _context.Todos.Find(id);

        if (todo != null)
        {
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}       