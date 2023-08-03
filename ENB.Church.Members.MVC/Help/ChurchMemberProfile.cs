using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Church.Members.Entities;
using ENB.Church.Members.MVC.Models;

namespace ENB.SchoolTimetables.MVC.Help
{
    public class ChurchMemberProfile : Profile
    {
        public ChurchMemberProfile()
        {


            #region Member 
            CreateMap<Member, DisplayMember>();

            CreateMap<CreateAndEditMember, Member>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.MemberContributions, t => t.Ignore())
              .ForMember(d => d.MemberActivities, t => t.Ignore())
              .ForMember(d => d.MemberAddress, t => t.Ignore());
            CreateMap<Member, CreateAndEditMember>();
            #endregion

            #region Staff
            CreateMap<Staff, DisplayStaff>();

            CreateMap<CreateAndEditStaff, Staff>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.AddressStaff, t => t.Ignore());
            CreateMap<Staff, CreateAndEditStaff>();

            #endregion

            #region Ministry
            CreateMap<Ministry, DisplayMinistry>();

            CreateMap<CreateAndEditMinistry, Ministry>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Ministry, CreateAndEditMinistry>();

            #endregion

            #region Identity
            CreateMap<UserRegistrationModel, ApplicationUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #endregion


            #region Member contribution
            CreateMap<MemberContribution, DisplayMemberContribution>()
             .ForMember(d => d.Member, t => t.Ignore())
             .ForMember(d => d.MemberId, t => t.MapFrom(y => y.MemberId)); 
            CreateMap<CreateAndEditMemberContribution, MemberContribution>()
              .ForMember(d => d.MemberId, t => t.MapFrom(y => y.MemberId))              
              .ForMember(d => d.Member, t => t.Ignore())             
              .ReverseMap();
            #endregion


            #region Activity
            CreateMap<Activity, DisplayActivity>();

            CreateMap<CreateAndEditActivity, Activity>()
              .ForMember(d => d.DateCreated, t => t.Ignore())               
               .ForMember(d => d.MinistriesActivities, t => t.Ignore())
              .ForMember(d => d.Color, t => t.MapFrom(y => y.ActivityStatus.ToString()))
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Activity, CreateAndEditActivity>();

            #endregion

            #region Ministry Activity
            CreateMap<MinistryActivity, DisplayMinistryActivity>()
             .ForMember(d => d.Ministry, t => t.Ignore())
             .ForMember(d => d.MinistryId, t => t.MapFrom(y => y.MinistryId));
            CreateMap<CreateAndEditMinistryActivity, MinistryActivity>()
              .ForMember(d => d.MinistryId, t => t.MapFrom(y => y.MinistryId))
              .ForMember(d => d.Ministry, t => t.Ignore())
              .ForMember(d => d.Activity, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ReverseMap();
            #endregion

            #region Member Activity 
            CreateMap<MemberActivity, DisplayMemberActivity>()
              .ForMember(d => d.Member, t => t.Ignore())             
              .ForMember(d => d.MinistryActivity, t => t.Ignore())
              .ForMember(d => d.Ministry, t => t.Ignore())             
              .ForMember(d => d.MinistryActivityId, t => t.MapFrom(y => y.MinistryActivityId))
              .ForMember(d => d.MinistryId, t => t.MapFrom(y => y.MinistryId));
            CreateMap<CreateAndEditMemberActivity, MemberActivity>()             
              .ForMember(d => d.MinistryActivityId, t => t.MapFrom(y => y.MinistryActivityId))
              .ForMember(d => d.MinistryId, t => t.MapFrom(y => y.MinistryId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.Member, t => t.Ignore())             
              .ForMember(d => d.MinistryActivity, t => t.Ignore())
              .ForMember(d => d.Ministry, t => t.Ignore());
            CreateMap<MemberActivity, CreateAndEditMemberActivity>();
            #endregion



            //#region PlannedTimetable
            //CreateMap<PlannedTimetable, DisplayPlannedTimeTable>()
            //  .ForMember(d => d.Subject, t => t.Ignore())
            //  .ForMember(d => d.Teacher, t => t.Ignore())
            //  .ForMember(d => d.ClassR, t => t.Ignore())
            //  .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
            //  .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
            //  .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId));
            //CreateMap<CreateAndEditPlannedTimeTable, PlannedTimetable>()
            //  .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
            //  .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
            //  .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ForMember(d => d.Subject, t => t.Ignore())
            //  .ForMember(d => d.Teacher, t => t.Ignore())
            //  .ForMember(d => d.ClassR, t => t.Ignore());
            //CreateMap<PlannedTimetable, CreateAndEditPlannedTimeTable>();
            //#endregion


            //#region GeneratedTimetable
            //CreateMap<GeneratedTimetable, DisplayGeneratedTimeTable>()
            //  .ForMember(d => d.ClassR, t => t.Ignore())
            //  .ForMember(d => d.Subject, t => t.Ignore())
            //  .ForMember(d => d.Teacher, t => t.Ignore())
            //  .ForMember(d => d.PlannedTimetable, t => t.Ignore())
            //  .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
            //  .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
            //  .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
            //  .ForMember(d => d.PlannedTimetableId, t => t.MapFrom(y => y.PlannedTimetableId));
            //CreateMap<CreateAndEditGeneratedTimeTable, GeneratedTimetable>()
            //  .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
            //  .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
            //  .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
            //  .ForMember(d => d.PlannedTimetableId, t => t.MapFrom(y => y.PlannedTimetableId))
            //  .ForMember(d => d.ClassR, t => t.Ignore())
            //  .ForMember(d => d.Subject, t => t.Ignore())
            //  .ForMember(d => d.Teacher, t => t.Ignore())
            //  .ForMember(d => d.PlannedTimetable, t => t.Ignore());
            //CreateMap<GeneratedTimetable, CreateAndEditGeneratedTimeTable>();
            //#endregion

            //#region ClaimProcessing
            //CreateMap<ClaimProcessing, DisplayClaimProcessing>()
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.Policy, t => t.Ignore())
            //  .ForMember(d => d.ClaimHeader, t => t.Ignore())
            //  .ForMember(d => d.PolicyId, t => t.MapFrom(y => y.PolicyId))
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.ClaimHeaderId, t => t.MapFrom(y => y.ClaimHeaderId));
            //CreateMap<CreateAndEditClaimProcessing, ClaimProcessing>()
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.PolicyId, t => t.MapFrom(y => y.PolicyId))
            //  .ForMember(d => d.ClaimHeaderId, t => t.MapFrom(y => y.ClaimHeaderId))
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ForMember(d => d.Policy, t => t.Ignore())
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.ClaimHeader, t => t.Ignore());
            //CreateMap<ClaimProcessing, CreateAndEditClaimProcessing>();
            //#endregion


            //#region ClaimProssecingStage 
            //CreateMap<ClaimProcessingStage, DisplayClaimProcessingStage>()
            //  .ForMember(d => d.RepliesProcessingStages, t => t.Ignore())
            // .ForMember(d => d.ParentClaimStage, t => t.Ignore());
            //CreateMap<CreateAndEditClaimProcessingStage, ClaimProcessingStage>()
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.ParentClaimStage, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore());
            //CreateMap<ClaimProcessingStage, CreateAndEditClaimProcessingStage>();
            //#endregion
            //#region PatientStaff
            //CreateMap<Staff_Patient_Association, DisplayStaff_Patient_Association>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditStaff_Patient_Association, Staff_Patient_Association>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            //#region Appointment
            //CreateMap<Appointment, DisplayAppointment>()
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditAppointment, Appointment>()
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.Color, t => t.MapFrom(y => y.EventStatus.ToString()))
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();
            //#endregion


            //#region PatientRecord
            //CreateMap<Patient_Record, DisplayPatientRecord>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditPatientRecord, Patient_Record>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion
            //#region BookingNotes
            //CreateMap<Booking_Note, DisplayBookingNote>()
            // .ForMember(d => d.Customer, t => t.Ignore())
            // .ForMember(d => d.Booking, t => t.Ignore())            
            // .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            // .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId));

            //CreateMap<CreateAndEditBookingNote, Booking_Note>()
            //  .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId))            
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.Booking, t => t.Ignore())              
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            #region Address

            CreateMap<Address, EditAddress>()
                  .ForMember(d => d.MemberId, t => t.Ignore())
                  .ForMember(d => d.StaffId, t => t.Ignore());
            CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Number_street!, s.City!, s.Zipcode!, s.State_province_county!, s.Country!));
            #endregion
        }
    }
}
