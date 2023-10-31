using System;
using System.Collections.Generic;

namespace RehearsalBase.Models;

public partial class ValidSubscription
{
    public int ValidSubId { get; set; }

    public int? Customer { get; set; }

    public int SubType { get; set; }

    public int HoursLeft { get; set; }

    public virtual Customer? CustomerNavigation { get; set; }

    public virtual SubscriptionType SubTypeNavigation { get; set; } = null!;
}
