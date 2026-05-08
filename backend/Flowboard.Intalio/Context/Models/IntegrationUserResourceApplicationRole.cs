using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationUserResourceApplicationRole
{
    public int Id { get; set; }

    public int IntegrationUserResourceId { get; set; }

    public short RoleId { get; set; }

    public short ApplicationId { get; set; }

    public byte UserTypeId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual IntegrationUserResource IntegrationUserResource { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual UserType UserType { get; set; } = null!;
}
