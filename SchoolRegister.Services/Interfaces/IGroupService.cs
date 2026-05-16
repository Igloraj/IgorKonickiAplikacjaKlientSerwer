using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces;

public interface IGroupService
{
    GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
    StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm);
    StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm);
    GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm);
    GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectGroupVm);
    SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachSubjectToTeacherVm);
    SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachSubjectToTeacherVm);
    GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate);
    IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null);
}
