using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")] // api/auth
    [ApiController] // infers where data is coming from without [FromBody]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn =  _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var userToReturn =  _mapper.Map<UserForDetailedDto>(user);
                return Ok(userToReturn);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser( int id, UserForUpdateDto userForUpdateDto)
        {
            // this is the code that we use to make sure the user can only update their profile and no one elses
            if ( id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) )
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(userForUpdateDto, userFromRepo);

            if( await _repo.SaveAll() )
            {
                return NoContent();
            }

            throw new Exception($"Updating user {id} failed on save");
        }



    }
}