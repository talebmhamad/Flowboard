using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class StructureVirtualAttribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Separator { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? Order { get; set; }

    public virtual ICollection<ApplicationStructureVirtualAttributeMapping> ApplicationStructureVirtualAttributeMappings { get; set; } = new List<ApplicationStructureVirtualAttributeMapping>();

    public virtual ICollection<StructureVirtualAttributesStructureAttribute> StructureVirtualAttributesStructureAttributes { get; set; } = new List<StructureVirtualAttributesStructureAttribute>();
}
