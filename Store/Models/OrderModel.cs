namespace WebApplicationL5.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public DateTime CreatedAt { get; set; }
}