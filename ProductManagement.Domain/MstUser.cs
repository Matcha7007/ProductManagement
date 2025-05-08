using System;
using System.Collections.Generic;

namespace ProductManagement.Domain;

public partial class MstUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
