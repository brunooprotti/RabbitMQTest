using System.ComponentModel.DataAnnotations;

namespace RabbitMQTest.API.Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string CourseTitle { get; set; }
    }
}