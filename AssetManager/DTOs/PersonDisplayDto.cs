﻿namespace AssetManager.DTOs;

public class PersonDisplayDto
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}