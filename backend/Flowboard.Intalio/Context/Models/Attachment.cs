using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Attachment
{
    public long Id { get; set; }

    public long AttachmentDataId { get; set; }

    public string Name { get; set; } = null!;

    public double FileSize { get; set; }

    public string Extension { get; set; } = null!;

    public string? ContentType { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AttachmentDatum AttachmentData { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
