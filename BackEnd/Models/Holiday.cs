public class Holiday
{
    public int Id { get; set; }
    public int IdEmployee { get; set; }
    public double Duration { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; }
    public Holiday() { }
    public Holiday(HolidayDTO holidayDTO, BackendContext context)
    {
        Id = holidayDTO.Id;
        IdEmployee = holidayDTO.IdEmployee;
        Duration = holidayDTO.Duration;
        Date = holidayDTO.Date;
        Reason = holidayDTO.Reason;
    }
}