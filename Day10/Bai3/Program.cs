namespace Bai3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
        {
            new Student { StudentId = 1, Name = "An" },
            new Student { StudentId = 2, Name = "Bình" },
            new Student { StudentId = 3, Name = "Minh" }
        };

            var enrollments = new List<Enrollment>
        {
            new Enrollment { StudentId = 1, Course = "Math" },
            new Enrollment { StudentId = 2, Course = "Physics" },
            new Enrollment { StudentId = 1, Course = "Chemistry" },
            new Enrollment { StudentId = 3, Course = "Biology" },
            new Enrollment { StudentId = 2, Course = "Math" },
            new Enrollment { StudentId = 2, Course = "Geography" },
            new Enrollment { StudentId = 1, Course = "IT" },
            new Enrollment { StudentId = 3, Course = "Art" }
        };

            var studentCourses = students
                .Join(
                    enrollments,
                    student => student.StudentId,
                    enrollment => enrollment.StudentId,
                    (student, enrollment) => new
                    {
                        student.Name,
                        enrollment.Course
                    }
                )
                .ToList();

            int count = 1;
            foreach (var sc in studentCourses)
            {
                Console.WriteLine($"{count++}. Student: {sc.Name}, Course: {sc.Course}");
            }
        }
    }
}
