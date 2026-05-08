using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class JobsSchedule
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ScheduleType { get; set; }

    public DateOnly? OneTimeOccurrenceDate { get; set; }

    public TimeOnly? OneTimeOccurrenceTime { get; set; }

    public int? FrequencyPeriodicity { get; set; }

    public int? FrequencyRecursEveryAmount { get; set; }

    public string? FrequencyRecursOn { get; set; }

    public bool? DailyFrequencyOccurrenceValue { get; set; }

    public TimeOnly? DailyFrequencyOnceOccurrenceTime { get; set; }

    public int? DailyFrequencyOccursEveryAmount { get; set; }

    public int? DailyFrequencyOccursEveryPeriodicity { get; set; }

    public int? DailyFrequencyStartHour { get; set; }

    public int? DailyFrequencyEndHour { get; set; }

    public DateOnly? DurationStartDate { get; set; }

    public bool? DurationEndDateAvailable { get; set; }

    public DateOnly? DurationEndDateValue { get; set; }

    public string? Description { get; set; }

    public int? DayFrequencyValue { get; set; }

    public int? FrequencyRecursEveryDayAmount { get; set; }

    public int? FrequencyRecursEveryTheAmount { get; set; }

    public int? FrequencyPeriodicityPosition { get; set; }

    public int? FrequencyPeriodicityDay { get; set; }

    public bool? MonthlyFrequencyOccurrenceValue { get; set; }

    public int? DailyFrequencyStartMinute { get; set; }

    public int? DailyFrequencyEndMinute { get; set; }

    public string? JobId { get; set; }

    public virtual ICollection<IntegrationStructureResource> IntegrationStructureResources { get; set; } = new List<IntegrationStructureResource>();

    public virtual ICollection<IntegrationUserResource> IntegrationUserResources { get; set; } = new List<IntegrationUserResource>();

    public virtual ICollection<JobsSequence> JobsSequences { get; set; } = new List<JobsSequence>();
}
