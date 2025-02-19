﻿namespace Hahn.Assesment.Domain.Entities;

public class AlertCategory
{
    public Guid Id { get; set; }
    public required string Category { get; set; }
    public required string Condition { get; set; }

    public Guid AlertId { get; set; }

    public virtual AlertEntity? Alerts { get; set; }
}
