using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AttributeType
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<ValidatorsAttributeType> ValidatorsAttributeTypes { get; set; } = new List<ValidatorsAttributeType>();
}
