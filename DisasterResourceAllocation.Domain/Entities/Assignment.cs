namespace DisasterResourceAllocation.Domain.Entities;

public class Assignment
{
    public string AreaId { get; set; } = string.Empty;
    public string? TruckId { get; set; }
    public Dictionary<string, int> ResourcesDelivered { get; set; } = new();
    public string Status { get; set; } = "Pending"; // Assigned, Failed, Pending
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Assignment(string areaId)
    {
        AreaId = areaId;
    }

    public Assignment(string areaId, string truckId, Dictionary<string, int> resourcesDelivered)
    {
        AreaId = areaId;
        TruckId = truckId;
        ResourcesDelivered = resourcesDelivered;
        Status = "Assigned";
    }
}
