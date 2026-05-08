using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationDataSourceProperty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short IntegrationDataSourceId { get; set; }

    public string? Description { get; set; }

    public string? DefaultValue { get; set; }

    public bool Mandatory { get; set; }

    public bool Encrypted { get; set; }

    public virtual IntegrationDataSource IntegrationDataSource { get; set; } = null!;

    public virtual ICollection<IntegrationItemDataSourceProperty> IntegrationItemDataSourceProperties { get; set; } = new List<IntegrationItemDataSourceProperty>();
}
