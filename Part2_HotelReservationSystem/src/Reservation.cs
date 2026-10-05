namespace Part2_HotelReservationSystem;

public enum ReservationStatus { 
	Pending, 
	Confirmed,
	CheckedIn,
	CheckedOut,
	Cancelled 
}

public class Reservation
{
    public int ReservationId { get; }
    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }
    public Room Room { get; }                         
    public ReservationStatus Status { get; private set; }

    internal Reservation(int reservationId, Room room, DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (reservationId <= 0)
            throw new ArgumentException("Reservation id must be positive.", nameof(reservationId));
        if (room == null)
            throw new ArgumentNullException(nameof(room));
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Check-out date must be strictly after check-in date.");
        if (room.IsUnderMaintenance)
            throw new InvalidOperationException($"Room {room.RoomNumber} is under maintenance and cannot be booked.");
        if (room.HasConflict(checkInDate, checkOutDate))
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} already has an active reservation overlapping {checkInDate} - {checkOutDate}.");

        ReservationId = reservationId;
        Room = room;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;

        room.Attach(this);                            
    }

    public int Nights => CheckOutDate.DayNumber - CheckInDate.DayNumber;

    public decimal TotalCost => Nights * Room.NightlyRate;

    public bool IsActive => Status != ReservationStatus.Cancelled && Status != ReservationStatus.CheckedOut;

    public void Confirm() => TransitionTo(ReservationStatus.Confirmed);

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException(
                $"Reservation {ReservationId} cannot be checked in: it must be Confirmed (currently {Status}).");
        TransitionTo(ReservationStatus.CheckedIn);
    }

    public void CheckOut() => TransitionTo(ReservationStatus.CheckedOut);

    public void Cancel() => TransitionTo(ReservationStatus.Cancelled);

    private void TransitionTo(ReservationStatus next)
    {
        bool legal = (Status, next) switch
        {
            (ReservationStatus.Pending, ReservationStatus.Confirmed) => true,
            (ReservationStatus.Confirmed, ReservationStatus.CheckedIn) => true,
            (ReservationStatus.CheckedIn, ReservationStatus.CheckedOut) => true,
            (ReservationStatus.Pending, ReservationStatus.Cancelled) => true,
            (ReservationStatus.Confirmed, ReservationStatus.Cancelled) => true,
            _ => false
        };

        if (!legal)
            throw new InvalidOperationException(
                $"Reservation {ReservationId}: illegal status change {Status} -> {next}.");

        Status = next;
    }

    public override string ToString() =>
        $"Reservation #{ReservationId} room {Room.RoomNumber} {CheckInDate} -> {CheckOutDate} " +
        $"[{Status}] nights={Nights} total={TotalCost:F2}";
}
