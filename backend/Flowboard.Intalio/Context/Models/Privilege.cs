using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Privilege
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RolesPrivilege> RolesPrivileges { get; set; } = new List<RolesPrivilege>();
}
