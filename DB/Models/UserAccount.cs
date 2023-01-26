using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdAccount { get; set; }

    public virtual Account IdAccountNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
