public class JobApplication
{
    public string CompanyName { get; set; } = "";
    public string PositionTitle { get; set; } = "";
    public Status Status { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime? ResponseDate { get; set; }
    public int SalaryExpectation { get; set; }
    public int GetDaysSinceApplied()
{
    return (DateTime.Now - ApplicationDate).Days;
}
public string GetSummary()
{
    return $"{CompanyName} - {PositionTitle} - {Status}";
}
}


