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
    public class StaffController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;
        private readonly IAsyncStaffRepository  _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditStaff> _validator;
        public StaffController(IMapper mapper, ILogger<StaffController> logger,
                                   IAsyncStaffRepository asyncStaffRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditStaff> validator)
        {
            _mapper = mapper;
            _logger = logger;
           _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: StaffController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStaffData()
        {
            IQueryable<Staff> allStaff = _asyncStaffRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayStaff>>(allStaff).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: StaffController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Staff not found");

            Staff dbStaff = await _asyncStaffRepository.FindById(id);

            ViewBag.Message = dbStaff.FullName;

            _logger.LogInformation($"Details of Staff: {ViewBag.Message}");

            if (dbStaff is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayStaff>(dbStaff);

            return View(data);
        }

        // GET: StaffController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditStaff createAndEditStaff)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditStaff);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditStaff);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Staff dbStaff = new();

                    _mapper.Map(createAndEditStaff, dbStaff);
                    await _asyncStaffRepository.Add(dbStaff);

                    _notyf.Success("Staff Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }

        }

        // GET: StaffController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Staff {id} not found");

            Staff dbStaff = await _asyncStaffRepository.FindById(id);

            if (dbStaff is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditStaff>(dbStaff));

            return View(data);
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditStaff createAndEditStaff)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Staff dbStaffToUpdate = await _asyncStaffRepository.FindById(createAndEditStaff.Id);

            //            _mapper.Map(createAndEditStaff, dbStaffToUpdate, typeof(CreateAndEditStaff), typeof(Staff));

            //             _notyf.Success("Staff Update  Successfully! ");

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

            ValidationResult result = await _validator.ValidateAsync(createAndEditStaff);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditStaff);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Staff dbStaffToUpdate = await _asyncStaffRepository.FindById(createAndEditStaff.Id);

                    _mapper.Map(createAndEditStaff, dbStaffToUpdate, typeof(CreateAndEditStaff), typeof(Staff));

                    _notyf.Success("Staff Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: StaffController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Staff dbStaff = await _asyncStaffRepository.FindById(id);
            ViewBag.Message = dbStaff.FullName;

            if (dbStaff is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayStaff>(dbStaff);
            return View(data);
        }

        // POST: StaffController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Staff dbStaff = await _asyncStaffRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncStaffRepository.Remove(dbStaff);

                _notyf.Error("Staff Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
