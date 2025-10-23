using MediatR;

namespace Scharff.Application.Queries.Parameter.CheckExternalExistenceUser
{
    public class CheckExternalExistenceUserQuery : IRequest<string>
    {
        public string User_Email { get; set; }
    }
}
