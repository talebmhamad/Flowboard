using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class License
{
    public short Id { get; set; }

    public string Content { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}
