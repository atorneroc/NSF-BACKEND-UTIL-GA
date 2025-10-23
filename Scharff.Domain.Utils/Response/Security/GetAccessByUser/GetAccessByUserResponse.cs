namespace Scharff.Domain.Response.Security.GetAccessByUser
{
    public class GetAccessByUserResponse
    {
        public string User_Code { get; set; } = string.Empty;
        public string Role_Description { get; set; } = string.Empty;
        public int Id_Role { get; set; }
        public Boolean Privilege { get; set; }
        public string Privilege_Description { get; set; } = string.Empty;
        public int Id_Menu { get; set; }
        public string Menu_Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Id_System { get; set; }
        public string System_Description { get; set; } = string.Empty;
    }
    public class ResponseAccess
    {
        public string User_Code { get; set; } = string.Empty;
        public int Id_Role { get; set; }
        public string Role_Description { get; set; } = string.Empty;
        public Boolean Privilege { get; set; }
        public string Privilege_Description { get; set; } = string.Empty;
        public List<ResponseMenu>? lstMenu { get; set; }
    }
    public class ResponseMenu
    {
        public int Id_Menu { get; set; }
        public string Menu_Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Id_System { get; set; }
        public string System_Description { get; set; } = string.Empty;
    }
}