using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute
{
    public int Id { get; set; }

    public int AnyTypeObjectVirtualAttributeId { get; set; }

    public int AnyTypeObjectAttributeId { get; set; }

    public short Order { get; set; }

    public virtual AnyTypeObjectAttribute AnyTypeObjectAttribute { get; set; } = null!;

    public virtual AnyTypeObjectVirtualAttribute AnyTypeObjectVirtualAttribute { get; set; } = null!;
}
