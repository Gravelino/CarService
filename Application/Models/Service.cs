namespace Application.Models;

public class Service: ISoftDeletable
{
    public int Id { get; set; }
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public decimal BasePrice { get; set; }
    public int Duration { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public int CategoryId { get; set; }
    public ServiceCategory? Category { get; set; }
}