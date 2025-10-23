using MediatR;
using Scharff.Domain.Response.Security.GetAccessByUser;
using Scharff.Domain.Utils.Exceptions;

namespace Scharff.Application.Queries.Security.GetAccessByUser
{
    public class GetAccessByUserHandler : IRequestHandler<GetAccessByUserQuery, List<ResponseAccess>>
    {
        private readonly Infrastructure.PostgreSQL.Queries.Security.GetAccessByUser.IGetAccessByUserQuery _GetAccessByUserQuery;

        public GetAccessByUserHandler(Infrastructure.PostgreSQL.Queries.Security.GetAccessByUser.IGetAccessByUserQuery GetAccessByUserQuery)
        {
            _GetAccessByUserQuery = GetAccessByUserQuery;
        }

        public async Task<List<ResponseAccess>> Handle(GetAccessByUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetAccessByUserQuery.GetAccessByUser(request.User_Email);
            List<ResponseAccess> lst = new List<ResponseAccess>();
            var lstAccess = result.GroupBy(e => (e.User_Code))
                                    .Select(group => group.First())
                                    .ToList();
            foreach (var access in lstAccess)
            {
                var objAccess = new ResponseAccess();
                objAccess.User_Code = access.User_Code;
                objAccess.Id_Role = access.Id_Role;
                objAccess.Role_Description = access.Role_Description;
                objAccess.Privilege = access.Privilege;
                objAccess.Privilege_Description = access.Privilege_Description;

                List<ResponseMenu> lstMenu = new List<ResponseMenu>();
                var lstFilter = result.Where(f => f.User_Code == objAccess.User_Code).ToList();
                foreach (var menu in lstFilter)
                {
                    var objMenu = new ResponseMenu();
                    objMenu.Id_Menu = menu.Id_Menu;
                    objMenu.Menu_Description = menu.Menu_Description;
                    objMenu.Icon = menu.Icon;
                    objMenu.Route = menu.Route;
                    objMenu.Id_System = menu.Id_System;
                    objMenu.System_Description = menu.System_Description;
                    lstMenu.Add(objMenu);
                }
                objAccess.lstMenu = lstMenu;
                lst.Add(objAccess);
            }

            if (!lst.Any())
            {
                throw new NotFoundException("El usuario no puede acceder al sistema.");
            }
            return lst;
        }
    }
}
