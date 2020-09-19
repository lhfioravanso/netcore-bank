using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService){
            this._userService = userService;
        }
        
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateUser([FromBody] CreateUserRequestDto dto)
        {
            try
            {
                CreateUserResponseDto user = this._userService.CreateUser(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetUser(int id)
        {
            try
            {
                UserResponseDto user = this._userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}