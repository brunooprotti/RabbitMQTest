using Microsoft.EntityFrameworkCore;

namespace RabbitMQTest.API.Data;

public interface IStudentDbContext
{
    DbSet<Student> Student { get; set; }
    Task<int> SaveChangesAsync();
}