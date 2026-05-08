using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationItem
{
    public short Id { get; set; }

    public short IntegrationDataSourceId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? PrimaryKey { get; set; }

    public virtual ICollection<IntegrationAnyTypeObjectResource> IntegrationAnyTypeObjectResources { get; set; } = new List<IntegrationAnyTypeObjectResource>();

    public virtual IntegrationDataSource IntegrationDataSource { get; set; } = null!;

    public virtual ICollection<IntegrationItemDataSourceProperty> IntegrationItemDataSourceProperties { get; set; } = new List<IntegrationItemDataSourceProperty>();

    public virtual ICollection<IntegrationStructureResource> IntegrationStructureResources { get; set; } = new List<IntegrationStructureResource>();

    public virtual ICollection<IntegrationUserResource> IntegrationUserResources { get; set; } = new List<IntegrationUserResource>();
}
