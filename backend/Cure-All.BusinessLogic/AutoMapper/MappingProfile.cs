using AutoMapper;
using Cure_All.BusinessLogic.Extensions;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(doc => doc.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(doc => doc.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(doc => doc.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(doc => doc.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber))
                .ForMember(doc => doc.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(doc => doc.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(doc => doc.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(doc => doc.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(doc => doc.City, opt => opt.MapFrom(x => x.User.City))
                .ForMember(doc => doc.Specialization, opt => opt.MapFrom(x => x.Specialization.Name))
                .ForMember(doc => doc.YearsOfExperience, opt => opt.MapFrom(x => (int)(DateTime.Now.Subtract(x.WorkStart).Days / 365)))
                .ForMember(doc => doc.WorkDayStart, opt => opt.MapFrom(x => $"{x.WorkDayStart.Hours}:{x.WorkDayStart.Minutes}"))
                .ForMember(doc => doc.WorkDayEnd, opt => opt.MapFrom(x => $"{x.WorkDayEnd.Hours}:{x.WorkDayEnd.Minutes}"))
                .ForMember(doc => doc.DinnerStart, opt => opt.MapFrom(x => $"{x.DinnerStart.Hours}:{x.DinnerStart.Minutes}"))
                .ForMember(doc => doc.DinnerEnd, opt => opt.MapFrom(x => $"{x.DinnerEnd.Hours}:{x.DinnerEnd.Minutes}"));
            CreateMap<DoctorForCreationDto, Doctor>()
                .ForMember(doc => doc.WorkDayStart, opt => opt.MapFrom(x => TimeSpan.Parse(x.WorkDayStart)))
                .ForMember(doc => doc.WorkDayEnd, opt => opt.MapFrom(x => TimeSpan.Parse(x.WorkDayEnd)))
                .ForMember(doc => doc.DinnerStart, opt => opt.MapFrom(x => TimeSpan.Parse(x.DinnerStart)))
                .ForMember(doc => doc.DinnerEnd, opt => opt.MapFrom(x => TimeSpan.Parse(x.DinnerEnd)));
            CreateMap<DoctorForEditingDto, Doctor>();
            CreateMap<DoctorsScedule, DoctorsSceduleDto>()
                .ForMember(docSced => docSced.dayOfWeek, opt => opt.MapFrom(x => x.dayOfWeek.ToString()));
            CreateMap<DoctorSceduleForCreationDto, DoctorsScedule>()
                .ForMember(docSced => docSced.dayOfWeek, opt => opt.MapFrom(x => x.dayOfWeek.GetDayOfWeek()));
            CreateMap<DoctorSceduleForEditingDto, DoctorsScedule>()
                .ForMember(docSced => docSced.dayOfWeek, opt => opt.MapFrom(x => x.dayOfWeek.GetDayOfWeek()));
            CreateMap<DoctorDayOffs, DoctorDayOffsDto>()
                .ForMember(docDayOff => docDayOff.Status, opt => opt.MapFrom(x => x.Status.ToString()));
            CreateMap<DoctorDayOffForCreationDto, DoctorDayOffs>()
                .ForMember(docDayOff => docDayOff.Status, opt => opt.MapFrom(x => x.Status.GetStatus()));
            CreateMap<DoctorDayOffForEditingDto, DoctorDayOffs>()
                .ForMember(docDayOff => docDayOff.Status, opt => opt.MapFrom(x => x.Status.GetStatus()));
            CreateMap<Patient, PatientDto>()
                .ForMember(pat => pat.PatientCardId, opt => opt.MapFrom(x => x.PatientCard.Id))
                .ForMember(pat => pat.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(pat => pat.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(pat => pat.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(pat => pat.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber))
                .ForMember(pat => pat.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(pat => pat.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(pat => pat.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(pat => pat.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(pat => pat.City, opt => opt.MapFrom(x => x.User.City));
            CreateMap<PatientForCreationDto, Patient>();
            CreateMap<PatientForEditingDto, Patient>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(app => app.StartTime, opt => opt.MapFrom(x => $"{x.StartTime.Hours}:{x.StartTime.Minutes}"))
                .ForMember(app => app.DoctorUserId, opt => opt.MapFrom(x => x.Doctor.UserId))
                .ForMember(app => app.DoctorFirstName, opt => opt.MapFrom(x => x.Doctor.User.FirstName))
                .ForMember(app => app.DoctorLastName, opt => opt.MapFrom(x => x.Doctor.User.LastName))
                .ForMember(app => app.PatientFirstName, opt => opt.MapFrom(x => x.PatientCard.Patient.User.FirstName))
                .ForMember(app => app.PatientLastName, opt => opt.MapFrom(x => x.PatientCard.Patient.User.LastName))
                .ForMember(app => app.IllnessName, opt => opt.MapFrom(x => x.Illness.Name));
            CreateMap<AppointmentForCreationDto, Appointment>()
                .ForMember(app => app.Completed, opt => opt.MapFrom(x => false))
                .ForMember(app => app.StartTime, opt => opt.MapFrom(x => TimeSpan.Parse(x.StartTime)));
            CreateMap<AppointmentForEditingDto, Appointment>();
            CreateMap<IllnessForCreationDto, Illness>();
            CreateMap<PatientCard, PatientCardDto>()
                .ForMember(card => card.PatientUserId, opt => opt.MapFrom(x => x.Patient.UserId))
                .ForMember(card => card.PatientFirstName, opt => opt.MapFrom(x => x.Patient.User.FirstName))
                .ForMember(card => card.PatientLastName, opt => opt.MapFrom(x => x.Patient.User.LastName));
        }
    }
}
