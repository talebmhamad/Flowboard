using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyTypeObjectVirtualAttribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short AnyTypeObjectId { get; set; }

    public string? Separator { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AnyTypeObject AnyTypeObject { get; set; } = null!;

    public virtual ICollection<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute> AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes { get; set; } = new List<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute>();
}
