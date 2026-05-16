namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; } = null!;
    public int GroupId { get; set; }

    public List<Grade> Grades { get; set; } = new();

    public Parent Parent { get; set; } = null!;
    public int ParentId { get; set; }

    public double AverageGrade
    {
        get
        {
            if (Grades == null || Grades.Count == 0)
            {
                return 0;
            }

            return Grades.Average(g => (double)g.GradeValue);
        }
    }

    public Dictionary<string, double> AverageGradePerSubject
    {
        get
        {
            if (Grades == null || Grades.Count == 0)
            {
                return new Dictionary<string, double>();
            }

            return Grades
                .Where(g => g.Subject != null)
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(x => (double)x.GradeValue)
                );
        }
    }

    public Dictionary<string, List<GradeScale>> GradesPerSubject
    {
        get
        {
            if (Grades == null || Grades.Count == 0)
            {
                return new Dictionary<string, List<GradeScale>>();
            }

            return Grades
                .Where(g => g.Subject != null)
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.GradeValue).ToList()
                );
        }
    }

    public Student()
    {
    }
}
