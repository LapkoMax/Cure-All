using AutoMapper;
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
                .ForMember(doc => doc.YearsOfExperience, opt => opt.MapFrom(x => (int)(DateTime.Now.Subtract(x.WorkStart).Days / 365)));
            CreateMap<DoctorForCreationDto, Doctor>();
            CreateMap<DoctorForEditingDto, Doctor>();
            CreateMap<Patient, PatientDto>()
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
                .ForMember(app => app.DoctorUserId, opt => opt.MapFrom(x => x.Doctor.UserId))
                .ForMember(app => app.DoctorFirstName, opt => opt.MapFrom(x => x.Doctor.User.FirstName))
                .ForMember(app => app.DoctorLastName, opt => opt.MapFrom(x => x.Doctor.User.LastName))
                .ForMember(app => app.PatientFirstName, opt => opt.MapFrom(x => x.PatientCard.Patient.User.FirstName))
                .ForMember(app => app.PatientLastName, opt => opt.MapFrom(x => x.PatientCard.Patient.User.LastName))
                .ForMember(app => app.IllnessName, opt => opt.MapFrom(x => x.Illness.Name));
            CreateMap<AppointmentForCreationDto, Appointment>()
                .ForMember(app => app.Completed, opt => opt.MapFrom(x => false));
            CreateMap<AppointmentForEditingDto, Appointment>();
            CreateMap<IllnessForCreationDto, Illness>();
            CreateMap<PatientCard, PatientCardDto>()
                .ForMember(card => card.PatientUserId, opt => opt.MapFrom(x => x.Patient.UserId))
                .ForMember(card => card.PatientFirstName, opt => opt.MapFrom(x => x.Patient.User.FirstName))
                .ForMember(card => card.PatientLastName, opt => opt.MapFrom(x => x.Patient.User.LastName));
        }
    }
}
