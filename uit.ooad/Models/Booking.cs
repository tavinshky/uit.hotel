using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using uit.ooad.Businesses;

namespace uit.ooad.Models
{
    public class Booking : RealmObject
    {
        public enum StatusEnum
        {
            Booked,
            CheckedIn,
            RequestedCheckOut,
            CheckedOut
        }

        [Ignored]
        public List<Patron> ListOfPatrons
        {
            set
            {
                if (IsManaged)
                    throw new Exception("Chỉ tạo setter cho trường dữ liệu này đối với đối tượng chưa được quản lý.");
                foreach (var patron in value)
                    Patrons.Add(patron.GetManaged());
            }
        }

        [PrimaryKey]
        public int Id { get; set; }

        public int Status { get; set; }
        public DateTimeOffset BookCheckInTime { get; set; }
        public DateTimeOffset BookCheckOutTime { get; set; }
        public DateTimeOffset RealCheckInTime { get; set; }
        public DateTimeOffset RealCheckOutTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public Employee EmployeeBooking { get; set; }
        public Employee EmployeeCheckIn { get; set; }
        public Employee EmployeeCheckOut { get; set; }
        public Bill Bill { get; set; }
        public Room Room { get; set; }
        public IList<Patron> Patrons { get; }

        [Backlink(nameof(HouseKeeping.Booking))]
        public IQueryable<HouseKeeping> HouseKeepings { get; }

        [Backlink(nameof(ServicesDetail.Booking))]
        public IQueryable<ServicesDetail> ServicesDetails { get; }

        public void CheckValidBeforeCreate()
        {
            // Kiểm tra các điều kiện thực thi trong này.
        }

        public Booking GetManaged() => BookingBusiness.Get(Id);
    }
}
