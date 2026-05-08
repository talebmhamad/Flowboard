using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyTypeAttributeValue
{
    public long Id { get; set; }

    public long AnyTypeId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual AnyType AnyType { get; set; } = null!;

    public virtual Attribute Attribute { get; set; } = null!;
}
