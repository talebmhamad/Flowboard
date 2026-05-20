using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationAnyTypeObjectResource
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

    public virtual ICollection<IntegrationAnyTypeObjectResourceAttribute> IntegrationAnyTypeObjectResourceAttributes { get; set; } = new List<IntegrationAnyTypeObjectResourceAttribute>();

    public virtual IntegrationItem IntegrationItem { get; set; } = null!;

    public virtual IntegrationType IntegrationType { get; set; } = null!;
}
