using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class CustomGrantType
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? AssemblyFullQualifiedName { get; set; }

    public string? ClassData { get; set; }

    public string? ClassFileName { get; set; }

    public bool Enabled { get; set; }
}
