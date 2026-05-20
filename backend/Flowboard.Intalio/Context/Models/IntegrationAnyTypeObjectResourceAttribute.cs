using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationAnyTypeObjectResourceAttribute
{
    public int Id { get; set; }

    public int IntegrationAnyTypeObjectResourceId { get; set; }

    public int AnyTypeObjectAttributeId { get; set; }

    public string MappingName { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AnyTypeObjectAttribute AnyTypeObjectAttribute { get; set; } = null!;

    public virtual IntegrationAnyTypeObjectResource IntegrationAnyTypeObjectResource { get; set; } = null!;
}
