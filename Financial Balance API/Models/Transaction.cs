using System;
using System.Collections.Generic;

namespace Financial_Balance_API.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public int Type { get; set; }

    public decimal Factor { get; set; }

    public int IdCurrency { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Currency IdCurrencyNavigation { get; set; } = null!;

    public virtual ICollection<TransactionsAccount> TransactionsAccounts { get; } = new List<TransactionsAccount>();
}
