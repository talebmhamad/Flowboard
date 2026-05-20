using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class TwoFactorAuthenticationProvider
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AssemblyFullQualifiedName { get; set; }

    public string? ClassFileName { get; set; }

    public string? ClassData { get; set; }

    public bool? IsDefault { get; set; }
}
