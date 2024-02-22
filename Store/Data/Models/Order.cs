namespace WebApplicationL5.Data.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public DateTime CreatedAt { get; set; }
}