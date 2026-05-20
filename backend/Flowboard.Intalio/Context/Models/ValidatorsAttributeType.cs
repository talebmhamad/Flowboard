using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ValidatorsAttributeType
{
    public int Id { get; set; }

    public short ValidatorId { get; set; }

    public short AttributeTypeId { get; set; }

    public virtual AttributeType AttributeType { get; set; } = null!;

    public virtual Validator Validator { get; set; } = null!;
}
