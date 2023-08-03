using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Church.Members.Entities;
using ENB.Church.Members.Infrastructure;
using ENB.Church.Members.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ENB.Church.Members.Entities.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ENB.Church.Members.Entities.Repositories;

namespace ENB.Car.Sales.MVC.Controllers
{
    public class MinistryActivityController : Controller
    {
        private readonly IAsyncMinistryRepository _asyncMinistryRepository;      
        private readonly IAsyncActivityRepository  _asyncActivityRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<MinistryActivityController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public MinistryActivityController(IAsyncMinistryRepository asyncMinistryRepository,                                       
                                      IAsyncActivityRepository  asyncActivityRepository,                                   
                                      IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<MinistryActivityController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncMinistryRepository = asyncMinistryRepository;
            _asyncActivityRepository = asyncActivityRepository;           
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _imapper = imapper;
            _notyf = notyf;
        }
        public async Task<IActionResult> Index(int ministryId)
        {
            ViewBag.Idmin = ministryId;

            var data = new CreateAndEditMinistryActivity()
            {

                ListActivities = _asyncActivityRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.Activity_Description,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()

            };

            var ministry = await _asyncMinistryRepository.FindById(ministryId);
            

            ViewBag.Message = ministry.MinistryName;

            return View(data);
        }
        public async Task<IActionResult> List(int ministryId)
        {
            ViewBag.Idmin = ministryId;
            var ministry = await _asyncMinistryRepository.FindById(ministryId);

            ViewBag.Message = ministry.MinistryName;

            return View();
        }


        public async Task<IActionResult> GetMinistryActivities(int ministryId)
        {

            var listMnActivities = (from cstpref in _asyncMinistryRepository.FindAll().Where(cs => cs.Id == ministryId).SelectMany(x => x.MinistryActivities)
                                    join crf in _asyncActivityRepository.FindAll() on cstpref.ActivityId equals crf.Id
                                    join cust in _asyncMinistryRepository.FindAll() on cstpref.MinistryId equals cust.Id
                                    select new DisplayMinistryActivity
                            {
                                Id = cstpref.Id,
                                ActivityId=cstpref.ActivityId,
                                MinistryActivity_Start_Date = crf.Start,  
                                MinistryActivity_End_Date=crf.End,
                                ActivityName=crf.Activity_Description,
                                MinistryName=cust.MinistryName,
                                DateCreated = cstpref.DateCreated,
                                DateModified = cstpref.DateModified

                            }).ToList();           

           

           var mdata= await Task.FromResult(listMnActivities);

            return Json(new { data = mdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int ministryId)
        {
            ViewBag.Idmin = ministryId;

            var data = new CreateAndEditMinistryActivity()
            {

                ListActivities = _asyncActivityRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.Activity_Description,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()

            };

            var ministry = await _asyncMinistryRepository.FindById(ministryId);

            ViewBag.Message = ministry.MinistryName;

            return View(data);
        }




        // POST: CarModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditMinistryActivity createAndEditMinistryActivity, int ministryId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (createAndEditMinistryActivity.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {
                            var ministry = await _asyncMinistryRepository.FindById(ministryId);


                            MinistryActivity ministryActivity = new();

                            var activity = await _asyncActivityRepository.FindById(createAndEditMinistryActivity.ActivityId);

                            createAndEditMinistryActivity.MinistryActivity_Start_Date = activity.Start;
                            createAndEditMinistryActivity.MinistryActivity_End_Date = activity.End;

                            _imapper.Map(createAndEditMinistryActivity, ministryActivity);


                            ministry.MinistryActivities.Add(ministryActivity);

                            _notyf.Success("Ministry Activity  Added  Successfully! ");

                            return RedirectToAction(nameof(Index), new { ministryId });
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
                createAndEditMinistryActivity.ListActivities = _asyncActivityRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.Activity_Description,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList();
                ViewBag.Idmin = createAndEditMinistryActivity.MinistryId;

                return View("Index", createAndEditMinistryActivity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MinistryActivities);
                            var ministryActivity = ministry.MinistryActivities.Single(x => x.Id == createAndEditMinistryActivity.Id);
                            var activity = await _asyncActivityRepository.FindById(createAndEditMinistryActivity.ActivityId);

                            createAndEditMinistryActivity.MinistryActivity_Start_Date = activity.Start;
                            createAndEditMinistryActivity.MinistryActivity_End_Date = activity.End;

                            _imapper.Map(createAndEditMinistryActivity, ministryActivity);

                            _notyf.Success(" MinistryActivity updated Successfully");

                            return RedirectToAction(nameof(Index), new { ministryId });
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

                createAndEditMinistryActivity.ListActivities = _asyncActivityRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.Activity_Description,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList();
                ViewBag.Idcust = createAndEditMinistryActivity.MinistryId;
                return View("Index", createAndEditMinistryActivity);
            }
          //  return View("Index");
        }


        //    // POST: LawyerController/Create
        //    [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateAndEditMinistryActivity createAndEditMinistryActivity , int ministryId)
        //{
        //    var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await using (await _asyncUnitOfWorkFactory.Create())
        //            {
        //                var ministry = await _asyncMinistryRepository.FindById(ministryId);
                       

        //                MinistryActivity ministryActivity = new();

        //                var activity = await _asyncActivityRepository.FindById(createAndEditMinistryActivity.ActivityId);

        //                ministryActivity.MinistryActivity_Start_Date = activity.Start;
        //                ministryActivity.MinistryActivity_End_Date = activity.End;

        //                _imapper.Map(createAndEditMinistryActivity, ministryActivity);


        //                ministry.MinistryActivities.Add(ministryActivity);

        //                _notyf.Success("Ministry Activity  Added  Successfully! ");

        //                return RedirectToAction(nameof(List), new { ministryId });
        //            }
        //        }
        //        catch (ModelValidationException mvex)
        //        {
        //            foreach (var error in mvex.ValidationErrors)
        //            {
        //                ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
        //            }
        //        }
        //    }
        //    createAndEditMinistryActivity.ListActivities = _asyncActivityRepository.FindAll().Select(d => new SelectListItem
        //    {
        //        Text = d.Activity_Description,
        //        Value = d.Id.ToString(),
        //        Selected = true

        //    }).Distinct().ToList();
        //    ViewBag.Idmin = createAndEditMinistryActivity.MinistryId;

        //    return View("Create", createAndEditMinistryActivity);
        //}




        //public async Task<IActionResult> Edit(int ministryId, int id)
        //{

        //    var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MinistryActivities);
        //    ViewBag.Message = ministry.MinistryName;
        //    ViewBag.Idmin = ministryId;
        //    ViewBag.Id = id;



        //    if (ministry is null)
        //    {
        //        return NotFound();
        //    }

        //    CreateAndEditMinistryActivity data = new()
        //    {

        //        ListActivities = _asyncActivityRepository.FindAll().Select(d => new SelectListItem
        //        {
        //            Text = d.Activity_Description,
        //            Value = d.Id.ToString(),
        //            Selected = true

        //        }).Distinct().ToList()
        //    };

        //    _imapper.Map(ministry.MinistryActivities.Single(x => x.Id == id), data);

        //    return View(data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreateAndEditMinistryActivity createAndEditMinistryActivity, int ministryId)
        //{

        //    ViewBag.Idmin = ministryId;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await using (await _asyncUnitOfWorkFactory.Create())
        //            {

        //                var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MinistryActivities);
        //                var ministryActivity = ministry.MinistryActivities.Single(x => x.Id == createAndEditMinistryActivity.Id);
        //                var activity = await _asyncActivityRepository.FindById(createAndEditMinistryActivity.ActivityId);

        //                ministryActivity.MinistryActivity_Start_Date= activity.Start;
        //                ministryActivity.MinistryActivity_End_Date= activity.End;

        //                _imapper.Map(createAndEditMinistryActivity, ministryActivity);

        //                _notyf.Success(" MinistryActivity updated Successfully");

        //                return RedirectToAction(nameof(List), new { ministryId });
        //            }
        //        }
        //        catch (ModelValidationException mvex)
        //        {
        //            foreach (var error in mvex.ValidationErrors)
        //            {
        //                ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
        //            }
        //        }
        //    }

        //    createAndEditMinistryActivity.ListActivities = _asyncActivityRepository.FindAll().Select(d => new SelectListItem
        //    {
        //        Text = d.Activity_Description,
        //        Value = d.Id.ToString(),
        //        Selected = true

        //    }).Distinct().ToList();
        //    ViewBag.Idcust = createAndEditMinistryActivity.MinistryId;
        //    return View("Edit", createAndEditMinistryActivity);
        //}

        public async Task<IActionResult> Details(int ministryId, int id)
        {

            var ministry = await _asyncMinistryRepository.FindById(ministryId);
            ViewBag.Message = ministry.MinistryName;
            ViewBag.Idmin = ministryId;
            ViewBag.Id = id;


            var lstMinistryActivities = from cpfs in _asyncMinistryRepository.FindAll().Where(x => x.Id == ministryId).SelectMany(cp => cp.MinistryActivities)
                              join cust in _asyncMinistryRepository.FindAll() on cpfs.MinistryId equals cust.Id
                              join crft in _asyncActivityRepository.FindAll() on cpfs.ActivityId equals crft.Id
                              select new DisplayMinistryActivity
                              {
                                  Id = cpfs.Id,
                                  MinistryId = cust.Id,                                  
                                  MinistryActivity_Start_Date=crft.Start,
                                  MinistryActivity_End_Date=crft.End,
                                  DateCreated = cpfs.DateCreated,
                                  DateModified = cpfs.DateModified,
                                  

                              };


            if (lstMinistryActivities is null)
            {
                return NotFound();
            }

            var sglMinistryActivity = lstMinistryActivities.Single(x => x.Id == id);           

            return View(sglMinistryActivity);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int ministryId, int id)
        {
           
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MinistryActivities);
                var ministryActivity = ministry.MinistryActivities.Single(x => x.Id == id);

                ministry.MinistryActivities.Remove(ministryActivity);

                _notyf.Error("MinistryActivity related to Ministry removed  Successfully");
            }
            return RedirectToAction(nameof(Index), new { ministryId });
        }

        //// POST: ApartmentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(DisplayMinistryActivity displayMinistryActivity, int ministryId)
        //{
        //    // ViewBag.Case_Id = caseid;
        //    await using (await _asyncUnitOfWorkFactory.Create())
        //    {
        //        var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MinistryActivities);
        //        var ministryActivity = ministry.MinistryActivities.Single(x => x.Id == displayMinistryActivity.Id);

        //        ministry.MinistryActivities.Remove(ministryActivity);

        //        _notyf.Error("MinistryActivity related to Ministry removed  Successfully");
        //    }
        //    return RedirectToAction(nameof(List), new { ministryId });
        //}
    }
}
