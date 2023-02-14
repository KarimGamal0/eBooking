using AutoMapper;
using eBooking.Dtos;
using eBooking.Models;
using eBooking.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IRoomService _roomsService;
        private readonly IHotelsService _hotelsService;
        private readonly IMapper _mapper;

        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public RoomsController(ApplicationDbContext context, IRoomService roomsService, IHotelsService hotelsService, UserManager<Client> userManager, IMapper mapper)
        {
            _context = context;
            _roomsService = roomsService;
            _hotelsService = hotelsService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            room.HotelId = dto.HotelId;

            _roomsService.Add(room);

            return Ok(room);
        }

        [HttpPut("Reserve")]
        public async Task<IActionResult> Reserve(int roomId, [FromBody] ReserveDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == dto.ClientId);
            if (user != null)
            {
                string name = user.Email;
                user.IsCheckedBefore = true;
                await _userManager.UpdateAsync(user);
            }

            var room = await _roomsService.GetById(roomId);

            if (room == null)
            {
                return BadRequest($"No room was found with id : {roomId}");
            }


            room.Availabilty = dto.Availabilty;
            room.ClientId = dto.ClientId;
            if ((room.RoomType == Enum.RoomType.Double || room.RoomType == Enum.RoomType.Double) && room.RoomCounter <= 2)
            {
                room.RoomCounter++;
            }

            _roomsService.Update(room);

            return Ok(room);
        }

        [HttpPut("Cancel")]
        public async Task<IActionResult> Cancel(int roomId, [FromBody] ReserveDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == dto.ClientId);
            
            var room = await _roomsService.GetById(roomId);

            if (room == null)
            {
                return BadRequest($"No room was found with id : {roomId}");
            }


            room.Availabilty = dto.Availabilty;
            room.ClientId = dto.ClientId;
            if ((room.RoomType == Enum.RoomType.Double || room.RoomType == Enum.RoomType.Double) && room.RoomCounter > 0)
            {
                room.RoomCounter--;
            }

            _roomsService.Update(room);

            return Ok(room);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var rooms = await _roomsService.GetAll();

            var dto = _mapper.Map<IEnumerable<RoomDetailsDto>>(rooms);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomByIdAsync(int id)
        {

            var room = await _roomsService.GetById(id);

            if (room == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<RoomDetailsDto>(room);

            return Ok(dto);
        }


        [HttpGet("GetByHotelId")]
        public async Task<IActionResult> GetAllRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _roomsService.GetAllRoomsByHotelId(hotelId);

            if (rooms == null)
            {
                return NotFound($"No rooms was found with hotel Id: {hotelId}");
            }

            return Ok(rooms);
        }

        [HttpGet("GetByClientlId")]
        public async Task<IActionResult> GetAllRoomsByClientIdAsync(int clientId)
        {
            var rooms = await _roomsService.GetAllRoomsByClientId(clientId);

            if (rooms == null)
            {
                return NotFound($"No rooms was found with hotel Id: {clientId}");
            }

            return Ok(rooms);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] RoomUpdateDto dto)
        {
            var room = await _roomsService.GetById(id);

            if (room == null)
            {
                return BadRequest($"No room was found with id : {id}");
            }

            room.StartDate = dto.StartDate;
            room.EndDate = dto.EndDate;
            room.RoomType = dto.RoomType;
            room.Level = dto.Level;
            room.Availabilty = dto.Availabilty;

            _roomsService.Update(room);

            return Ok(room);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var room = await _roomsService.GetById(id);

            if (room == null)
            {
                return BadRequest($"No room was found with id : {id}");
            }

            _roomsService.Delete(room);

            return Ok(room);
        }
    }
}
