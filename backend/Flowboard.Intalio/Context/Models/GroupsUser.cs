using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class GroupsUser
{
    public long Id { get; set; }

    public short GroupId { get; set; }

    public long UserId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
