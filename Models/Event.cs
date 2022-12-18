using System;
using System.Collections.Generic;

namespace Insat.Models;

public partial class Event
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Img { get; set; }

    public long? ClubId { get; set; }

    public string? Description { get; set; }

    public virtual Club? Club { get; set; }
}
