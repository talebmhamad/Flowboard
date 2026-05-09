using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AccessToken
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public short ApplicationId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Data { get; set; }

    public string Key { get; set; } = null!;

    public string? Type { get; set; }

    public string? SessionId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
