using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Church.Members.Entities;
using ENB.Church.Members.Entities.Repositories;
using ENB.Church.Members.Infrastructure;
using ENB.Church.Members.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ENB.Church.Members.MVC.Controllers
{
   // [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAsyncMemberRepository _asyncMemberRepository;
        private readonly IAsyncStaffRepository _asyncStaffRepository;        
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        /// <summary>
        /// Initializes a new instance of the AddressesController class.
        /// </summary>
        public AddressesController(IAsyncMemberRepository asyncMemberRepository,
                                   IAsyncStaffRepository asyncStaffRepository,                                   
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   IMapper mapper,
                                   INotyfService notyf)
        {
            _asyncMemberRepository = asyncMemberRepository;           
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Edit(int memberId, int staffId)
        {
            ViewBag.MemberId = memberId;
            ViewBag.StaffId = staffId;
           
            
            var address = new Address();
            var message = "";

            if (memberId != 0)
            {
                var member = await _asyncMemberRepository.FindById(memberId);
                if (member is null)
                {
                    return NotFound();
                }
                address = member.MemberAddress;
                message = member.FullName;
            }

            if (staffId != 0)
            {
                var staff = await _asyncStaffRepository.FindById(staffId);
                if (staff is null)
                {
                    return NotFound();
                }
                address = staff.AddressStaff;
                message = staff.FullName;
            }

            

            var data = new EditAddress();

            ViewBag.Message = message;

            _mapper.Map(address, data);
            return View(data);
        }

        public  IActionResult Redirect(int memberId, int staffId)
        {
            ViewBag.CustId = memberId;
            ViewBag.StaffId = staffId;
            var redirect= RedirectToAction("");

                  

            if (memberId != 0)
            {
              redirect=  RedirectToAction("Index","Member");
            }

            if (staffId != 0)
            {
              redirect=  RedirectToAction("Index","Staff");
            }


            return redirect;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddress editAddressModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        if (editAddressModel.MemberId != 0)
                        {
                            var Member = await _asyncMemberRepository.FindById(editAddressModel.MemberId);
                            _mapper.Map(editAddressModel, Member.MemberAddress);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Member");
                        }

                        if (editAddressModel.StaffId != 0)
                        {
                            var staff = await _asyncStaffRepository.FindById(editAddressModel.StaffId);
                            _mapper.Map(editAddressModel, staff.AddressStaff);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Staff");
                        }                       


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
    }
}
