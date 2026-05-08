using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Group
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<GroupsUser> GroupsUsers { get; set; } = new List<GroupsUser>();
}
