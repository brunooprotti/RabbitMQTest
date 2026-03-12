using RabbitMQTest.API.Data;
using RabbitMQTest.API.Services;

namespace RabbitMQTest.API.Endpoints;

public static class RabbitEndpoints
{
    public static void MapRabbitEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/rabbitmq").WithTags("RabbitMQ");

        group.MapPost("/", CreateNewStudent)
             .WithName("CreateNewStudent")
             .WithDescription("Creates a new student and sends a message to RabbitMQ.");
    }

    private static async Task<IResult> CreateNewStudent(StudentDto studentDto, IMessageProducer messageProducer, IStudentDbContext dbContext)
    {
        Student student = new Student
        {
            StudentName = studentDto.StudentName,
            Age = studentDto.Age,
            CourseTitle = studentDto.CourseTitle
        };

        dbContext.Student.Add(student);
        await dbContext.SaveChangesAsync();

        await messageProducer.SendMessage(student);

        return Results.Ok(student);
    }
}