using GLIMPSE.Domain.Models;

namespace GLIMPSE.DOMAIN.ViewModels;

public class ProjectsVM
{
    public User? User { get; set; }
    public List<Project>? Projects { get; set; }
    public Dictionary<Project, User>? ActiveProjects { get; set; }
}