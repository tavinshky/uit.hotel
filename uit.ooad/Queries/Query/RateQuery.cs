using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Query
{
    public class RateQuery : QueryType<Rate>
    {
        public RateQuery()
        {
            Field<ListGraphType<RateType>>(
                _List,
                "Trả về một danh sách các loại giá cơ bản",
                resolve: context => RateBusiness.Get()
            );
            Field<RateType>(
                _Item,
                "Trả về thông tin một loại giá cơ bản",
                IdArgument(),
                context => RateBusiness.Get(GetId<int>(context))
            );
        }
    }
}