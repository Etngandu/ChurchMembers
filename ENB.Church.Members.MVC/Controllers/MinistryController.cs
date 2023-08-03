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
    public class MinistryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MinistryController> _logger;
        private readonly IAsyncMinistryRepository  _asyncMinistryRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditMinistry> _validator;
        public MinistryController(IMapper mapper, ILogger<MinistryController> logger,
                                   IAsyncMinistryRepository asyncMinistryRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditMinistry> validator)
        {
            _mapper = mapper;
            _logger = logger;
           _asyncMinistryRepository = asyncMinistryRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: MinistryController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMinistryData()
        {
            IQueryable<Ministry> allMinistry = _asyncMinistryRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayMinistry>>(allMinistry).ToList();

            await Task.FromResult(Mpdata);

            return Json(new { data = Mpdata });
        }

        // GET: MinistryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Ministry not found");

            Ministry dbMinistry = await _asyncMinistryRepository.FindById(id);

            ViewBag.Message = dbMinistry.MinistryName;

            _logger.LogInformation($"Details of Ministry: {ViewBag.Message}");

            if (dbMinistry is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayMinistry>(dbMinistry);

            return View(data);
        }

        // GET: MinistryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MinistryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMinistry createAndEditMinistry)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditMinistry);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditMinistry);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Ministry dbMinistry = new();

                    _mapper.Map(createAndEditMinistry, dbMinistry);
                    await _asyncMinistryRepository.Add(dbMinistry);

                    _notyf.Success("Ministry Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }

        }

       
        // POST: CarModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditMinistry createAndEditMinistry)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditMinistry);

            if (createAndEditMinistry.Id == 0)
            {
                if (!result.IsValid)
                {
                    // Copy the validation results into ModelState.
                    // ASP.NET uses the ModelState collection to populate 
                    // error messages in the View.
                    result.AddToModelState(this.ModelState);

                    // re-render the view when validation failed.
                    return View("Index", createAndEditMinistry);
                }
                else
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Ministry dbMinistry = new();

                        _mapper.Map(createAndEditMinistry, dbMinistry);
                        await _asyncMinistryRepository.Add(dbMinistry);

                        _notyf.Success("Ministry Created  Successfully! ");

                        return RedirectToAction("Index");
                    }
                }

            }
            else
            {
                if (!result.IsValid)
                {
                    // Copy the validation results into ModelState.
                    // ASP.NET uses the ModelState collection to populate 
                    // error messages in the View.
                    result.AddToModelState(this.ModelState);

                    // re-render the view when validation failed.
                    return View("Index", createAndEditMinistry);
                }
                else
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Ministry dbMinistryToUpdate = await _asyncMinistryRepository.FindById(createAndEditMinistry.Id);

                        _mapper.Map(createAndEditMinistry, dbMinistryToUpdate, typeof(CreateAndEditMinistry), typeof(Ministry));

                        _notyf.Success("Ministry Update  Successfully! ");

                        return RedirectToAction(nameof(Index));
                    }
                }

            }
           
        }


       
        // POST: MinistryController/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Ministry dbMinistry = await _asyncMinistryRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncMinistryRepository.Remove(dbMinistry);

                _notyf.Error("Ministry Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
