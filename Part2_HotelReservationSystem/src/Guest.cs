namespace Part2_HotelReservationSystem;

public class Guest
{
    private readonly List<Reservation> _reservations = new();   

    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (guestId <= 0)
            throw new ArgumentException("Guest id must be positive.", nameof(guestId));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation MakeReservation(int reservationId, Room room, DateOnly checkIn, DateOnly checkOut)
    {
        var reservation = new Reservation(reservationId, room, checkIn, checkOut); 
        _reservations.Add(reservation);
        return reservation;
    }

    public override string ToString() => $"Guest #{GuestId} {FullName} ({PhoneNumber})";
}
