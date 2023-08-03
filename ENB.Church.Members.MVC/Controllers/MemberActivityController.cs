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
    public class MemberActivityController : Controller
    {
        private readonly IAsyncMinistryRepository _asyncMinistryRepository;   
        
        private readonly IAsyncActivityRepository  _asyncActivityRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IAsyncMemberRepository _asyncMemberRepository;
        private readonly ILogger<MemberActivityController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public MemberActivityController(IAsyncMinistryRepository asyncMinistryRepository,                                       
                                       IAsyncActivityRepository  asyncActivityRepository,                                   
                                       IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                       IAsyncMemberRepository asyncMemberRepository,
                                       ILogger<MemberActivityController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncMinistryRepository = asyncMinistryRepository;
            _asyncActivityRepository = asyncActivityRepository;           
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _asyncMemberRepository = asyncMemberRepository;
            _logger = logger;
            _imapper = imapper;
            _notyf = notyf;
        }
        public async Task<IActionResult> Index(int ministryId, int ministryActivityId)
        {
            ViewBag.Idmin = ministryId;
            ViewBag.MinistryAct = ministryActivityId;

            var data = new CreateAndEditMemberActivity()
            {

                ListMembers = _asyncMemberRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()

            };

            var ministry = await _asyncMinistryRepository.FindById(ministryId, x=>x.MinistryActivities);
            var ministryactivity = ministry.MinistryActivities.Single(y => y.Id == ministryActivityId);

            data.MemberActivity_Start_Date = ministryactivity.MinistryActivity_Start_Date;
            data.MemberActivity_End_Date=   ministryactivity.MinistryActivity_End_Date;

            var activity = await _asyncActivityRepository.FindById(ministryactivity.ActivityId);

            ViewBag.Message = activity.Activity_Description;
           

            return View(data);
        }
        


        public async Task<IActionResult> GetMembersActivities(int ministryId, int ministryActivityId)
        {

            var listMnActivities = (from mnAct in _asyncMinistryRepository.FindAll().Where(cs => cs.Id == ministryId).SelectMany(x => x.MemberActivities)
                                     .Where(y=>y.MinistryActivityId== ministryActivityId)                                   
                                    join mnb in _asyncMemberRepository.FindAll() on mnAct.MemberId equals mnb.Id
                                    select new DisplayMemberActivity
                            {
                                Id = mnAct.Id,  
                                MemberId=mnb.Id,
                                MemberActivity_Start_Date = mnAct.MemberActivity_Start_Date,  
                                MemberActivity_End_Date= mnAct.MemberActivity_End_Date,                              
                                MemberName=mnb.FullName,
                                DateCreated = mnAct.DateCreated,
                                DateModified = mnAct.DateModified

                            }).ToList();           

           

           var mdata= await Task.FromResult(listMnActivities);

            return Json(new { data = mdata });


        }

        


        // POST: CarModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditMemberActivity createAndEditMemberActivity, 
                                               int ministryId, 
                                               int ministryActivityId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (createAndEditMemberActivity.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {
                            var ministry = await _asyncMinistryRepository.FindById(ministryId, x=>x.MinistryActivities);
                            var ministryActivity = ministry.MinistryActivities.Single(y => y.Id == ministryActivityId);
                                                

                            MemberActivity memberActivity = new();
                            

                            _imapper.Map(createAndEditMemberActivity, memberActivity);


                            ministry.MemberActivities.Add(memberActivity);

                            _notyf.Success("Member Added to Activity    Successfully! ");

                            return RedirectToAction(nameof(Index), new { ministryId,ministryActivityId });
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
                createAndEditMemberActivity.ListMembers = _asyncMemberRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.FullName,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList();
                ViewBag.Idmin = createAndEditMemberActivity.MemberId;

                return View("Index", createAndEditMemberActivity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MemberActivities);
                            var memberActivity = ministry.MemberActivities.Where(y => y.MinistryActivityId == ministryActivityId)
                                                 .Single(z=>z.Id==createAndEditMemberActivity.Id);

                           

                            _imapper.Map(createAndEditMemberActivity, memberActivity);
                           

                            _notyf.Success("Member Activity Updated  Successfully! ");

                            return RedirectToAction(nameof(Index), new { ministryId, ministryActivityId });
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

                createAndEditMemberActivity.ListMembers = _asyncMemberRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.FullName,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList();
                ViewBag.Idcust = createAndEditMemberActivity.MemberId;
                return View("Index", createAndEditMemberActivity);
            }
          
        }


       

       


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int ministryId, int ministryActivityId, int id)
        {
           
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var ministry = await _asyncMinistryRepository.FindById(ministryId, x => x.MemberActivities);
                var memberActivity = ministry.MemberActivities.Where(y => y.MinistryActivityId == ministryActivityId)
                                     .Single(z=>z.Id==id);

                   ministry.MemberActivities.Remove(memberActivity);

                _notyf.Error("Membe rActivity  removed  Successfully");
            }
            return RedirectToAction(nameof(Index), new { ministryId,ministryActivityId });
        }

       
    }
}
