using HotelManagment.Models;
using HotelManagment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Text;

namespace HotelManagment.Controllers
{
    public class AccountController : Controller
    {


        static string UserName = "";
        string Baseurl = "https://localhost:7193/api";
        public IActionResult Login()
        {
            return View(new LoginVM());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(login);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Clients";

                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    var clientResponse = response.Content.ReadAsStringAsync().Result;
                    var clientInfo = JsonConvert.DeserializeObject<Client>(clientResponse);
                    AccountInfo.UserId = clientInfo.Id;
                    AccountInfo.UserName = clientInfo.FirstName;
                    AccountInfo.IsAuthenticated = true;
                    AccountInfo.IsAdmin = clientInfo.UserName;
                    //AccountInfo.IsCheckedBefore = clientInfo.IsCheckedBefore;
                    return RedirectToAction("Index", "Hotels");
                }
                else
                {
                    return View(login);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            AccountInfo.IsAuthenticated = false;
            AccountInfo.UserName = "";
            AccountInfo.IsAdmin = "app-user";
            AccountInfo.IsCheckedBefore = false;

            return RedirectToAction("Index", "Hotels");
        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(registerVM);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Clients/Register";

                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    var clientResponse = response.Content.ReadAsStringAsync().Result;
                    var clientInfo = JsonConvert.DeserializeObject<Client>(clientResponse);

                    //return RedirectToAction("Index", "Hotels");
                    return View("RegisterCompleted");
                }
                else
                {
                    return View(registerVM);
                }
            }


        }
    }
}
