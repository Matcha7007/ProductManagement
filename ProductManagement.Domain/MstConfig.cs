using System;
using System.Collections.Generic;

namespace ProductManagement.Domain;

public partial class MstConfig
{
    public int Id { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string? ConfigValue { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public int LastModifiedBy { get; set; }

    public TimeOnly? LastModifiedAt { get; set; }
}
