namespace Application.Models;

public class Job : ISoftDeletable
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    
    public int VisitId { get; set; }
    public Visit? Visit { get; set; }
    
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public int WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
   // public int JobScheduleId { get; set; }
    public JobSchedule? JobSchedule { get; set; }
    public DateTime? DeletedAt { get; set; }
}