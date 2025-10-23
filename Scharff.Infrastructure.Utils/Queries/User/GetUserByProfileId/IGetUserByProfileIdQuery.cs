using Scharff.Domain.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.User.GetUserByProfileId
{
    public interface IGetUserByProfileIdQuery
    {
        Task<List<ResponseAllUserByProfileId>> GetUserByProfile( string profile_Id);
        
    }
}
