using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.services;
using ProjectManagement.Models;
using System.Reflection.Metadata;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectManagement.Migrations;


namespace ProjectManagement.Controllers
{
    [Authorize]
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private int GetUserFromToken()
        {
            var token = Request.Cookies["AuthToken"]; // Lấy token từ cookie
            if (string.IsNullOrEmpty(token))
            {
                throw new CustomException(new ApiResponse(803, "Unauthorized: Token not found."));
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Lấy userId từ claim
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Console.WriteLine($"User ID from Token: {userIdClaim}");

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new CustomException(new ApiResponse(498, "Unauthorized: Invalid token"));
            }

            return userId; // Trả về userId dưới dạng int
        }




        private readonly ProjectServices _projectService;

        public ProjectController(ProjectServices projectService)
        {
            _projectService = projectService;
        }

        // thêm Project
        [HttpPost("add")]
        public IActionResult AddProject([FromBody] Get_Project get)
        {
            try
            {
                var userId = GetUserFromToken(); // Lấy id người thực hiện
                var project = _projectService.AddProject(get, userId);
                var data = new
                {
                    NameProject = project.NameProject,
                    DecriptionProject = project.DecriptionProject,
                    UserId = project.UserId
                };
                return Ok(new ApiResponse(201, "Project added successfully", data));
            }


            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Get_Project get)
        {
            try
            {
                var userId = GetUserFromToken();
                var project = _projectService.UpdateProject(id, get, userId);
                var data = new
                {
                    NameProject = project.NameProject,
                    DecriptionProject = project.DecriptionProject,
                    UserId = project.UserId
                };
                return Ok(new ApiResponse(200, "Project updated successfully", data));
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }


        // xóa Project

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                //Console.WriteLine($"Request to delete project with ID: {id}");

                var userId = GetUserFromToken();
                var project = _projectService.GetProjectById(id);

                if (project == null)
                {
                    //Console.WriteLine("Project not found!");
                    return NotFound(new ApiResponse(802, "Project not found."));
                }

                if (project.UserId != userId)
                {
                    //Console.WriteLine("Permission denied!");
                    throw new CustomException(new ApiResponse(403, "You do not have permission to delete this project."));
                }

                _projectService.DeleteProject(id);
                // Console.WriteLine("Project deleted successfully.");
                return Ok(new ApiResponse(200, "Project deleted successfully"));
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }


    }
}
