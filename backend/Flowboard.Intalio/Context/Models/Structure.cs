using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Structure
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public long? StructureParentId { get; set; }

    public long? ManagerId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? External { get; set; }

    public virtual ICollection<Identity> Identities { get; set; } = new List<Identity>();

    public virtual ICollection<IntegrationStructureResourceStructure> IntegrationStructureResourceStructures { get; set; } = new List<IntegrationStructureResourceStructure>();

    public virtual ICollection<Structure> InverseStructureParent { get; set; } = new List<Structure>();

    public virtual User? Manager { get; set; }

    public virtual ICollection<StructureAttributeValue> StructureAttributeValues { get; set; } = new List<StructureAttributeValue>();

    public virtual Structure? StructureParent { get; set; }

    public virtual ICollection<StructuresUser> StructuresUsers { get; set; } = new List<StructuresUser>();

    public virtual ICollection<SystemStructureUserAdmin> SystemStructureUserAdmins { get; set; } = new List<SystemStructureUserAdmin>();

    public virtual ICollection<UserAttributeValue> UserAttributeValues { get; set; } = new List<UserAttributeValue>();
}
