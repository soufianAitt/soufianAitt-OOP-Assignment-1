using Part2_HotelReservationSystem;

var hotel = new Hotel();
var single = hotel.AddRoom(101, RoomType.Single, 80m);
var suite = hotel.AddRoom(301, RoomType.Suite, 250m);
var guest = hotel.RegisterGuest("Mona Ali", "+20 100 000 0000");

var d = (int day) => new DateOnly(2026, 10, day);

var res = hotel.Book(guest, single, d(10), d(13));
Console.WriteLine(res);                       
res.Confirm();
res.CheckIn();
single.ChangeNightlyRate(100m);               
Console.WriteLine(res);
res.CheckOut();
Console.WriteLine(res);

void Expect(string label, Action action)
{
    try
    {
        action();
        Console.WriteLine($"[FAIL] {label}: no exception!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[OK] {label}: {ex.GetType().Name}");
    }
}

Expect("check-out before check-in", () => hotel.Book(guest, suite, d(5), d(5)));
Expect("empty guest name", () => hotel.RegisterGuest("", "123"));
Expect("negative rate", () => single.ChangeNightlyRate(-5m));

var r2 = hotel.Book(guest, suite, d(20), d(22));
Expect("double booking", () => hotel.Book(guest, suite, d(21), d(24)));
Expect("check-in while Pending", () => r2.CheckIn());
r2.Confirm();
r2.Cancel();
Expect("cancel twice", () => r2.Cancel());
Expect("check-out from Cancelled", () => r2.CheckOut());
hotel.Book(guest, suite, d(21), d(24));       

single.StartMaintenance();
Expect("book room under maintenance", () => hotel.Book(guest, single, d(25), d(27)));
single.EndMaintenance();
hotel.Book(guest, single, d(25), d(27));

Console.WriteLine($"\n{guest} history:");
foreach (var r in guest.Reservations) {
	Console.WriteLine("  " + r);
};
