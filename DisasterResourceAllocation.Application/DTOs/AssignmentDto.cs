namespace DisasterResourceAllocation.Application.DTOs;

public class AssignmentDto
{
    public string AreaId { get; set; } = string.Empty;
    public string? TruckId { get; set; }
    public Dictionary<string, int> ResourcesDelivered { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public string? FailureReason { get; set; }
}

public class AssignmentResponseDto
{
    public List<AssignmentDto> Assignments { get; set; } = new();
    public DateTime GeneratedAt { get; set; }
    public int TotalAssigned { get; set; }
    public int TotalFailed { get; set; }
}
