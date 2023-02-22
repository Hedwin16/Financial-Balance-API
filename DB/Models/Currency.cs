using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public partial class Currency
{
    public int Id { get; set; }

    [Required]
    [Range(3, 10)]
    public string Description { get; set; } = null!;

    [Required]
    public decimal Factor { get; set; }

    [Required]
    [MaxLength(10)]
    public string IsoCode { get; set; } = null!;

    [Required]
    [MaxLength(5)]
    public string? Symbol { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
