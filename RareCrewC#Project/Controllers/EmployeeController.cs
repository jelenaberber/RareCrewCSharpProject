using Microsoft.AspNetCore.Mvc;
using RareCrewC_Project.Models;
using RareCrewC_Project.Services;

namespace RareCrewC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employeeData = await _employeeService.GetEmployeeWorkDataAsync();
            return View(employeeData);
        }
    }
}
