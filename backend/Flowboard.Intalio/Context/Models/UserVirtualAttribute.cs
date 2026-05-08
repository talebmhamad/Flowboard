using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UserVirtualAttribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool ByStructure { get; set; }

    public string? Separator { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? Order { get; set; }

    public virtual ICollection<ApplicationUserVirtualAttributeMapping> ApplicationUserVirtualAttributeMappings { get; set; } = new List<ApplicationUserVirtualAttributeMapping>();

    public virtual ICollection<UserVirtualAttributesUserAttribute> UserVirtualAttributesUserAttributes { get; set; } = new List<UserVirtualAttributesUserAttribute>();
}
