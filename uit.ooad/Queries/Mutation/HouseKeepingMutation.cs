using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Mutation
{
    public class HouseKeepingMutation : QueryType<HouseKeeping>
    {
        public HouseKeepingMutation()
        {
            Field<HouseKeepingType>(
                _Creation,
                "Tạo và trả về một hình thức dọn dẹp trong một phòng",
                InputArgument<HouseKeepingCreateInput>(),
                _CheckPermission(
                    p => p.PermissionCreateHouseKeeping,
                    context => HouseKeepingBusiness.Add(GetInput(context))
                )
            );
        }
    }
}