using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uit.hotel.Models;

namespace uit.hotel.DataAccesses
{
    public class BillDataAccess : RealmDatabase
    {
        private static int NextId => Get().Count() == 0 ? 1 : Get().Max(i => i.Id) + 1;

        public static async Task<Bill> Book(Bill bill, List<Booking> bookings)
        {
            await Database.WriteAsync(realm =>
            {
                bill.Id = NextId;
                bill = realm.Add(bill);

                foreach (var booking in bookings)
                {
                    booking.Bill = bill;
                    BookingDataAccess.Add(realm, booking);

                    booking.CalculateTotal();
                }

                bill.CalculateTotalPrice();
            });
            return bill;
        }

        public static async Task<Bill> BookAndCheckIn(Bill bill, List<Booking> bookings)
        {
            await Database.WriteAsync(realm =>
            {
                bill.Id = NextId;
                bill = realm.Add(bill);

                foreach (var booking in bookings)
                {
                    booking.Bill = bill;
                    BookingDataAccess.BookAndCheckIn(realm, booking);

                    booking.CalculateTotal();
                }

                bill.CalculateTotalPrice();
            });
            return bill;
        }

        public static async Task<Bill> PayTheBill(Employee employee, Bill billInDatabase)
        {
            await Database.WriteAsync(realm =>
            {
                billInDatabase.Employee = employee;
                billInDatabase.Time = DateTimeOffset.Now;

                var remain = billInDatabase.TotalPrice + billInDatabase.Discount - billInDatabase.TotalReceipts;
                if (remain == 0) return;

                var receipt = new Receipt();
                receipt.Money = remain;
                receipt.Bill = billInDatabase;
                receipt.Employee = employee;

                ReceiptDataAccess.Add(realm, receipt);

                billInDatabase.CalculateTotalReceipts();
            });
            return billInDatabase;
        }

        public static async Task<Bill> Update(Bill billInDatabase, Bill bill)
        {
            await Database.WriteAsync(realm =>
            {
                billInDatabase.Discount = bill.Discount;
                billInDatabase.Patron = bill.Patron.GetManaged();
            });

            return billInDatabase;
        }

        public static async void Delete(Bill bill) => await Database.WriteAsync(realm =>
        {
            foreach (var booking in bill.Bookings) realm.Remove(booking);
            realm.Remove(bill);
        });

        public static Bill Get(int billId) => Database.Find<Bill>(billId);

        public static IEnumerable<Bill> Get() => Database.All<Bill>();
    }
}
