using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationStructureResourceStructure
{
    public int Id { get; set; }

    public int IntegrationStructureResourceId { get; set; }

    public long StructureId { get; set; }

    public virtual IntegrationStructureResource IntegrationStructureResource { get; set; } = null!;

    public virtual Structure Structure { get; set; } = null!;
}
