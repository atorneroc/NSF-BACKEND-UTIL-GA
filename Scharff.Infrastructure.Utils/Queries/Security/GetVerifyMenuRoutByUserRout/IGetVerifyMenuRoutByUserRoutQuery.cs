namespace Scharff.Infrastructure.PostgreSQL.Queries.Security.GetVerifyMenuRoutByUserRout
{
    public interface IGetVerifyMenuRoutByUserRoutQuery
    {
        Task<int> GetVerifyMenuRoutByUserRout(string user_email, string rout);
    }
}
