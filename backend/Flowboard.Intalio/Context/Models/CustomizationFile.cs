using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class CustomizationFile
{
    public short Id { get; set; }

    public byte Type { get; set; }

    public string? Name { get; set; }

    public string? Data { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? AssemblyFullQualifiedName { get; set; }
}
