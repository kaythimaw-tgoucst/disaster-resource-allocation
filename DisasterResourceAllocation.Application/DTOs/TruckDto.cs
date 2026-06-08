namespace DisasterResourceAllocation.Application.DTOs;

public class TruckDto
{
    public string TruckId { get; set; } = string.Empty;
    public Dictionary<string, int> AvailableResources { get; set; } = new();
    public Dictionary<string, int> TravelTimes { get; set; } = new();
}

public class TruckResponseDto
{
    public string TruckId { get; set; } = string.Empty;
    public Dictionary<string, int> AvailableResources { get; set; } = new();
    public Dictionary<string, int> TravelTimes { get; set; } = new();
    public bool IsAssigned { get; set; }
    public DateTime CreatedAt { get; set; }
}
