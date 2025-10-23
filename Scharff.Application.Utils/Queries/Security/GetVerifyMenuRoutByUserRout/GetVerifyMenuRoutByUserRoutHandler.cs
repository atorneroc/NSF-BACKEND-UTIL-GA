using MediatR;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.Security.GetVerifyMenuRoutByUserRout;

namespace Scharff.Application.Queries.Security.GetVerifyMenuRoutByUserRout
{
    public class GetVerifyMenuRoutByUserRoutHandler : IRequestHandler<GetVerifyMenuRoutByUserRoutQuery, int>
    {
        private readonly IGetVerifyMenuRoutByUserRoutQuery _getVerifyMenuRoutByUserRoutQuery;

        public GetVerifyMenuRoutByUserRoutHandler(IGetVerifyMenuRoutByUserRoutQuery getVerifyMenuRoutByUserRoutQuery)
        {
            _getVerifyMenuRoutByUserRoutQuery = getVerifyMenuRoutByUserRoutQuery;
        }

        public async Task<int> Handle(GetVerifyMenuRoutByUserRoutQuery request, CancellationToken cancellationToken)
        {
            int cant = await _getVerifyMenuRoutByUserRoutQuery.GetVerifyMenuRoutByUserRout(request.User_Email, request.Route);

            return cant;
        }
    }
}
