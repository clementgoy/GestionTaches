public static class SeedData
{
    public static void Init()
    {
        BackendContext context = new BackendContext();
        if (context.Tasks.Any())
        {
            return;
        }
        Task task1 = new Task
        {
            Duration = 5.8,
            Description = "create an API for a task management application",
            DueDate = DateTime.Parse("2024-12-14"),
        };
        Task task2 = new Task
        {
            Duration = 6.7,
            Description = "fill the database with examples of tasks",
            DueDate = DateTime.Parse("2024-04-05"),
        };
        Task task3 = new Task
        {
            Duration = 7.1,
            Description = "organize the next wes",
            DueDate = DateTime.Parse("2025-01-13"),
        };
        Task task4 = new Task
        {
            Duration = 8.5,
            Description = "Finalize the monthly report",
            DueDate = DateTime.Now.AddDays(5),
        };
        Task task5 = new Task
        {
            Duration = 2,
            Description = "Prepare the client presentation",
            DueDate = DateTime.Now.AddDays(10),
        };

        context.AddRange(task1, task2, task3, task4, task5);

        if (context.Employees.Any())
        {
            return;
        }
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

        if (context.Assignments.Any())
        {
            return;
        }
        Assignment assignment1 = new Assignment
        {
            IdEmployee = 1,
            IdTask = 1,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };
        Assignment assignment2 = new Assignment
        {
            IdEmployee = 1,
            IdTask = 3,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };
        Assignment assignment3 = new Assignment
        {
            IdEmployee = 2,
            IdTask = 4,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };
        Assignment assignment4 = new Assignment
        {
            IdEmployee = 3,
            IdTask = 2,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };
        Assignment assignment5 = new Assignment
        {
            IdEmployee = 3,
            IdTask = 3,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };
        Assignment assignment6 = new Assignment
        {
            IdEmployee = 5,
            IdTask = 4,
            Message = "To be done as quickly as possible, even if it means pausing other tasks",
        };

        context.Assignments.AddRange(assignment1, assignment2, assignment3, assignment4, assignment5, assignment6);

        Holiday holiday1 = new Holiday
        {
            IdEmployee = 1,
            Date = DateTime.Now.AddDays(30),
            Duration = 2,
            Reason = "Holidays",
        };
        Holiday holiday2 = new Holiday
        {
            IdEmployee = 2,
            Date = DateTime.Now.AddDays(60),
            Duration = 1.8,
            Reason = "Personnal break",
        };
        Holiday holiday3 = new Holiday
        {
            IdEmployee = 3,
            Date = DateTime.Now.AddDays(60),
            Duration = 5.8,
            Reason = "Trip",
        };
        Holiday holiday4 = new Holiday
        {
            IdEmployee = 1,
            Date = DateTime.Now.AddDays(60),
            Duration = 60,
            Reason = "Sabbatical leave",
        };

        context.Holidays.AddRange(holiday1, holiday2, holiday3, holiday4);

        context.SaveChanges();
    }
}
