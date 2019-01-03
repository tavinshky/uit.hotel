using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Query
{
    public class RoomKindQuery : QueryType<RoomKind>
    {
        public RoomKindQuery()
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<RoomKindType>>>>(
                _List,
                "Trả về một danh sách các loại phòng",
                resolve: _CheckPermission_List(
                    p => p.PermissionGetMap,
                    context => RoomKindBusiness.Get()
                )
            );

            Field<NonNullGraphType<RoomKindType>>(
                nameof(RoomKind),
                "Trả về thông tin của một loại phòng",
                _IdArgument(),
                _CheckPermission_Object(
                    p => p.PermissionGetMap,
                    context => RoomKindBusiness.Get(_GetId<int>(context))
                )
            );
        }
    }
}
