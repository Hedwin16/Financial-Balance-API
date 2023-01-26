using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class TypeOfTransaction
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<TransactionsType> TransactionsTypes { get; } = new List<TransactionsType>();
}
