using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class NotificationTemplate
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? BookmarkList { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
