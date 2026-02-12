namespace FamilyTracker.Domain.ValueObjects;

public class Location
{
    public string Street { get; private set; }
    public string BuildingNumber { get; private set; }

    public Location(string street, string buildingNumber)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));
        
        if (string.IsNullOrWhiteSpace(buildingNumber))
            throw new ArgumentException("Building number cannot be empty", nameof(buildingNumber));

        Street = street;
        BuildingNumber = buildingNumber;
    }

    public static Location Create(string street, string buildingNumber)
    {
        return new Location(street, buildingNumber);
    }

    public override string ToString()
    {
        return $"{Street} {BuildingNumber}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Location other)
            return false;

        return Street == other.Street && BuildingNumber == other.BuildingNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, BuildingNumber);
    }
}
