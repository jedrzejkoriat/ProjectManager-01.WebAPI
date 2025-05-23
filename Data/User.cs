﻿using Microsoft.AspNetCore.Identity;

namespace ProjectManager_01.WebAPI.Data;

public sealed class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}