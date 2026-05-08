using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class SystemStructureUserAdmin
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long StructureId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Structure Structure { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
