﻿namespace ProjectManager_01.Domain.Models;

public sealed class Priority
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
}
