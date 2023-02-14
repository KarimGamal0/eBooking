using HotelManagment.Models;
using HotelManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Text;

namespace HotelManagment.Controllers
{
    
    public class HotelsController : Controller
    {
        string Baseurl;
        HttpClient client;

        public HotelsController()
        {
            client = new HttpClient();
            Baseurl = "https://localhost:7193/api";
        }

        public async Task<IActionResult> Index()
        {
            List<Hotel> HotelsInfo = new List<Hotel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync("api/Hotels");
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    HotelsInfo = JsonConvert.DeserializeObject<List<Hotel>>(HotelResponse);
                }

                return View(HotelsInfo);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(hotel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/hotels";

                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(hotel);
                }
            }
        }


        public async Task<IActionResult> Edit(int id)
        {

            var hotel = new Hotel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/hotels/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsAsync<Hotel>();

                    hotel = HotelResponse.Result;
                }

            }
            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Hotel hotel)
        {

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(hotel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/hotels/" + id.ToString();

                HttpResponseMessage response = await client.PutAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(hotel);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.DeleteAsync(Baseurl + "/Hotels/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsAsync<Hotel>();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
        }
    }
}