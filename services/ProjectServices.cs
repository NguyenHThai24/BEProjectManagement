using ProjectManagement.Data;
using ProjectManagement.Models;

namespace ProjectManagement.services
{
    public class ProjectServices
    {
        private readonly AppDbContext _context;

        public ProjectServices(AppDbContext context)
        {
            _context = context;
        }

        // Lấy project theo id
        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id)
                   ?? throw new CustomException(new ApiResponse(802, "Project not found."));
        }


        // Thêm Project
        public Project AddProject(Get_Project get, int userId)
        {
            if (userId <= 0)
            {
                throw new CustomException(new ApiResponse(601, "User not found."));
            }

            var newProject = new Project
            {
                NameProject = get.NameProject,
                DecriptionProject = get.DecriptionProject ?? "",
                UserId = userId
            };

            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return newProject;
        }


        public Project UpdateProject(int id, Get_Project get, int userId)
        {
            var existingProject = _context.Projects.Find(id);
            if (existingProject == null)
            {
                throw new CustomException(new ApiResponse(802, "Project not found."));
            }

            existingProject.NameProject = get.NameProject;
            existingProject.DecriptionProject = get.DecriptionProject ?? "";
            existingProject.UserId = userId;

            _context.SaveChanges();
            return existingProject;
        }


        // Xóa Project
        public void DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                throw new CustomException(new ApiResponse(802, "Project not found."));
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }


    }
}
