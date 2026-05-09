using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class UserVirtualAttributesUserAttribute
{
    public int Id { get; set; }

    public int UserVirtualAttributeId { get; set; }

    public int? UserAttributeId { get; set; }

    public short Order { get; set; }

    public string? StaticAttributeName { get; set; }

    public virtual UserAttribute? UserAttribute { get; set; }

    public virtual UserVirtualAttribute UserVirtualAttribute { get; set; } = null!;
}
