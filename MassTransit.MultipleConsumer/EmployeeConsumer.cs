namespace MassTransit.MultipleConsumer
{
    public class EmployeeConsumer : IConsumer<Employee>
    {
        public async Task Consume(ConsumeContext<Employee> context)
        {
            await context.RespondAsync(new Employee
            {
                EmployeeName = context.Message.EmployeeName,
                NetSal = context.Message.Salary - (context.Message.Salary * context.Message.Tax / 100),
            });
        }
    }
}
