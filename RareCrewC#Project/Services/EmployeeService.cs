using Newtonsoft.Json;
using RareCrewC_Project.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace RareCrewC_Project.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeWorkData>> GetEmployeeWorkDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==");
                response.EnsureSuccessStatusCode();

                var employees = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());

                var employeeWorkTime = employees
                    .GroupBy(e => e.EmployeeName)
                    .Select(g => new EmployeeWorkData
                    {
                        Name = g.Key,
                        TotalTimeWorked = Math.Round(g.Sum(e => (e.EndTimeUtc - e.StarTimeUtc).TotalHours))
                    })
                    .OrderByDescending(e => e.TotalTimeWorked)
                    .ToList();

                return employeeWorkTime;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Došlo je do greške prilikom preuzimanja podataka.", ex);
            }
        }

    }
}
