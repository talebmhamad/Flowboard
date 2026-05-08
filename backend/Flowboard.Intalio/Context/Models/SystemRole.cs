using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class SystemRole
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
