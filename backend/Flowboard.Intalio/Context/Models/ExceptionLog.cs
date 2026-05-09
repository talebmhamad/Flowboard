using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ExceptionLog
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? PrimaryKeyValue { get; set; }

    public string Exception { get; set; } = null!;

    public string MachineName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? Level { get; set; }
}
