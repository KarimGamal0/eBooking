using HotelManagment.Enum;
using HotelManagment.Models;
using HotelManagment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace HotelManagment.Controllers
{
    public class RoomsController : Controller
    {


        string Baseurl;
        HttpClient client;

        static int ID = 0;

        public RoomsController()
        {
            client = new HttpClient();
            Baseurl = "https://localhost:7193/api";
        }

        public async Task<IActionResult> Index(int id)
        {
            int Hotelid = Convert.ToInt32(TempData["ID"]);

            ID = id;

            if (id == 0)
            {
                id = Convert.ToInt32(TempData["ID"]);

            }
            else
            {
                TempData["ID"] = id;
            }
            //https://localhost:7193/api/Rooms/GetByHotelId?hotelId=6

            List<Room> roomsInfo = new List<Room>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync("api/Rooms/GetByHotelId?hotelId=" + ID);
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    roomsInfo = JsonConvert.DeserializeObject<List<Room>>(HotelResponse);
                }

                return View(roomsInfo);
            }
        }


        public async Task<IActionResult> Create()
        {
            int Hotelid = Convert.ToInt32(TempData["ID"]);

            TempData["ID"] = Hotelid;

            var hotel = new Hotel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/hotels/" + ID);

                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsAsync<Hotel>();

                    hotel = HotelResponse.Result;
                }


            }

            Room room = new Room();
            room.HotelId = Hotelid;
            room.Hotel = hotel;

            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoomVM room)
        {
            room.HotelId = ID;

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(room);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Rooms";

                HttpResponseMessage response = await client.PostAsync(url, data);


                if (response.IsSuccessStatusCode)
                {
                    var RoomResponse = response.Content.ReadAsAsync<NewRoomVM>();
                    return RedirectToAction("Index", "Rooms", new { id = ID });
                }
                else
                {
                    return View(room);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Room room)
        {

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(room);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Rooms/" + id.ToString();

                HttpResponseMessage response = await client.PutAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Rooms", new { id = ID });
                }
                else
                {
                    return View(room);
                }
            }
        }

        public async Task<IActionResult> Reserve(int id)
        {
            var room = new Room();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/rooms/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var RoomResponse = Res.Content.ReadAsAsync<Room>();

                    room = RoomResponse.Result;
                    room.Availabilty = RoomAvailabilty.Reserved;
                    room.clientId = AccountInfo.UserId;
                    AccountInfo.IsCheckedBefore= true;
                }
            }

            

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(room);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Rooms/Reserve?roomId=" + id;

                HttpResponseMessage response = await client.PutAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Rooms", new { id = ID });
                }
                else
                {
                    return View("Error");
                }
            }
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var room = new Room();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/rooms/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var RoomResponse = Res.Content.ReadAsAsync<Room>();

                    room = RoomResponse.Result;
                    room.Availabilty = RoomAvailabilty.Available;
                    room.clientId = 0;
                }
            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(room);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = Baseurl + "/Rooms/Cancel?roomId=" + id;

                HttpResponseMessage response = await client.PutAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ClientRooms", "Rooms");
                }
                else
                {
                    return View("Error");
                }
            }
        }


        public async Task<IActionResult> Edit(int id)
        {

            var room = new Room();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/Rooms/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var RoomResponse = Res.Content.ReadAsAsync<Room>();

                    room = RoomResponse.Result;
                }

            }
            return View(room);
        }

        public async Task<IActionResult> Delete(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.DeleteAsync(Baseurl + "/Rooms/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var RoomResponse = Res.Content.ReadAsAsync<Room>();
                    TempData["ID"] = RoomResponse.Result.HotelId;
                    return RedirectToAction("Index", "Rooms", new { id = ID });
                }
                else
                {
                    return View();
                }

            }
        }

        
        public async Task<IActionResult> ClientRooms()
        {
            List<Room> roomsInfo = new List<Room>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync("api/Rooms/GetByClientlId?clientId=" + AccountInfo.UserId);
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    roomsInfo = JsonConvert.DeserializeObject<List<Room>>(HotelResponse);
                    if(roomsInfo.Count > 0)
                    {
                        AccountInfo.IsCheckedBefore= true;
                    }
                }

                return View(roomsInfo);
            }
        }

    }
}
