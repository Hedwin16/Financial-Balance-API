using System;
using System.Collections.Generic;

namespace Financial_Balance_API.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Amount { get; set; }

    public virtual ICollection<TransactionsAccount> TransactionsAccounts { get; } = new List<TransactionsAccount>();

    public virtual ICollection<UserAccount> UserAccounts { get; } = new List<UserAccount>();
}
