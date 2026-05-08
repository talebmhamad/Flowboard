using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class RolesPrivilege
{
    public int Id { get; set; }

    public short RoleId { get; set; }

    public int PrivilegeId { get; set; }

    public virtual Privilege Privilege { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
