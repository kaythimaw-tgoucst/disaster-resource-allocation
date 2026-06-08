namespace DisasterResourceAllocation.Application.DTOs;

public class AreaDto
{
    public string AreaId { get; set; } = string.Empty;
    public int UrgencyLevel { get; set; }
    public Dictionary<string, int> RequiredResources { get; set; } = new();
    public int TimeConstraint { get; set; }
}

public class AreaResponseDto
{
    public string AreaId { get; set; } = string.Empty;
    public int UrgencyLevel { get; set; }
    public Dictionary<string, int> RequiredResources { get; set; } = new();
    public int TimeConstraint { get; set; }
    public DateTime CreatedAt { get; set; }
}
