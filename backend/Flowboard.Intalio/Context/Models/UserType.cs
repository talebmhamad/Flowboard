using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UserType
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<IntegrationUserResourceApplicationRole> IntegrationUserResourceApplicationRoles { get; set; } = new List<IntegrationUserResourceApplicationRole>();

    public virtual ICollection<UserApplicationRole> UserApplicationRoles { get; set; } = new List<UserApplicationRole>();
}
