using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebFormApp.BLL.DTOs
{
    public class UserAndRoleDTO
    {
        public IEnumerable<UserDTO> listUser { get; set; }
        public IEnumerable<RoleDTO> listRole { get; set; }
    }
}
