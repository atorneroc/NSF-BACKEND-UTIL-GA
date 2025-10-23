using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser;

namespace Scharff.Application.Queries.Parameter.CheckExternalExistenceUser
{
    public class CheckExternalExistenceUserHandler : IRequestHandler<CheckExternalExistenceUserQuery, string>
    {
        private readonly ICheckExternalExistenceUserQuery _checkExternalExistenceUserQuery;

        public CheckExternalExistenceUserHandler(ICheckExternalExistenceUserQuery checkExternalExistenceUserQuery)
        {
            _checkExternalExistenceUserQuery = checkExternalExistenceUserQuery;
        }
        public async Task<string> Handle(CheckExternalExistenceUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _checkExternalExistenceUserQuery.CheckExternalExistenceUser(request.User_Email.ToUpper());

            return result;
        }
    }
}
