using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.DAL;

namespace SampleMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBLL _userBLL;
        private readonly IRoleBLL _roleBLL;

        public UsersController(IUserBLL userBLL,IRoleBLL roleBLL) 
        { 
            _userBLL = userBLL;
            _roleBLL = roleBLL;
        }
        public IActionResult Index()
        {
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			var users = _userBLL.GetAll();
			var listUsers = new SelectList(users, "Username", "Username");
			ViewBag.Users = listUsers;

			var roles = _roleBLL.GetAllRoles();
			var listRoles = new SelectList(roles, "RoleID", "RoleName");
			ViewBag.Roles = listRoles;

			var usersWithRoles = _userBLL.GetAllWithRoles();
			return View(usersWithRoles);
		}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserCreateDTO userCreateDto) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _userBLL.Insert(userCreateDto);
                ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
            }

            return View();
        }
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var userdto = _userBLL.LoginMVC(loginDTO);
                var userDtoSerialize = JsonSerializer.Serialize(userdto);
                HttpContext.Session.SetString("user", userDtoSerialize);
                TempData["Message"] = "Welcome " + userdto.Username;
                ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
            }

            return RedirectToAction("Index", "Home");

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }

        public IActionResult Profile() 
        {
            var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
            return View(userDto);
        }
        public IActionResult AddUserRole() 
        {
            var viewModel = new UserAndRoleDTO()
            {
                listUser = _userBLL.GetAll(),
                listRole = _roleBLL.GetAllRoles()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddUserRole(string Username, int RoleId )
        {
            _roleBLL.AddUserToRole(Username, RoleId);
            return RedirectToAction("Index");
        }
        public IActionResult AddRole() 
        { 
        return View();
        }
        [HttpPost]
		public IActionResult AddRole(string RoleName)
		{
            _roleBLL.AddRole(RoleName);
			return RedirectToAction("Index");
		}
	}
}
