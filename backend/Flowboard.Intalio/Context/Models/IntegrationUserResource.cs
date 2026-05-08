using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationUserResource
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short IntegrationItemId { get; set; }

    public byte IntegrationTypeId { get; set; }

    public bool AllowCreate { get; set; }

    public bool AllowUpdate { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? ApplicationId { get; set; }

    public bool? AllUsers { get; set; }

    public int? ScheduleId { get; set; }

    public bool IsSystem { get; set; }

    public virtual Application? Application { get; set; }

    public virtual IntegrationItem IntegrationItem { get; set; } = null!;

    public virtual IntegrationType IntegrationType { get; set; } = null!;

    public virtual ICollection<IntegrationUserResourceApplicationRole> IntegrationUserResourceApplicationRoles { get; set; } = new List<IntegrationUserResourceApplicationRole>();

    public virtual ICollection<IntegrationUserResourceAttribute> IntegrationUserResourceAttributes { get; set; } = new List<IntegrationUserResourceAttribute>();

    public virtual JobsSchedule? Schedule { get; set; }
}
