using System;
using System.Collections.Generic;

namespace Financial_Balance_API.Models;

public partial class Privilege
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<UserPrivilege> UserPrivileges { get; } = new List<UserPrivilege>();
}
