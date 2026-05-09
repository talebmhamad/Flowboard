using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Audit
{
    public long Id { get; set; }

    public string? OriginalValue { get; set; }

    public string? NewValue { get; set; }

    public short Module { get; set; }

    public byte Action { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;
}
