using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Mutation
{
    public class ServicesDetailMutation : QueryType<ServicesDetail>
    {
        public ServicesDetailMutation()
        {
            Field<ServicesDetailType>(
                _Creation,
                "Tạo và trả về một chi tiết dịch vụ mới",
                InputArgument<ServicesDetailCreateInput>(),
                _CheckPermission(
                    p => p.PermissionCreateServicesDetail,
                    context => ServicesDetailBusiness.Add(GetInput(context))
                )
            );
        }
    }
}