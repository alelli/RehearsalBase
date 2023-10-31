using System;
using System.Collections.Generic;

namespace RehearsalBase.Models;

public partial class SubscriptionType
{
    public int SubscriptionId { get; set; }

    public int Category { get; set; }

    public int Price { get; set; }

    public int Hours { get; set; }

    public virtual RehearsalCategory CategoryNavigation { get; set; } = null!;

    public virtual ICollection<ValidSubscription> ValidSubscriptions { get; set; } = new List<ValidSubscription>();
}
