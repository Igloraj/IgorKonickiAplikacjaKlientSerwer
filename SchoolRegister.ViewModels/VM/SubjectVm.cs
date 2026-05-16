namespace SchoolRegister.ViewModels.VM;

public class SubjectVm
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IList<GroupVm> Groups { get; set; } = new List<GroupVm>();
    public string? TeacherName { get; set; }
    public int? TeacherId { get; set; }
}
