using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UserAttribute
{
    public int Id { get; set; }

    public int AttributeId { get; set; }

    public bool ByStructure { get; set; }

    public bool IsProfile { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? Order { get; set; }

    public virtual ICollection<ApplicationUserAttributeMapping> ApplicationUserAttributeMappings { get; set; } = new List<ApplicationUserAttributeMapping>();

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual ICollection<IntegrationUserResourceAttribute> IntegrationUserResourceAttributes { get; set; } = new List<IntegrationUserResourceAttribute>();

    public virtual ICollection<UserVirtualAttributesUserAttribute> UserVirtualAttributesUserAttributes { get; set; } = new List<UserVirtualAttributesUserAttribute>();
}
