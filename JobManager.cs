public class JobManager
{
    public List<JobApplication> Applications { get; set; } = new List<JobApplication>();
    private string filePath = "jobs.json";
    public void AddJob(JobApplication job)
{
    Applications.Add(job);
}
public void ShowAll()
{
    foreach (JobApplication job in Applications)
    {
        Console.WriteLine(job.GetSummary());
    }
}
public void UpdateStatus(string companyName, Status newStatus)
{
    var job = Applications.FirstOrDefault(j => j.CompanyName.ToLower() == companyName.ToLower());
    if (job != null)
{
    job.Status = newStatus;
}
}
public void ShowByStatus(Status status)
{
    var filtrerade = Applications.Where(job => job.Status == status).ToList();
    foreach (JobApplication job in filtrerade)
{
    Console.WriteLine(job.GetSummary());
}
}
public void ShowSortedByDate()
{
    var sorterade = Applications.OrderBy(job => job.ApplicationDate).ToList();
    foreach (JobApplication job in sorterade)
{
    Console.WriteLine(job.GetSummary());
}
}
public void ShowStatistics()
{
    Console.WriteLine($"Totalt antal ansökningar: {Applications.Count}");
    int antalApplied = Applications.Count(job => job.Status == Status.Applied);
    Console.WriteLine($"Antal ansökningar med status Applied: {antalApplied}");
    int antalInterview = Applications.Count(job => job.Status == Status.Interview);
    Console.WriteLine($"Antal ansökningar med status Interview: {antalInterview}");
    int antalOffer = Applications.Count(job => job.Status == Status.Offer);
    Console.WriteLine($"Antal ansökningar med status Offer: {antalOffer}");
    int antalRejected = Applications.Count(job => job.Status == Status.Rejected);
    Console.WriteLine($"Antal ansökningar med status Rejected: {antalRejected}");
    var besvarade = Applications.Where(job => job.ResponseDate.HasValue).ToList();
    if (besvarade.Count > 0)
{
    double medelSvarstid = besvarade.Average(job => (job.ResponseDate!.Value - job.ApplicationDate).TotalDays);
    Console.WriteLine($"Genomsnittlig svarstid: {medelSvarstid:F1} dagar");
}
else
{
    Console.WriteLine("Ingen ansökan har fått svar ännu.");
}
}
public void RemoveJob(string companyName)
{
    var job = Applications.FirstOrDefault(
        j => j.CompanyName.ToLower() == companyName.ToLower());

    if (job != null)
    {
        Applications.Remove(job);
        Console.WriteLine("Jobbansökan har tagits bort!");
    }
    else
    {
        Console.WriteLine("Företaget hittades inte!");
    }
}
public void SaveJobs()
{
    string json = System.Text.Json.JsonSerializer.Serialize(Applications);
    File.WriteAllText(filePath, json);
    Console.WriteLine("Jobbansökningarna har sparats!");
}
public void LoadJobs()
{
    if (File.Exists(filePath))
    {
        string json = File.ReadAllText(filePath);

        Applications = System.Text.Json.JsonSerializer
            .Deserialize<List<JobApplication>>(json)
            ?? new List<JobApplication>();

        Console.WriteLine("Sparade jobbansökningar har lästs in!");
    }
}
}