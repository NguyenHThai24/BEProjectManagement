namespace ProjectManagement.Models
{
    public class Project
    {

        public int Id { get; set; }


        public string NameProject { get; set; }

        public string DecriptionProject { get; set; }

        public int UserId { get; set; }
    }


    public record Get_Project(
       string NameProject,
       string? DecriptionProject
   );
}
