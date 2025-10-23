using MediatR;

namespace Scharff.Application.Queries.Security.GetVerifyMenuRoutByUserRout
{
    public class GetVerifyMenuRoutByUserRoutQuery : IRequest<int>
    {
        public string User_Email { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
    }
}
