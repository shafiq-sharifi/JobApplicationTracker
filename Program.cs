JobManager manager = new JobManager();
manager.LoadJobs();

while (true)
{
    Console.WriteLine("\n--- JOB APPLICATION TRACKER ---");
    Console.WriteLine("1. Lägg till ny ansökan");
    Console.WriteLine("2. Visa alla ansökningar");
    Console.WriteLine("3. Filtrera efter status");
    Console.WriteLine("4. Sortera efter datum");
    Console.WriteLine("5. Visa statistik");
    Console.WriteLine("6. Uppdatera status");
    Console.WriteLine("7. Ta bort ansökan");
    Console.WriteLine("8. Avsluta");

    Console.Write("Välj ett alternativ: ");
    string val = Console.ReadLine() ?? "";
    switch (val)
{
    case "1":
        Console.Write("Ange företagets namn: ");
        string foretag = Console.ReadLine() ?? "";
        Console.Write("Ange tjänstens namn: ");
        string tjanst = Console.ReadLine() ?? "";
        JobApplication nyAnsokan = new JobApplication();
        nyAnsokan.CompanyName = foretag;
        nyAnsokan.PositionTitle = tjanst;   
        nyAnsokan.Status = Status.Applied;
        nyAnsokan.ApplicationDate = DateTime.Now;
        manager.AddJob(nyAnsokan);
        Console.WriteLine("Jobbansökan har lagts till!");
        break;

    case "2":
        manager.ShowAll();
        break;
        
        case "3":
    Console.Write("Ange status (Applied, Interview, Offer, Rejected): ");
    string filterText = Console.ReadLine() ?? "";

    if (Enum.TryParse<Status>(filterText, true, out Status filterStatus)
        && Enum.IsDefined(typeof(Status), filterStatus))
    {
        manager.ShowByStatus(filterStatus);
    }
    else
    {
        Console.WriteLine("Ogiltig status!");
    }
    break;
    

    case "4":
    manager.ShowSortedByDate();
    break;

    case "5":
    manager.ShowStatistics();
    break;

    case "6":
    Console.Write("Vilket företag vill du uppdatera? ");
    string foretagAttUppdatera = Console.ReadLine() ?? "";

    Console.Write("Ange ny status (Applied, Interview, Offer, Rejected): ");
    string nyStatusText = Console.ReadLine() ?? "";

    if (Enum.TryParse<Status>(nyStatusText, true, out Status nyStatus)
    && Enum.IsDefined(typeof(Status), nyStatus))
{
    manager.UpdateStatus(foretagAttUppdatera, nyStatus);
    Console.WriteLine("Status har uppdaterats!");
}
else
{
    Console.WriteLine("Ogiltig status!");
}
 break;

 case "7":
    Console.Write("Vilket företag vill du ta bort? ");
    string foretagAttTaBort = Console.ReadLine() ?? "";

    manager.RemoveJob(foretagAttTaBort);
    break;

    case "8":
        manager.SaveJobs();
        Console.WriteLine("Programmet avslutas.");
        return;

    default:
        Console.WriteLine("Ogiltigt val! Försök igen.");
        break;
}
}