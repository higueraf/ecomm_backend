using Ecomm.Application.Commons.Bases.Request;

namespace Ecomm.Infraestructure.Commons.Ordering
{
    public static class PaginateQuery
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest request)
        {
            return queryable.Skip((request.NumPage -1) * request.Records).Take(request.Records);
        }
    }
}
