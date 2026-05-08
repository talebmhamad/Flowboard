using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UserApplicationRole
{
    public long Id { get; set; }

    public short RoleId { get; set; }

    public short ApplicationId { get; set; }

    public long UserId { get; set; }

    public byte UserTypeId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserType UserType { get; set; } = null!;
}
