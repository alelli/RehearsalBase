using System;
using System.Collections.Generic;

namespace RehearsalBase.Models;

public partial class Rehearsal
{
    public int RehearsalId { get; set; }

    public DateOnly RehearsalDate { get; set; }

    public TimeOnly RehearsalStart { get; set; }

    public TimeOnly RehearsalEnd { get; set; }

    public int Category { get; set; }

    public int? Customer { get; set; }

    public virtual RehearsalCategory CategoryNavigation { get; set; } = null!;

    public virtual Customer? CustomerNavigation { get; set; }
}
