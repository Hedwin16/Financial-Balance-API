using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Pass { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; } = new List<UserAccount>();

    public virtual ICollection<UserPrivilege> UserPrivileges { get; } = new List<UserPrivilege>();
}
