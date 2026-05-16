using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize]
public class SubjectController : BaseController
{
    private readonly ISubjectService _subjectService;

    public SubjectController(
        ISubjectService subjectService,
        ILogger<SubjectController> logger,
        IMapper mapper,
        IStringLocalizer localizer)
        : base(logger, mapper, localizer)
    {
        _subjectService = subjectService;
    }

    public IActionResult Index()
    {
        var subjects = _subjectService.GetSubjects();
        return View(subjects);
    }

    public IActionResult Details(int id)
    {
        var subject = _subjectService.GetSubject(x => x.Id == id);

        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    [Authorize(Roles = "Teacher,Admin")]
    public IActionResult Create()
    {
        return View(new AddOrUpdateSubjectVm { TeacherId = 1 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher,Admin")]
    public IActionResult Create(AddOrUpdateSubjectVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _subjectService.AddOrUpdateSubject(model);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Teacher,Admin")]
    public IActionResult Edit(int id)
    {
        var subject = _subjectService.GetSubject(x => x.Id == id);

        if (subject == null)
        {
            return NotFound();
        }

        var model = Mapper.Map<AddOrUpdateSubjectVm>(subject);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher,Admin")]
    public IActionResult Edit(AddOrUpdateSubjectVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _subjectService.AddOrUpdateSubject(model);
        return RedirectToAction(nameof(Index));
    }
}
