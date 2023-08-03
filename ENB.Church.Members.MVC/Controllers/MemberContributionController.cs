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
    public class MemberContributionController : Controller
    {
        private readonly IAsyncMemberRepository _asyncMemberRepository;      
        private readonly IAsyncMinistryRepository  _asyncMinistryRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<MemberContributionController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public MemberContributionController(IAsyncMemberRepository asyncMemberRepository,                                       
                                      IAsyncMinistryRepository  asyncMinistryRepository,                                   
                                      IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<MemberContributionController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncMemberRepository = asyncMemberRepository;
            _asyncMinistryRepository = asyncMinistryRepository;           
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _imapper = imapper;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int memberId)
        {
            ViewBag.IdMember = memberId;
            var Member = await _asyncMemberRepository.FindById(memberId);

            ViewBag.Message = Member.FullName;

            return View();
        }


        public async Task<IActionResult> GetMemberContributions(int memberId)
        {

            var memberContributions = (from mbrctr in _asyncMemberRepository.FindAll().Where(mb => mb.Id == memberId).SelectMany(x => x.MemberContributions)
                            join mnt in _asyncMinistryRepository.FindAll() on mbrctr.MinistryId equals mnt.Id
                            select new DisplayMemberContribution
                            {
                                Id = mbrctr.Id,
                                Payment_Method = mbrctr.Payment_Method,  
                                Contribution_amount=mbrctr.Contribution_amount,
                                Contribution_Date=mbrctr.Contribution_Date,
                                Contribution_Comments=mbrctr.Contribution_Comments,
                                DateCreated = mbrctr.DateCreated,
                                DateModified = mbrctr.DateModified

                            }).ToList();

            

            var Mpdata = await Task.FromResult(memberContributions);
           

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int memberId)
        {
            ViewBag.IdMember = memberId;

            var data = new CreateAndEditMemberContribution()
            {

                ListMinistry = _asyncMinistryRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.MinistryName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            };



            var member = await _asyncMemberRepository.FindById(memberId);

            ViewBag.Message = member.FullName;
            data.Contribution_Date = DateTime.Today;


            return View(data);
        }

        



        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMemberContribution createAndEditMemberContribution, int memberId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var member = await _asyncMemberRepository.FindById(memberId);

                        MemberContribution memberContribution  = new();

                        _imapper.Map(createAndEditMemberContribution, memberContribution);


                        member.MemberContributions.Add(memberContribution);

                        _notyf.Success("MemberContribution  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { memberId });
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
            createAndEditMemberContribution.ListMinistry = _asyncMinistryRepository.FindAll().Select(d => new SelectListItem
            {
                Text = d.MinistryName,
                Value = d.Id.ToString(),
                Selected = true

            }).Distinct().ToList();
            ViewBag.IdMember = createAndEditMemberContribution.MemberId;

            return View("Create", createAndEditMemberContribution);
        }




        public async Task<IActionResult> Edit(int memberId, int id)
        {

            var member = await _asyncMemberRepository.FindById(memberId, x => x.MemberContributions);
            ViewBag.Message = member.FullName;
            ViewBag.IdMember = memberId;
            ViewBag.Id = id;



            if (member is null)
            {
                return NotFound();
            }

            CreateAndEditMemberContribution data = new()
            {

                ListMinistry = _asyncMinistryRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.MinistryName,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList()
            };

            _imapper.Map(member.MemberContributions.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditMemberContribution createAndEditMemberContribution, int memberId)
        {

            ViewBag.IdMember = memberId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var member = await _asyncMemberRepository.FindById(memberId, x => x.MemberContributions);
                        var memberContribution = member.MemberContributions.Single(x => x.Id == createAndEditMemberContribution.Id);

                        _imapper.Map(createAndEditMemberContribution, memberContribution);

                        _notyf.Success(" memberContribution updated Successfully");

                        return RedirectToAction(nameof(List), new { memberId });
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

            createAndEditMemberContribution.ListMinistry = _asyncMinistryRepository.FindAll().Select(d => new SelectListItem
            {
                Text = d.MinistryName,
                Value = d.Id.ToString(),
                Selected = true

            }).Distinct().ToList();
            ViewBag.IdMember = createAndEditMemberContribution.MemberId;
            return View("Edit", createAndEditMemberContribution);
        }

        public async Task<IActionResult> Details(int memberId, int id)
        {

            var member = await _asyncMemberRepository.FindById(memberId);
            ViewBag.Message = member.FullName;
            ViewBag.IdMember = memberId;
            ViewBag.Id = id;


            var lstMemberContribution = from mbctr in _asyncMemberRepository.FindAll().Where(x => x.Id == memberId).SelectMany(cp => cp.MemberContributions)
                              join mnb in _asyncMemberRepository.FindAll() on mbctr.MemberId equals mnb.Id
                              join mnt in _asyncMinistryRepository.FindAll() on mbctr.MinistryId equals mnt.Id
                              select new DisplayMemberContribution
                              {
                                  Id = mbctr.Id,
                                  Contribution_amount=mbctr.Contribution_amount,
                                  Contribution_Date=mbctr.Contribution_Date,
                                  Contribution_Comments=mbctr.Contribution_Comments,
                                  Payment_Method=mbctr.Payment_Method,
                                  MemberName=mnb.FullName,
                                  MinistryName=mnt.MinistryName,
                                  DateCreated = mbctr.DateCreated,
                                  DateModified = mbctr.DateModified,                                 

                              };


            if (lstMemberContribution is null)
            {
                return NotFound();
            }

            var sglMemberContribution = lstMemberContribution.Single(x => x.Id == id);

          

            return View(sglMemberContribution);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int memberId, int id)
        {
            var member = await _asyncMemberRepository.FindById(memberId);
            ViewBag.Message = member.FullName;
            ViewBag.IdMember = memberId;
            ViewBag.Id = id;


            var lstMemberContribution = from mbctr in _asyncMemberRepository.FindAll().Where(x => x.Id == memberId).SelectMany(cp => cp.MemberContributions)
                                        join mnb in _asyncMemberRepository.FindAll() on mbctr.MemberId equals mnb.Id
                                        join mnt in _asyncMinistryRepository.FindAll() on mbctr.MinistryId equals mnt.Id
                                        select new DisplayMemberContribution
                                        {
                                            Id = mbctr.Id,
                                            Contribution_amount = mbctr.Contribution_amount,
                                            Contribution_Date = mbctr.Contribution_Date,
                                            Contribution_Comments = mbctr.Contribution_Comments,
                                            Payment_Method = mbctr.Payment_Method,
                                            MemberName = mnb.FullName,
                                            MinistryName = mnt.MinistryName,
                                            DateCreated = mbctr.DateCreated,
                                            DateModified = mbctr.DateModified,

                                        };


            if (lstMemberContribution is null)
            {
                return NotFound();
            }

            var sglMemberContribution = lstMemberContribution.Single(x => x.Id == id);



            return View(sglMemberContribution);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayMemberContribution displayMemberContribution, int memberId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var member = await _asyncMemberRepository.FindById(memberId, x => x.MemberContributions);
                var memberContribution = member.MemberContributions.Single(x => x.Id == displayMemberContribution.Id);

                member.MemberContributions.Remove(memberContribution);

                _notyf.Error("Contribution related to Member removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { memberId });
        }
    }
}
