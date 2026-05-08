using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationType
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<IntegrationAnyTypeObjectResource> IntegrationAnyTypeObjectResources { get; set; } = new List<IntegrationAnyTypeObjectResource>();

    public virtual ICollection<IntegrationStructureResource> IntegrationStructureResources { get; set; } = new List<IntegrationStructureResource>();

    public virtual ICollection<IntegrationUserResource> IntegrationUserResources { get; set; } = new List<IntegrationUserResource>();
}
