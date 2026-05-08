using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Parameter
{
    public int Id { get; set; }

    public string Keyword { get; set; } = null!;

    public string? Description { get; set; }

    public string? Content { get; set; }
}
