using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Web.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IWebHostEnvironment _env;

    public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment env)
    {
        _employeeService = employeeService;
        _env = env;
    }

    public async Task<IActionResult> Index(string? searchName, string? searchSpec)
    {
        ViewBag.SearchName = searchName;
        ViewBag.SearchSpec = searchSpec;

        var employees = await _employeeService.SearchEmployeesAsync(searchName, searchSpec, null, null);
        return View(employees);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto dto, IFormFile? avatarFile)
    {
        if (avatarFile != null && avatarFile.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "avatars");
            Directory.CreateDirectory(uploads);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);

            using (var stream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await avatarFile.CopyToAsync(stream);
            }
            dto.AvatarPath = "/avatars/" + fileName;
        }

        await _employeeService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
}
