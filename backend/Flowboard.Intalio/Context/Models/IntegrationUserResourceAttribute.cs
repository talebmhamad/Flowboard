using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationUserResourceAttribute
{
    public int Id { get; set; }

    public int IntegrationUserResourceId { get; set; }

    public int? UserAttributeId { get; set; }

    public string MappingName { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Name { get; set; }

    public virtual IntegrationUserResource IntegrationUserResource { get; set; } = null!;

    public virtual UserAttribute? UserAttribute { get; set; }
}
