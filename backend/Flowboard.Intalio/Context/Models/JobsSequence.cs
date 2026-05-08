using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class JobsSequence
{
    public int Id { get; set; }

    public string? Sequence { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ScheduleId { get; set; }

    public virtual JobsSchedule? Schedule { get; set; }
}
