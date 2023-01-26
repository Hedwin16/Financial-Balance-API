using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class TransactionsAccount
{
    public int Id { get; set; }

    public int IdTransaction { get; set; }

    public int IdAccount { get; set; }

    public virtual Account IdAccountNavigation { get; set; } = null!;

    public virtual Transaction IdTransactionNavigation { get; set; } = null!;
}
