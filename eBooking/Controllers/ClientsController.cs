using AutoMapper;
using eBooking.Dtos;
using eBooking.Models;
using eBooking.Services;
using eBooking.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientsService _clientsService;
        private readonly IMapper _mapper;
        private readonly IApplicationBuilder _appBuilder;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        public ClientsController(IClientsService clientsService, IMapper mapper, SignInManager<Client> signInManager, UserManager<Client> userManager)
        {
            _clientsService = clientsService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateAsync([FromForm] ClientDto dto)
        //{
        //    var client = _mapper.Map<Client>(dto);
        //    _clientsService.Add(client);
        //    //]_context.Clients.AddAsync(client);

        //    return Ok(client);
        //}



        [HttpPost]

        public async Task<IActionResult> Login([FromBody] ClientLoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);

            var clientDto = new ClientDataDto();

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (passwordCheck)
                {
                    var result = _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

                    clientDto.Id = user.Id;
                    clientDto.FirstName = user.FirstName;
                    clientDto.UserName = user.UserName;

                }


                return Ok(clientDto);
            }
            return BadRequest();
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] ClientRegisterDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.EmailAddress);

            if (user != null)
            {
                return BadRequest();
            }

            var client = new Client()
            {
                UserName = "app-user",
                FirstName = dto.FirstName,
                LastName = dto.LastName,

                Email = dto.EmailAddress,
                NormalizedEmail = dto.EmailAddress,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(client, dto.PassWord);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var clients = await _clientsService.GetAll();

            var dto = _mapper.Map<IEnumerable<ClientDetailsDto>>(clients);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientByIdAsync(int id)
        {
            var client = await _clientsService.GetById(id);

            if (client == null)
            {
                return NotFound($"No client was found with id : {id}");
            }

            var dto = _mapper.Map<ClientDetailsDto>(client);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ClientDto dto)
        {
            var client = await _clientsService.GetById(id);

            if (client == null)
            {
                return BadRequest($"No room was found with id : {id}");
            }


            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;
            //client.Password = dto.Password;



            _clientsService.Update(client);

            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var client = await _clientsService.GetById(id);

            if (client == null)
            {
                return BadRequest($"No room was found with id : {id}");
            }

            _clientsService.Delete(client);

            return Ok(client);
        }


    }
}
