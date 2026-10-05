namespace Part2_HotelReservationSystem;

public class Hotel
{
    private readonly List<Room> _rooms = new();
    private readonly List<Guest> _guests = new();
    private int _nextGuestId = 1;
    private int _nextReservationId = 1;

    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyList<Guest> Guests => _guests.AsReadOnly();

    public Room AddRoom(int roomNumber, RoomType type, decimal nightlyRate)
    {
		//checking inside each room in the lit
        if (_rooms.Any(r => r.RoomNumber == roomNumber))
            throw new InvalidOperationException($"the room is already taken.");
        var room = new Room(roomNumber, type, nightlyRate);
        _rooms.Add(room);
        return room;
    }

    public Guest RegisterGuest(string fullName, string phoneNumber)
    {
        var guest = new Guest(_nextGuestId, fullName, phoneNumber);   
        _nextGuestId++;
        _guests.Add(guest);
        return guest;
    }

    public Reservation Book(Guest guest, Room room, DateOnly checkIn, DateOnly checkOut)
    {
        var reservation = guest.MakeReservation(_nextReservationId, room, checkIn, checkOut);
        _nextReservationId++;
        return reservation;
    }
}
