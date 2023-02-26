using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Business.ViewModels;
using MultiShop.Core.Entities;
using MultiShop.Utilities;
using System.Net;
using System.Net.Mail;

namespace MultiShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration _config;
        public AuthController(IWebHostEnvironment env, UserManager<AppUser> userManager, IServiceProvider serviceProvider, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _env = env;
            _userManager = userManager;
            this.serviceProvider = serviceProvider;
            _signInManager = signInManager;
            _config = config;
        }
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new()
            {
                UserName = "Admin",
                Email = "Admin@gmail.com",
                ProfilePhoto = "Default/DefaultProfile.jpg"

            };
            var identityResult = await _userManager.CreateAsync(admin, "Admin123.");
            if (!identityResult.Succeeded)
            {
                string rslt = string.Empty;
                foreach (var error in identityResult.Errors)
                {
                    rslt += error.Description + ",";
                }
                ModelState.AddModelError("Password", rslt);
                return BadRequest(rslt);
            }
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Memmber.ToString()))
            {
                var role = new IdentityRole(Roles.Memmber.ToString());
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
            {
                var role = new IdentityRole(Roles.Moderator.ToString());
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                var role = new IdentityRole(Roles.Admin.ToString());
                await roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var imageUrl = string.Empty;
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            if (registerVM.ProfilePhoto == null)
            {
                string[] route = new string[] { "uploads", "Users", "Default", "DefaultProfile.jpg" };
                foreach (var routeItem in route)
                {
                    imageUrl = Path.Combine(imageUrl, routeItem);
                }
            }
            if (registerVM.ProfilePhoto != null)
            {
                imageUrl = await registerVM.ProfilePhoto.SaveFileAsync(_env.WebRootPath, "wwwroot", "uploads", "Users");
            }
            AppUser appUser = new()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                ProfilePhoto = imageUrl
            };
            var identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                string rslt = string.Empty;
                foreach (var error in identityResult.Errors)
                {
                    rslt += error.Description + ",";
                }
                ModelState.AddModelError("Password", rslt);
                return View(registerVM);
            }
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Memmber.ToString()))
            {
                var role = new IdentityRole(Roles.Memmber.ToString());
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
            {
                var role = new IdentityRole(Roles.Moderator.ToString());
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                var role = new IdentityRole(Roles.Admin.ToString());
                await roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(appUser, Roles.Memmber.ToString());
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("UsernameOrEmail", "Password or Username is incorrect");
                    return View(loginVM);
                }
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.rememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "many false tries");
                return View(loginVM);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("UsernameOrEmail", "Password or Username is incorrect");
                return View(loginVM);
            }
            if (user.IsActive)
            {
                ModelState.AddModelError("", "not found");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordVM);
            }
            if (forgotPasswordVM.Email == null)
            {
                return BadRequest("email not found!");
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (user == null)
            {
                return BadRequest("user not found!");

            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

           
            var resetPasswordUrl = Url.Action("ResetPassword", "Auth", new { token, email = user.Email }, Request.Scheme);

            var smtpClient = new SmtpClient(_config.GetValue<string>("MailSettings:port"), _config.GetValue<int>("MailSettings:port"))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.GetValue<string>("MailSettings:Mail"), _config.GetValue<string>("MailSettings:Verification")),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("testtestov123dot@gmail.com"),
                Subject = "Reset Password",
                Body = "Click on this link to reset your password: " + resetPasswordUrl
            };
            mailMessage.To.Add(new MailAddress(user.Email));

            smtpClient.Send(mailMessage);
            return Content("Check your Email.");
        }
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token.");
                return View();
            }

            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Don't reveal that the user does not exist");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return BadRequest("dont work");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

    }
}
