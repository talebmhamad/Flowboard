using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Attribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short AttributeTypeId { get; set; }

    public long? Min { get; set; }

    public long? Max { get; set; }

    public bool UniqueConstraint { get; set; }

    public bool Mandatory { get; set; }

    public string? ListValue { get; set; }

    public string? Group { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? Hidden { get; set; }

    public virtual ICollection<AnyTypeAttributeValue> AnyTypeAttributeValues { get; set; } = new List<AnyTypeAttributeValue>();

    public virtual ICollection<AnyTypeObjectAttribute> AnyTypeObjectAttributes { get; set; } = new List<AnyTypeObjectAttribute>();

    public virtual AttributeType AttributeType { get; set; } = null!;

    public virtual ICollection<AttributeValidator> AttributeValidators { get; set; } = new List<AttributeValidator>();

    public virtual ICollection<StructureAttributeValue> StructureAttributeValues { get; set; } = new List<StructureAttributeValue>();

    public virtual ICollection<StructureAttribute> StructureAttributes { get; set; } = new List<StructureAttribute>();

    public virtual ICollection<UserAttributeValue> UserAttributeValues { get; set; } = new List<UserAttributeValue>();

    public virtual ICollection<UserAttribute> UserAttributes { get; set; } = new List<UserAttribute>();
}
