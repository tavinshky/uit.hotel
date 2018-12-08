using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Query
{
    public class ReceiptQuery : QueryType<Receipt>
    {
        public ReceiptQuery()
        {
            Field<ListGraphType<ReceiptType>>(
                _List,
                "Trả về một danh sách các phiếu thu",
                resolve: context => ReceiptBusiness.Get()
            );
            Field<ReceiptType>(
                _Item,
                "Trả về thông tin một phiếu thu",
                IdArgument(),
                context => ReceiptBusiness.Get(GetId<int>(context))
            );
        }
    }
}