using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace RehearsalBase.Controllers
{
    public class AccountController : Controller
    {
        private readonly PostgreDbContext _context;
        public AccountController(PostgreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            ViewBag.ServerError = "";
            return View(new Customer());
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string email, string password, string name)
        {
            Customer newCustomer = new Customer()
            {
                CustomerEmail = email,
                CustomerName = name,
                CustomerPassword = HashPassword(password)
            };

            var validationMessage = await ValidateFormAsync(email, password, name);
            if (validationMessage != "")
            {
                ViewBag.ServerError = validationMessage;
                return View(newCustomer);
            }
            
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            await CookieAuthenticate(email, name);
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private async Task CookieAuthenticate(string email, string name)
        {
            var nameClaim = new Claim(ClaimTypes.Name, name);
            var emailClaim = new Claim(ClaimTypes.Email, email);
            var roleClaim = new Claim(ClaimTypes.Role, "User");

            var claims = new List<Claim> { nameClaim, emailClaim, roleClaim };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        private async Task<string> ValidateFormAsync(string email, string password, string repeatPassword = "", string name = "")
        {
            if (email == null || password == null || repeatPassword == null || name == null)
            {
                return "Заполните все поля!";
            }
            if (!EmailIsValid(email))
            {
                return "Почта некорректна!";
            }
            if (!PasswordIsValid(password))
            {
                return "Пароль некорректен!";
            }

            if (name == "" && repeatPassword == "")
            {
                if (password != repeatPassword)
                {
                    return "Пароли не совпадают!";
                }
                var customer = await GetCustomerByEmailAsync(email);
                if (customer == null)
                {
                    return "Пользователь с такой почтой не зарегистрирован!";
                }
                if (HashPassword(password) != customer.CustomerPassword)
                {
                    return "Пароль неверен!";
                }
            }
            else
            {
                if (!NameIsValid(name))
                {
                    return "Имя некорректно!";
                }

                if (await GetCustomerByEmailAsync(email) != null)
                {
                    return "Пользователь с такой почтой уже зарегистрирован!";
                }

            }
            return "";
        }
        private bool NameIsValid(string name)
        {
            return name.All(char.IsLetter);
        }
        private bool EmailIsValid(string email)
        {
            var regex = new Regex(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, new TimeSpan(2000));
            return regex.IsMatch(email);
        }
        private bool PasswordIsValid(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{10,}$"); 
        }

        [NonAction]
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes);//.Replace("-", "").ToLower();
                return hash;
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ServerError = "";
            return View(new Customer());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string repeatPassword)
        {
            var validationMessage = await ValidateFormAsync(email, password);
            if (validationMessage != "")
            {
                ViewBag.ServerError = validationMessage;
                return View(new Customer() { CustomerEmail = email, CustomerPassword = password });
            }
            var customer = await GetCustomerByEmailAsync(email);
            await CookieAuthenticate(email, customer.CustomerName);

            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        [NonAction]
        private async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.CustomerEmail == email);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
