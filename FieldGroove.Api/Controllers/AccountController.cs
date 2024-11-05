using FieldGroove.Application.Interfaces;
using FieldGroove.Api.JwtAuthToken;
using FieldGroove.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldGroove.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly IUnitOfWork unitOfWork;
		public AccountController(IConfiguration configuration, IUnitOfWork unitOfWork)
		{
			this.configuration = configuration;
			this.unitOfWork = unitOfWork;
		}

		// Login Action in Api Controller

		[HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel entity)
		{
            if (ModelState.IsValid)
            {
				var isUser = await unitOfWork.UserRepository.IsValid(entity);
                if (isUser)
                {
					var JwtToken = new JwtToken(configuration);

					var response = new
					{
						User = entity.Email!,
						Token = JwtToken.GenerateJwtToken(entity.Email!),
                        Status = "OK",
						Timestamp = DateTime.Now
					};
                    return Ok(response);
                }
                return NotFound("User Not Found");
            }
            return BadRequest();
        }

		// Register Action in Api Controller

		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterModel entity)
		{
            if (ModelState.IsValid)
			{
				var isUser = await unitOfWork.UserRepository.IsRegistered(entity);
                if (!isUser)
				{
					bool response =await unitOfWork.UserRepository.Create(entity);
					return response? Ok(): StatusCode(500, "An internal server error occurred.");
				}
				return Conflict(new { error = "User already registered" });
			}
			return BadRequest(entity);
		 }
	}
}
