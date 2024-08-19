using Microsoft.AspNetCore.Mvc;
using RareCrewC_Project.Services;
using RareCrewC_Project.Models;

namespace RareCrewC_Project.Controllers
{

    public class PieChartController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly PieChartService _chartService;

        public PieChartController(EmployeeService employeeService, PieChartService chartService)
        {
            _employeeService = employeeService;
            _chartService = chartService;
        }

        public async Task<IActionResult> GenerateChart()
        {
            var employeeData = await _employeeService.GetEmployeeWorkDataAsync();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "charts", "employee_work_hours.png");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            _chartService.GeneratePieChart(employeeData, filePath);

            return PhysicalFile(filePath, "image/png");
        }
    }

}
