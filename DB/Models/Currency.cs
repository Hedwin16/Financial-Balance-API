using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class Currency
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal Factor { get; set; }

    public string IsoCode { get; set; } = null!;

    public string? Symbol { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
