using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Identity
{
    public Guid Id { get; set; }

    public long? StructureId { get; set; }

    public long? UserId { get; set; }

    public long? AnyTypeId { get; set; }

    public virtual AnyType? AnyType { get; set; }

    public virtual Structure? Structure { get; set; }

    public virtual User? User { get; set; }
}
