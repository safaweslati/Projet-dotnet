using System;
using System.Collections.Generic;

namespace Insat.Models;

public partial class Student
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public long Ninscri { get; set; }

    public virtual ICollection<Club> Clubs { get; } = new List<Club>();
}
