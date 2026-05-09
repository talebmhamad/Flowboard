using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AttachmentDatum
{
    public long Id { get; set; }

    public byte[] Data { get; set; } = null!;

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}
