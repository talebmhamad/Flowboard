using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyTypeObject
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AnyTypeObjectAttribute> AnyTypeObjectAttributes { get; set; } = new List<AnyTypeObjectAttribute>();

    public virtual ICollection<AnyTypeObjectVirtualAttribute> AnyTypeObjectVirtualAttributes { get; set; } = new List<AnyTypeObjectVirtualAttribute>();

    public virtual ICollection<AnyType> AnyTypes { get; set; } = new List<AnyType>();
}
