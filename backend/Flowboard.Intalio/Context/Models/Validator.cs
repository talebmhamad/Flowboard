using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Validator
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AssemblyFullQualifiedName { get; set; }

    public string? ClassFileName { get; set; }

    public string? ClassData { get; set; }

    public string? JavascriptFileName { get; set; }

    public string? JavascriptFunctionName { get; set; }

    public string? Javascript { get; set; }

    public bool IsSystem { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AttributeValidator> AttributeValidators { get; set; } = new List<AttributeValidator>();

    public virtual ICollection<ValidatorProperty> ValidatorProperties { get; set; } = new List<ValidatorProperty>();

    public virtual ICollection<ValidatorsAttributeType> ValidatorsAttributeTypes { get; set; } = new List<ValidatorsAttributeType>();
}
