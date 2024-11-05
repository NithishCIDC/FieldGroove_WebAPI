using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using FieldGroove.Api.Interfaces;

namespace FieldGroove.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController(IUnitOfWork unitOfWork) : ControllerBase
	{

		//Leads Action in Api Controller

		[HttpGet("Leads")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Leads()
		{
			var User = await unitOfWork.LeadsRepository.GetAll();
            var response = new 
            {
                Data = User,
                TotalCount = User.Count,
                Status = "success",
                Timestamp = DateTime.UtcNow.ToString("o")
            };
            return Ok(response);
		}

		[HttpGet("Leads/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Leads(int id)
		{
			var User = await unitOfWork.LeadsRepository.GetById(id);
            return Ok(User);
		}

		//CreateLead Action in Api Controller

		[HttpPost("CreateLead")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateLead([FromBody] LeadsModel model)
		{
			if (ModelState.IsValid)
			{
				await unitOfWork.LeadsRepository.Create(model);
				return Ok();
			}
			return BadRequest();
		}

		//EditLead Action in Api Controller

		[HttpPut("EditLead")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> EditLead([FromBody] LeadsModel model)
		{
			if (ModelState.IsValid)
			{
				if (await unitOfWork.LeadsRepository.isAny((int)model.Id!))
				{
                    await unitOfWork.LeadsRepository.Update(model);
					return Ok(model);
				}
				return NotFound();
			}
			return BadRequest();
		}

		// Delete Action in Api Controller 

		[HttpDelete("DeleteLead/{id:int}")]
		public async Task<IActionResult> DeleteLead(int id)
		{
			var response = await unitOfWork.LeadsRepository.GetById(id);
			if (response is not null)
			{
				await unitOfWork.LeadsRepository.Delete(response);
				return Ok(response);
			}
			return NotFound();
		}

		// Download the CSV file 

		[HttpGet("download-csv")]
		public async Task<IActionResult> DownloadCsv()
		{
			var records = await unitOfWork.LeadsRepository.GetAll();

			var csv = new StringBuilder();
			csv.AppendLine("ID,Project Name,Status,Added,Type,Contact,Action,Assignee,Bid Date");

			foreach (var record in records)
			{
				csv.AppendLine($"{record.Id},{record.ProjectName},{record.Status},{record.Added},{record.Type},{record.Contact},{record.Action},{record.Assignee},{record.BidDate}");
			}

			byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
			return File(buffer, "text/csv", "LeadsData.csv");
		}
	}
}
