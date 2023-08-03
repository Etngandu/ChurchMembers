using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Church.Members.Entities.Repositories;
using ENB.Church.Members.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ENB.Church.Members.MVC.Models;
using ENB.Church.Members.Entities;

namespace ENB.Church.Members.MVC.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ActivityController> _logger;
        private readonly IAsyncActivityRepository _asyncActivityRepository;        
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public ActivityController(IMapper mapper, ILogger<ActivityController> logger,
                                   IAsyncActivityRepository asyncActivityRepository,                                   
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncActivityRepository = asyncActivityRepository;            
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }
        public IActionResult Index(DateTime? eventDate)
        {
            ViewBag.EventDate = eventDate ?? DateTime.Now;
            return View();
        }

        public async Task<IActionResult> List()
        {

            var activities = await Task.FromResult(_asyncActivityRepository.FindAll());



            return View();
        }


        public JsonResult GetListActivities()
        {

            var dbactivityEvents = _asyncActivityRepository.FindAll();

            var events = new List<DisplayActivity>();

            _mapper.Map(dbactivityEvents, events);

            return Json(new { data = events });


        }


        [HttpGet]
        public IActionResult CreateCal(string eventDate)
        {
           // ViewBag.EventDate = eventDate;           
            var data= new CreateAndEditActivity();
            data.Start=DateTime.Parse(eventDate);
            data.End = DateTime.Parse(eventDate);

            return View(data);
        }

        [HttpGet]
        public IActionResult CreateActivity()
        {            

            return View();
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditActivity createAndEditActivity)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                         Activity activity = new();

                        _mapper.Map(createAndEditActivity, activity);

                    await  _asyncActivityRepository.Add(activity);

                        _notyf.Success("Activity event Added  Successfully! ");

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return View();
        }

        public JsonResult GetEvents()
        {

            var dbactivityEvents = _asyncActivityRepository.FindAll();

            var events = new List<DisplayActivity>();

            _mapper.Map(dbactivityEvents, events);

            

            return Json(events);


        }

        public async Task<IActionResult> EditActivity( int id)
        {

            
            ViewBag.Id = id;

            var dbactvity = await _asyncActivityRepository.FindById(id);

            if (dbactvity is null)
            {
                return NotFound();
            }

            var activity= new CreateAndEditActivity();

            _mapper.Map(dbactvity, activity);

            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditActivity createAndEditActivity)
        {

           
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var dbactivity = await _asyncActivityRepository.FindById(createAndEditActivity.Id);
                        

                        _mapper.Map(createAndEditActivity, dbactivity);

                        _notyf.Success("Event updated Successfully");

                        return RedirectToAction(nameof(List));
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {

          
            ViewBag.Id = id;

            var dbactivity = await _asyncActivityRepository.FindById(id);

            if (dbactivity is null)
            {
                return NotFound();
            }
            var myevent = new DisplayActivity();

           _mapper.Map(dbactivity, myevent);

            
            return View(myevent);
        }

        public async Task<IActionResult> Delete(int id)
        {

            ViewBag.Id = id;

            var dbactivity = await _asyncActivityRepository.FindById(id);

            if (dbactivity is null)
            {
                return NotFound();
            }
            var myevent = new DisplayActivity();

            _mapper.Map(dbactivity, myevent);


            return View(myevent);
        }

        // POST: BookingEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayActivity DisplayActivity)
        {
            var activity = await _asyncActivityRepository.FindById(DisplayActivity.Id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                
                

                _asyncActivityRepository.Remove(activity);

                _notyf.Error("Activity related  removed  Successfully");
            }
            return RedirectToAction(nameof(List));
        }
    }
}
