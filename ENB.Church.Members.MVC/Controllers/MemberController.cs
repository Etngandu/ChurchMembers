using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Church.Members.Entities;
using ENB.Church.Members.Entities.Repositories;
using ENB.Church.Members.Infrastructure;
using ENB.Church.Members.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ENB.Church.Members.MVC.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MemberController> _logger;
        private readonly IAsyncMemberRepository  _asyncMemberRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditMember> _validator;
        public MemberController(IMapper mapper, ILogger<MemberController> logger,
                                   IAsyncMemberRepository asyncMemberRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditMember> validator)
        {
            _mapper = mapper;
            _logger = logger;
           _asyncMemberRepository = asyncMemberRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMemberData()
        {
            IQueryable<Member> allMember = _asyncMemberRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayMember>>(allMember).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: MemberController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Member not found");

            Member dbMember = await _asyncMemberRepository.FindById(id);

            ViewBag.Message = dbMember.FullName;

            _logger.LogInformation($"Details of Member: {ViewBag.Message}");

            if (dbMember is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayMember>(dbMember);

            return View(data);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMember createAndEditMember)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditMember);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditMember);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Member dbMember = new();

                    _mapper.Map(createAndEditMember, dbMember);
                    await _asyncMemberRepository.Add(dbMember);

                    _notyf.Success("Member Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }

        }

        // GET: MemberController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Member {id} not found");

            Member dbMember = await _asyncMemberRepository.FindById(id);

            if (dbMember is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditMember>(dbMember));

            return View(data);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditMember createAndEditMember)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Member dbMemberToUpdate = await _asyncMemberRepository.FindById(createAndEditMember.Id);

            //            _mapper.Map(createAndEditMember, dbMemberToUpdate, typeof(CreateAndEditMember), typeof(Member));

            //             _notyf.Success("Member Update  Successfully! ");

            //            return RedirectToAction(nameof(Index));
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //    }
            //}
            //return View();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditMember);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditMember);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Member dbMemberToUpdate = await _asyncMemberRepository.FindById(createAndEditMember.Id);

                    _mapper.Map(createAndEditMember, dbMemberToUpdate, typeof(CreateAndEditMember), typeof(Member));

                    _notyf.Success("Member Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: MemberController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Member dbMember = await _asyncMemberRepository.FindById(id);
            ViewBag.Message = dbMember.FullName;

            if (dbMember is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayMember>(dbMember);
            return View(data);
        }

        // POST: MemberController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Member dbMember = await _asyncMemberRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncMemberRepository.Remove(dbMember);

                _notyf.Error("Member Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
