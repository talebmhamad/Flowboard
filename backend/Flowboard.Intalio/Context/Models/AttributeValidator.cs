using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AttributeValidator
{
    public int Id { get; set; }

    public short? ValidatorId { get; set; }

    public int? AttributeId { get; set; }

    public int? ValidatorPropertyId { get; set; }

    public string? Value { get; set; }

    public virtual Attribute? Attribute { get; set; }

    public virtual Validator? Validator { get; set; }

    public virtual ValidatorProperty? ValidatorProperty { get; set; }
}
