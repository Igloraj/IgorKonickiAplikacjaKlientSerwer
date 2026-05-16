using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices;

public class GroupService : BaseService, IGroupService
{
    private readonly UserManager<User> _userManager;

    public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        try
        {
            if (addOrUpdateGroupVm == null)
            {
                throw new ArgumentNullException(nameof(addOrUpdateGroupVm));
            }

            var group = Mapper.Map<Group>(addOrUpdateGroupVm);

            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
            {
                DbContext.Groups.Add(group);
            }
            else
            {
                DbContext.Groups.Update(group);
            }

            DbContext.SaveChanges();

            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        try
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            student.GroupId = attachStudentToGroupVm.GroupId;
            DbContext.SaveChanges();

            return Mapper.Map<StudentVm>(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
    {
        try
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == detachStudentToGroupVm.StudentId);

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            student.GroupId = null;
            DbContext.SaveChanges();

            return Mapper.Map<StudentVm>(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
    {
        try
        {
            var exists = DbContext.SubjectGroups.Any(sg =>
                sg.GroupId == attachSubjectGroupVm.GroupId &&
                sg.SubjectId == attachSubjectGroupVm.SubjectId);

            if (!exists)
            {
                DbContext.SubjectGroups.Add(new SubjectGroup
                {
                    GroupId = attachSubjectGroupVm.GroupId,
                    SubjectId = attachSubjectGroupVm.SubjectId
                });

                DbContext.SaveChanges();
            }

            var group = DbContext.Groups.FirstOrDefault(g => g.Id == attachSubjectGroupVm.GroupId);

            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectGroupVm)
    {
        try
        {
            var subjectGroup = DbContext.SubjectGroups.FirstOrDefault(sg =>
                sg.GroupId == detachSubjectGroupVm.GroupId &&
                sg.SubjectId == detachSubjectGroupVm.SubjectId);

            if (subjectGroup != null)
            {
                DbContext.SubjectGroups.Remove(subjectGroup);
                DbContext.SaveChanges();
            }

            var group = DbContext.Groups.FirstOrDefault(g => g.Id == detachSubjectGroupVm.GroupId);

            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachSubjectToTeacherVm)
    {
        try
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachSubjectToTeacherVm.SubjectId);

            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            subject.TeacherId = attachSubjectToTeacherVm.TeacherId;
            DbContext.SaveChanges();

            return Mapper.Map<SubjectVm>(subject);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachSubjectToTeacherVm)
    {
        try
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == detachSubjectToTeacherVm.SubjectId);

            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            subject.TeacherId = null;
            DbContext.SaveChanges();

            return Mapper.Map<SubjectVm>(subject);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
    {
        try
        {
            var group = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
    {
        try
        {
            var groups = DbContext.Groups.AsQueryable();

            if (filterPredicate != null)
            {
                groups = groups.Where(filterPredicate);
            }

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
