using System;
using System.Collections.Generic;

namespace RehearsalBase.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string CustomerPassword { get; set; } = null!;

    public virtual ICollection<Rehearsal> Rehearsals { get; set; } = new List<Rehearsal>();

    public virtual ICollection<ValidSubscription> ValidSubscriptions { get; set; } = new List<ValidSubscription>();
}
