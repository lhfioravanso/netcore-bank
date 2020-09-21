using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Application.Security;


namespace Application.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AccessManager _accessManager;

        public UserController(IUserService userService, AccessManager accessManager){
            this._userService = userService;
            this._accessManager = accessManager;
        }
        
        [AllowAnonymous]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [AllowAnonymous]
        [HttpPost("/Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Authenticate([FromBody] LoginRequestDto dto)
        {
            try
            {
                LoginResponseDto user = this._userService.Login(dto);
                user.Token = this._accessManager.GenerateToken(user.Id).AccessToken;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}