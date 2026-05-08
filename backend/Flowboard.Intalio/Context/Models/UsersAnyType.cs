using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UsersAnyType
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long AnyTypeId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AnyType AnyType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
