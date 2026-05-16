using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces;

public interface ITeacherService
{
    TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate);
    IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterPredicate = null);
    IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups);
}
