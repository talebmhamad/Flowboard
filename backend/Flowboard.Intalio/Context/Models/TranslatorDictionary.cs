using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class TranslatorDictionary
{
    public int Id { get; set; }

    public string Keyword { get; set; } = null!;

    public string? En { get; set; }

    public string? Fr { get; set; }

    public string? Ar { get; set; }

    public bool IsSystem { get; set; }
}
