﻿namespace ProjectManager_01.WebAPI.Data;

public sealed class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
