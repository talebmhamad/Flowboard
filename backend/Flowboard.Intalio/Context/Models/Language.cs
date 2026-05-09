using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Language
{
    public byte Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ApplicationStructureAttributeMapping> ApplicationStructureAttributeMappings { get; set; } = new List<ApplicationStructureAttributeMapping>();

    public virtual ICollection<ApplicationStructureVirtualAttributeMapping> ApplicationStructureVirtualAttributeMappings { get; set; } = new List<ApplicationStructureVirtualAttributeMapping>();

    public virtual ICollection<ApplicationUserAttributeMapping> ApplicationUserAttributeMappings { get; set; } = new List<ApplicationUserAttributeMapping>();

    public virtual ICollection<ApplicationUserVirtualAttributeMapping> ApplicationUserVirtualAttributeMappings { get; set; } = new List<ApplicationUserVirtualAttributeMapping>();
}
