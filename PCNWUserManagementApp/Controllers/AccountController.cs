using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCNWUserManagementApp.Data;
using PCNWUserManagementApp.Models;
using PCNWUserManagementApp.ViewModels;

namespace PCNWUserManagementApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly PCNWUserManagementAppContext _context;

		public AccountController(PCNWUserManagementAppContext context,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager,RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_signInManager = signManager;
			_context = context;
		}
		public IActionResult Index()
		{
			var rolall=new List<string>();
			var USERS = _context.User.ToList();

			var Roles=_roleManager.Roles.ToList();
			foreach (var role in Roles)
			{
				rolall.Add(role.Name);

				}
			ViewBag.Roles = Roles;
			return View();
		}
		public IActionResult CreateRole()
		{
			return View();
		}
		[HttpPost]
		public async Task <IActionResult> CreateRole(string roleName)
		{
			if(!string.IsNullOrEmpty(roleName))
			{
				var exist =await _roleManager.RoleExistsAsync(roleName);
				if (!exist)
				{
					var result = await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
					if(result.Succeeded)
					{
						return RedirectToAction("Index");
                    }
				}
			}
			return View();

		}

		public async Task<IActionResult> Register()
		{

			ViewBag.roles = await _roleManager.Roles.ToListAsync();
			return View();

		}
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model, IFormFile ImagePath, IFormFile projectFile)
        {
            if (ModelState.IsValid)
            {
                // Process uploaded image
                
				IdentityUser iduser = new IdentityUser()
				{
					Email = model.Email,
					
					UserName = model.Email,
					PhoneNumber = model.Contact,
				};

				var status=await _userManager.CreateAsync(iduser,model.Password);
				if(status.Succeeded)
				{ 

				User modelUser = new User()
				{
					UserName = model.UserName,
					Email = model.Email,
					Password = model.Password,
					Address = model.Address,
					Gender = model.Gender,
					ContactNo = model.Contact,
					Name = model.Name,
					UserType = model.UserType,
					ProjectName = model.ProjectName,
					ImagePath = ImagePath.FileName,
					projectFile=projectFile.FileName,
					IsActive = "Y",
					LastUpdated=DateTime.Now,
				};
					_context.User.Add(modelUser);
				    _context.SaveChanges();
                }
                if (ImagePath != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImagePath.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImagePath.CopyToAsync(stream);
                    }
                    model.ImagePath = "/images/" + ImagePath.FileName;
                }

                // Process project file
                if (projectFile != null)
                {
                    var projectFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/projects", projectFile.FileName);
                    using (var stream = new FileStream(projectFilePath, FileMode.Create))
                    {
                        await projectFile.CopyToAsync(stream);
                    }
                    model.projectFile = "/projects/" + projectFile.FileName;
                }


                // Save user to the database, e.g., using _userManager or your user service

                return RedirectToAction("Index", "Home");
            }
            ViewBag.roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }

    }
}
