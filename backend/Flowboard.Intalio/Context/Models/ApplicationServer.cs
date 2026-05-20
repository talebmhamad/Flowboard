using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ApplicationServer
{
    public int Id { get; set; }

    public string? ServerName { get; set; }

    public string? Url { get; set; }
}
