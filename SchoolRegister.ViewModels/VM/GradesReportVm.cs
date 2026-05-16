namespace SchoolRegister.ViewModels.VM;

public class GradesReportVm
{
    public StudentVm? Student { get; set; }
    public IList<GradeVm> Grades { get; set; } = new List<GradeVm>();
}
