using System;
using System.Collections.Generic;

namespace RehearsalBase.Models;

public partial class RehearsalCategory
{
    public int CategoryId { get; set; }

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Rehearsal> Rehearsals { get; set; } = new List<Rehearsal>();

    public virtual ICollection<SubscriptionType> SubscriptionTypes { get; set; } = new List<SubscriptionType>();
}
