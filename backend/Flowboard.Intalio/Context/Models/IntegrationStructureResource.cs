using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationStructureResource
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short IntegrationItemId { get; set; }

    public byte IntegrationTypeId { get; set; }

    public bool AllowCreate { get; set; }

    public bool AllowUpdate { get; set; }

    public bool AllowDelete { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? AllStructures { get; set; }

    public int? ScheduleId { get; set; }

    public bool IsSystem { get; set; }

    public byte StructureType { get; set; }

    public virtual IntegrationItem IntegrationItem { get; set; } = null!;

    public virtual ICollection<IntegrationStructureResourceAttribute> IntegrationStructureResourceAttributes { get; set; } = new List<IntegrationStructureResourceAttribute>();

    public virtual ICollection<IntegrationStructureResourceStructure> IntegrationStructureResourceStructures { get; set; } = new List<IntegrationStructureResourceStructure>();

    public virtual IntegrationType IntegrationType { get; set; } = null!;

    public virtual JobsSchedule? Schedule { get; set; }
}
