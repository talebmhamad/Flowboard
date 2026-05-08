using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class StructureAttributeValue
{
    public long Id { get; set; }

    public long StructureId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual Structure Structure { get; set; } = null!;
}
