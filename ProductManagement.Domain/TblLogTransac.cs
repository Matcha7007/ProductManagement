using System;
using System.Collections.Generic;

namespace ProductManagement.Domain;

public partial class TblLogTransac
{
    public Guid LogTransacId { get; set; }

    public string TransacType { get; set; } = null!;

    public int TransacId { get; set; }

    public int TransacStep { get; set; }

    public string TransacAction { get; set; } = null!;

    public int TransacBy { get; set; }

    public DateTime TransacStartAt { get; set; }

    public DateTime? TransacFinishAt { get; set; }

    public string? TransacParams { get; set; }

    public string? TransacNext { get; set; }

    public DateTime CreatedAt { get; set; }
}
