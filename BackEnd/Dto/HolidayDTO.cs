using System.ComponentModel.DataAnnotations;
public class HolidayDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The employee id is required")]
    public int IdEmployee { get; set; }

    [Required(ErrorMessage = "The duration is required")]
    public double Duration { get; set; }

    [Required(ErrorMessage = "The starting date is required")]
    public DateTime Date { get; set; }

    [StringLength(100, ErrorMessage = "The reason must contains between 3 and 100 characters", MinimumLength = 3)]
    public string Reason { get; set; }

    public HolidayDTO() { }

    public HolidayDTO(Holiday holiday)
    {
        Id = holiday.Id;
        IdEmployee = holiday.IdEmployee;
        Duration = holiday.Duration;
        Date = holiday.Date;
        Reason = holiday.Reason;
    }
}