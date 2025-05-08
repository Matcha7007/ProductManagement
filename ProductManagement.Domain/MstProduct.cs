using System;
using System.Collections.Generic;

namespace ProductManagement.Domain;

public partial class MstProduct
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
