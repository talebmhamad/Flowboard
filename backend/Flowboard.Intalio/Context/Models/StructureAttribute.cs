using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class StructureAttribute
{
    public int Id { get; set; }

    public int AttributeId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? Order { get; set; }

    public virtual ICollection<ApplicationStructureAttributeMapping> ApplicationStructureAttributeMappings { get; set; } = new List<ApplicationStructureAttributeMapping>();

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual ICollection<IntegrationStructureResourceAttribute> IntegrationStructureResourceAttributes { get; set; } = new List<IntegrationStructureResourceAttribute>();

    public virtual ICollection<StructureVirtualAttributesStructureAttribute> StructureVirtualAttributesStructureAttributes { get; set; } = new List<StructureVirtualAttributesStructureAttribute>();
}
