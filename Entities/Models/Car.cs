using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Car
{
    [Column("CarId")] public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    [Column(TypeName = "decimal(18,4)")] public decimal Price { get; set; }
    [Column(TypeName = "decimal(18,4)")] public decimal RemainingPayment { get; set; }
    public string SalerId { get; set; }
    [ForeignKey("SalerId")] public virtual User Saler { get; set; }
    public string? CustomerId { get; set; }
    [ForeignKey("CustomerId")] public virtual User Customer { get; set; }
    public InternalStatusEnum Status { get; set; }
}

public enum InternalStatusEnum
{
    Sold = 1,
    ReadyForSale = 2,
    PartiallyPaid
}