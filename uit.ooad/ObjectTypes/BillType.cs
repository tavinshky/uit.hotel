using System.Linq;
using GraphQL.Types;
using uit.ooad.Models;

namespace uit.ooad.ObjectTypes
{
    public class BillType : ObjectGraphType<Bill>
    {
        public BillType()
        {
            Name = nameof(Bill);
            Description = "Một phiếu hóa đơn của khách hàng";

            Field(x => x.Id).Description("Id của hóa đơn");
            Field(x => x.Time).Description("Thời điểm in hóa đơn");

            Field<PatronType>(nameof(Bill.Patron), resolve: context => context.Source.Patron,
                              description: "Thông tin khách hàng thanh toán hóa đơn");
            Field<EmployeeType>(nameof(Bill.Employee), resolve: context => context.Source.Employee,
                                description: "Thông tin nhân viên nhận thanh toán hóa đơn");

            Field<ListGraphType<ReceiptType>>(
                nameof(Bill.Receipts),
                resolve: context =>
                    context.Source.Receipts.ToList(),
                description: "Danh sách các biên nhận cho hóa đơn");

            Field<ListGraphType<BookingType>>(
                nameof(Bill.Bookings),
                resolve: context => context.Source.Bookings.ToList(),
                description: "Danh sách các thông tin đặt trước của hóa đơn"
            );
        }
    }
}