namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public List<Subject> Subjects { get; set; } = new();
    public string Title { get; set; } = null!;

    public Teacher()
    {
    }
}
