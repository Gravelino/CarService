namespace Application.Models;


public class Visit : ISoftDeletable
{
    public int Id { get; set; }
    public DateTime VisitStartDate { get; set; }
    public DateTime VisitEndDate { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public int CarId { get; set; }
    public Car? Car { get; set; }
    
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<Job>? Jobs { get; set; }
    public ICollection<Feedback>? Feedbacks { get; set; }
}