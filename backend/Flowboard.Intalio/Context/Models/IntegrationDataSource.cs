using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationDataSource
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsSystem { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? AssemblyFullQualifiedNameUserPush { get; set; }

    public string? ClassDataUserPush { get; set; }

    public string? ClassFileNameUserPush { get; set; }

    public string? AssemblyFullQualifiedNameUserPull { get; set; }

    public string? ClassDataUserPull { get; set; }

    public string? ClassFileNameUserPull { get; set; }

    public string? AssemblyFullQualifiedNameStructurePush { get; set; }

    public string? ClassDataStructurePush { get; set; }

    public string? ClassFileNameStructurePush { get; set; }

    public string? AssemblyFullQualifiedNameStructurePull { get; set; }

    public string? ClassDataStructurePull { get; set; }

    public string? ClassFileNameStructurePull { get; set; }

    public virtual ICollection<IntegrationDataSourceProperty> IntegrationDataSourceProperties { get; set; } = new List<IntegrationDataSourceProperty>();

    public virtual ICollection<IntegrationItem> IntegrationItems { get; set; } = new List<IntegrationItem>();
}
