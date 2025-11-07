public static class SeedData
{
    public static void Init(BackendContext context)
    {
        // Tasks
        if (!context.Tasks.Any())
        {
            context.AddRange(
                new Task
                {
                    Duration = 5.8,
                    Description = "create an API for a task management application",
                    DueDate = DateTime.Parse("2024-12-14")
                },
                new Task
                {
                    Duration = 6.7,
                    Description = "fill the database with examples of tasks",
                    DueDate = DateTime.Parse("2024-04-05")
                },
                new Task
                {
                    Duration = 7.1,
                    Description = "organize the next wes",
                    DueDate = DateTime.Parse("2025-01-13")
                },
                new Task
                {
                    Duration = 8.5,
                    Description = "Finalize the monthly report",
                    DueDate = DateTime.Now.AddDays(5)
                },
                new Task
                {
                    Duration = 2.0,
                    Description = "Prepare the client presentation",
                    DueDate = DateTime.Now.AddDays(10)
                }
            );
        }

        // Employees
        if (!context.Employees.Any())
        {
            Employee employee1 = new Employee
            {
                Name = "Goy",
                FirstName = "Clement",
                Email = "cgoy001@ensc.fr",
                Status = "Manager",
                Pole = "Logistics",
                ResetToken = "",
                ResetTokenExpires = DateTime.Parse("2024-03-02"),
            };
            employee1.SetPassword("cg1");
            Employee employee2 = new Employee
            {
                Name = "Dupont",
                FirstName = "François",
                Email = "dfrancois@ensc.fr",
                Status = "TeamMember",
                Pole = "Logistics",
                ResetToken = "",
                ResetTokenExpires = DateTime.Parse("2024-03-02"),
            };
            employee2.SetPassword("cfw156lm");
            Employee employee3 = new Employee
            {
                Name = "Valleran",
                FirstName = "Sebastien",
                Email = "svalleran002@ensc.fr",
                Status = "TeamMember",
                Pole = "Logistics",
                ResetToken = "",
                ResetTokenExpires = DateTime.Parse("2024-03-02"),
            };
            employee3.SetPassword("jmwd151413");
            Employee employee4 = new Employee
            {
                Name = "Doe",
                FirstName = "John",
                Email = "john.doe@example.com",
                Status = "TeamMember",
                Pole = "Logistics",
                ResetToken = "",
                ResetTokenExpires = DateTime.Parse("2024-03-02"),
            };
            employee4.SetPassword("mdp1");
            Employee employee5 = new Employee
            {
                Name = "Smith",
                FirstName = "Jane",
                Email = "jane.smith@example.com",
                Status = "TeamMember",
                Pole = "Logistics",
                ResetToken = "",
                ResetTokenExpires = DateTime.Parse("2024-03-02"),
            };
            employee5.SetPassword("mdp3");

            context.AddRange(employee1, employee2, employee3, employee4, employee5);
        }

        // Assignments
        if (!context.Assignments.Any())
        {
            context.Assignments.AddRange(
                new Assignment
                {
                    IdEmployee = 1,
                    IdTask = 1,
                    Message = "To be done..."
                },
                new Assignment
                {
                    IdEmployee = 1,
                    IdTask = 3,
                    Message = "To be done..."
                },
                new Assignment
                {
                    IdEmployee = 2,
                    IdTask = 4,
                    Message = "To be done..."
                },
                new Assignment
                {
                    IdEmployee = 3,
                    IdTask = 2,
                    Message = "To be done..."
                },
                new Assignment
                {
                    IdEmployee = 3,
                    IdTask = 3,
                    Message = "To be done..."
                },
                new Assignment
                {
                    IdEmployee = 5,
                    IdTask = 4,
                    Message = "To be done..."
                }
            );
        }

        // Holidays
        if (!context.Holidays.Any())
        {
            context.Holidays.AddRange(
                new Holiday
                {
                    IdEmployee = 1,
                    Date = DateTime.Now.AddDays(30),
                    Duration = 2,
                    Reason = "Holidays"
                },
                new Holiday
                {
                    IdEmployee = 2,
                    Date = DateTime.Now.AddDays(60),
                    Duration = 1.8,
                    Reason = "Personnal break"
                },
                new Holiday
                {
                    IdEmployee = 3,
                    Date = DateTime.Now.AddDays(60),
                    Duration = 5.8,
                    Reason = "Trip"
                },
                new Holiday
                {
                    IdEmployee = 1,
                    Date = DateTime.Now.AddDays(60),
                    Duration = 60,
                    Reason = "Sabbatical leave"
                }
            );
        }

        context.SaveChanges();
    }
}
