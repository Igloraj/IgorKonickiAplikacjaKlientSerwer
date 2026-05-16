using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public virtual Group? Group { get; set; }

    [ForeignKey("Group")]
    public int? GroupId { get; set; }

    public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Parent? Parent { get; set; }

    [ForeignKey("Parent")]
    public int? ParentId { get; set; }

    [NotMapped]
    public double AverageGrade
    {
        get
        {
            if (Grades == null || Grades.Count == 0)
            {
                return 0.0d;
            }

            return Math.Round(Grades.Average(g => (int)g.GradeValue), 1);
        }
    }

    [NotMapped]
    public IDictionary<string, double> AverageGradePerSubject
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
                .Select(g => new
                {
                    SubjectName = g.Key,
                    AvgGrade = Math.Round(g.Average(avg => (int)avg.GradeValue), 1)
                })
                .ToDictionary(avg => avg.SubjectName, avg => avg.AvgGrade);
        }
    }

    [NotMapped]
    public IDictionary<string, List<GradeScale>> GradesPerSubject
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
                .Select(g => new
                {
                    SubjectName = g.Key,
                    GradeList = g.Select(x => x.GradeValue).ToList()
                })
                .ToDictionary(x => x.SubjectName, x => x.GradeList);
        }
    }

    public Student()
    {
    }
}
