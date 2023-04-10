namespace Public.Dtos.Car;

public class CarDto
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public decimal RemainingPayment { get; set; }
    public string Status { get; set; }
    public string SalerFullName { get; set; }
    public string CustomerFullName { get; set; }
}