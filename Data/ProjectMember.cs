﻿namespace ProjectManager_01.WebAPI.Data;

public sealed class ProjectMember
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid RoleId { get; set; }
    public DateTimeOffset JoinDate { get; set; }
}