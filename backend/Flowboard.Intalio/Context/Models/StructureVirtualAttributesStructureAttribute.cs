using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class StructureVirtualAttributesStructureAttribute
{
    public int Id { get; set; }

    public int StructureVirtualAttributeId { get; set; }

    public int StructureAttributeId { get; set; }

    public short Order { get; set; }

    public virtual StructureAttribute StructureAttribute { get; set; } = null!;

    public virtual StructureVirtualAttribute StructureVirtualAttribute { get; set; } = null!;
}
