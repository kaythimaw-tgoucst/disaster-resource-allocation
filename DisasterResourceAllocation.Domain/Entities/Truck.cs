namespace DisasterResourceAllocation.Domain.Entities;

public class Truck
{
    public string TruckId { get; set; } = string.Empty;
    public Dictionary<string, int> AvailableResources { get; set; } = new();
    public Dictionary<string, int> TravelTimes { get; set; } = new(); // AreaId -> Hours
    public bool IsAssigned { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Truck(string truckId, Dictionary<string, int> availableResources, Dictionary<string, int> travelTimes)
    {
        TruckId = truckId;
        AvailableResources = availableResources;
        TravelTimes = travelTimes;
    }
}
