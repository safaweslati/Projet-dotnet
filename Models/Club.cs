using System;
using System.Collections.Generic;

namespace Insat.Models;

public partial class Club
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Student> Studs { get; } = new List<Student>();
}
