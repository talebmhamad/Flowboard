using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ValidatorProperty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short ValidatorId { get; set; }

    public string? Description { get; set; }

    public string? DefaultValue { get; set; }

    public bool Mandatory { get; set; }

    public bool IsNumber { get; set; }

    public virtual ICollection<AttributeValidator> AttributeValidators { get; set; } = new List<AttributeValidator>();

    public virtual Validator Validator { get; set; } = null!;
}
