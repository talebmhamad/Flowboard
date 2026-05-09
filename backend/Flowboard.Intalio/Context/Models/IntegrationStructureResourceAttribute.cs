using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationStructureResourceAttribute
{
    public int Id { get; set; }

    public int IntegrationStructureResourceId { get; set; }

    public int? StructureAttributeId { get; set; }

    public string MappingName { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Name { get; set; }

    public virtual IntegrationStructureResource IntegrationStructureResource { get; set; } = null!;

    public virtual StructureAttribute? StructureAttribute { get; set; }
}
