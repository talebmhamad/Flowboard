using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Delegation
{
    public long Id { get; set; }

    public long? FromUserId { get; set; }

    public long? ToUserId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual User? ToUser { get; set; }
}
