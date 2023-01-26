using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class UserPrivilege
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdPrivilege { get; set; }

    public virtual Privilege IdPrivilegeNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
