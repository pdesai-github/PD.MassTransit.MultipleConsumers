namespace MassTransit.MultipleConsumer
{
    public class StudentConsumer : IConsumer<Student>
    {
        public async Task Consume(ConsumeContext<Student> context)
        {
            await context.RespondAsync(new Student
            {
                StudentName = context.Message.StudentName,
                Marks = context.Message.Maths + context.Message.Science
            });
        }
    }
}
