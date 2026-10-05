namespace Part2_HotelReservationSystem;

public enum RoomType { Single, Double, Suite }
public class Room
{
    private readonly List<Reservation> _reservations = new();

    public int RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (roomNumber <= 0)
            throw new ArgumentException("Room number must be positive.", nameof(roomNumber));
        if (!Enum.IsDefined(roomType))
            throw new ArgumentException("Invalid room type.", nameof(roomType));
        ValidateRate(nightlyRate);

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        ValidateRate(newRate);
        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException($"Room {RoomNumber} is already under maintenance.");
        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        if (!IsUnderMaintenance)
            throw new InvalidOperationException($"Room {RoomNumber} is not under maintenance.");
        IsUnderMaintenance = false;
    }

    internal bool HasConflict(DateOnly checkIn, DateOnly checkOut) =>
        _reservations.Any(r => r.IsActive && r.CheckInDate < checkOut && checkIn < r.CheckOutDate);

    internal void Attach(Reservation reservation) => _reservations.Add(reservation);

    private static void ValidateRate(decimal rate)
    {
        if (rate <= 0)
            throw new ArgumentException("Nightly rate must be positive.", nameof(rate));
    }

    public override string ToString() =>
        $"Room {RoomNumber} ({RoomType}) {NightlyRate:F2}/night{(IsUnderMaintenance ? " [MAINTENANCE]" : "")}";
}
