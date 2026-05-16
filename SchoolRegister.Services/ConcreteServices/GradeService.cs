using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class GradeService : BaseService, IGradeService
{
    private readonly UserManager<User> _userManager;

    public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            var teacher = DbContext.Users
                .OfType<Teacher>()
                .FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);

            if (teacher == null)
            {
                throw new ArgumentException("Teacher not found");
            }

            var isTeacher = _userManager.IsInRoleAsync(teacher, "Teacher").Result;

            if (!isTeacher)
            {
                throw new UnauthorizedAccessException("User is not a teacher");
            }

            var grade = new Grade
            {
                StudentId = addGradeToStudentVm.StudentId,
                SubjectId = addGradeToStudentVm.SubjectId,
                GradeValue = addGradeToStudentVm.GradeValue,
                DateOfIssue = DateTime.Now
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
    {
        try
        {
            var student = DbContext.Users
                .OfType<Student>()
                .FirstOrDefault(s => s.Id == getGradesVm.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var getterUser = DbContext.Users.FirstOrDefault(u => u.Id == getGradesVm.GetterUserId);

            if (getterUser == null)
            {
                throw new ArgumentException("Getter user not found");
            }

            var isTeacher = _userManager.IsInRoleAsync(getterUser, "Teacher").Result;
            var isStudent = _userManager.IsInRoleAsync(getterUser, "Student").Result;
            var isParent = _userManager.IsInRoleAsync(getterUser, "Parent").Result;

            var canRead =
                isTeacher ||
                (isStudent && getterUser.Id == student.Id) ||
                (isParent && student.ParentId == getterUser.Id);

            if (!canRead)
            {
                throw new UnauthorizedAccessException("User cannot read grades");
            }

            var grades = DbContext.Grades
                .Where(g => g.StudentId == getGradesVm.StudentId)
                .ToList();

            return new GradesReportVm
            {
                Student = Mapper.Map<StudentVm>(student),
                Grades = Mapper.Map<IList<GradeVm>>(grades)
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
