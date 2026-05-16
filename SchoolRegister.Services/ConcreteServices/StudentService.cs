using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices;

public class StudentService : BaseService, IStudentService
{
    public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        : base(dbContext, mapper, logger)
    {
    }

    public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
    {
        try
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(filterPredicate);
            return Mapper.Map<StudentVm>(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filterPredicate = null)
    {
        try
        {
            var students = DbContext.Users.OfType<Student>().AsQueryable();

            if (filterPredicate != null)
            {
                students = students.Where(filterPredicate);
            }

            return Mapper.Map<IEnumerable<StudentVm>>(students);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
