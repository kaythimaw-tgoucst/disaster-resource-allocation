namespace DisasterResourceAllocation.Domain.Entities;

public class Area
{
    public string AreaId { get; set; } = string.Empty;
    public int UrgencyLevel { get; set; } // 1-5
    public Dictionary<string, int> RequiredResources { get; set; } = new();
    public int TimeConstraint { get; set; } // Hours
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Area(string areaId, int urgencyLevel, Dictionary<string, int> requiredResources, int timeConstraint)
    {
        AreaId = areaId;
        UrgencyLevel = urgencyLevel;
        RequiredResources = requiredResources;
        TimeConstraint = timeConstraint;
    }
}
