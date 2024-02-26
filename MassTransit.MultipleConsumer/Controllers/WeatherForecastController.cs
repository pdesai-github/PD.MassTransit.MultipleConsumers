using Microsoft.AspNetCore.Mvc;

namespace MassTransit.MultipleConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IRequestClient<Student> _studentClient;
        IRequestClient<Employee> _employeeClient;
        private readonly IBus _bus;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IBusControl bus, IRequestClient<Student> studentClient,IRequestClient<Employee> employeeClient)
        {
            _logger = logger;
            _bus = bus;
            _studentClient = studentClient;
            _employeeClient = employeeClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var studentResponse = await _studentClient.GetResponse<Student>(new { StudentName = "Pradip", Maths = 60, Science = 90 });
            var employeeResponse = await _employeeClient.GetResponse<Employee>(new { EmployeeName = "Amit", Salary = 10000, Tax = 30 });

            AggregateResponse res = new AggregateResponse() 
            {
                Student = studentResponse.Message.StudentName,
                Marks = studentResponse.Message.Marks,
                Employee = employeeResponse.Message.EmployeeName,
                Salary = employeeResponse.Message.NetSal,
            };

            return Ok(res);
        }
    }
}