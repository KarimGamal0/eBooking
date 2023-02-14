using AutoMapper;
using eBooking.Dtos;
using eBooking.Models;
using eBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsService _hotelsService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        //public List<string> _allowedExtensions = new List<string>() { ".jpg", ".png" };
        //public long _maxAllowedImageSize = 1048576;

        public HotelsController(IHotelsService hotelService, IRoomService roomService, IMapper mapper)
        {
            _hotelsService = hotelService;
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var hotels = await _hotelsService.GetAll();
            var data = _mapper.Map<IEnumerable<HotelDetailsDto>>(hotels);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        { 
            var hotel = await _hotelsService.GetById(id);

            if (hotel == null)
            {
                return NotFound($"No hotel was found with id :{id}");
            }

            var dto = _mapper.Map<HotelDetailsDto>(hotel);

            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);

            await _hotelsService.Add(hotel);

            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] HotelUpdateDto dto)
        {

            var hotel = await _hotelsService.GetById(id);

            if (hotel == null)
            {
                return NotFound($"No hotel was found with id : {id}");
            }

            hotel.Name = dto.Name;
            hotel.Location = dto.Location;
            hotel.ImageUrl= dto.ImageUrl;

            _hotelsService.Update(hotel);

            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var hotel = await _hotelsService.GetById(id);

            if (hotel == null)
            {
                return NotFound($"No hotel was found with id : {id}");
            }

            _hotelsService.Delete(hotel);

            return Ok(hotel);
        }

    }
}
