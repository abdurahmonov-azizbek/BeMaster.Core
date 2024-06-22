// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Application.Common.Identity;
using BeMaster.Domain.Common.Filters;
using BeMaster.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] SearchOptions searchOptions)
        {
            var result = userService.Get(searchOptions);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await userService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var result = await userService.CreateAsync(user);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            var result = await userService.UpdateAsync(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var result = await userService.DeleteByIdAsync(id);

            return Ok(result);
        }
    }
}
