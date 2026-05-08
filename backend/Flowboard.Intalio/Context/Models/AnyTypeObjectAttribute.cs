using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyTypeObjectAttribute
{
    public int Id { get; set; }

    public short AnyTypeObjectId { get; set; }

    public int AttributeId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public short? Order { get; set; }

    public virtual AnyTypeObject AnyTypeObject { get; set; } = null!;

    public virtual ICollection<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute> AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes { get; set; } = new List<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute>();

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual ICollection<IntegrationAnyTypeObjectResourceAttribute> IntegrationAnyTypeObjectResourceAttributes { get; set; } = new List<IntegrationAnyTypeObjectResourceAttribute>();
}
