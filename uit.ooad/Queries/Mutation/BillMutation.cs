using System.Collections.Generic;
using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Authentication;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Mutation
{
    public class BillMutation : QueryType<Bill>
    {
        public BillMutation()
        {
            Field<NonNullGraphType<BillType>>(
                _Creation,
                "Tạo và trả về một đơn đặt phòng",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<NonNullGraphType<BookingCreateInput>>>>
                        { Name = "bookings" },
                    new QueryArgument<NonNullGraphType<BillCreateInput>> { Name = "bill" }
                ),
                _CheckPermission_TaskObject(
                    p => p.PermissionManageHiringRooms,
                    async context =>
                    {
                        var employee = AuthenticationHelper.GetEmployee(context);
                        var bill = context.GetArgument<Bill>("bill");
                        var bookings = context.GetArgument<List<Booking>>("bookings");

                        var billInDatabase = await BillBusiness.Book(employee, bill, bookings);
                        return billInDatabase;
                    }
                )
            );
            
            Field<NonNullGraphType<BillType>>(
                "BookAndCheckIn",
                "Đặt và nhận phòng ngay tại khách sạn",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<NonNullGraphType<BookingCreateInput>>>>
                        { Name = "bookings" },
                    new QueryArgument<NonNullGraphType<BillCreateInput>> { Name = "bill" }
                ),
                _CheckPermission_TaskObject(
                    p => p.PermissionManageHiringRooms,
                    async context =>
                    {
                        var employee = AuthenticationHelper.GetEmployee(context);
                        var bill = context.GetArgument<Bill>("bill");
                        var bookings = context.GetArgument<List<Booking>>("bookings");

                        return await BillBusiness.BookAndCheckIn(employee, bill, bookings);
                    }
                )
            );
        }
    }
}
