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
                .ForMember(doc => doc.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(doc => doc.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(doc => doc.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(doc => doc.City, opt => opt.MapFrom(x => x.User.City))
                .ForMember(doc => doc.Specialization, opt => opt.MapFrom(x => x.Specialization.Name))
                .ForMember(doc => doc.YearsOfExperience, opt => opt.MapFrom(x => (int)(DateTime.Now.Subtract(x.WorkStart).Days / 365)));
            CreateMap<DoctorForCreationDto, Doctor>();
            CreateMap<Patient, PatientDto>()
                .ForMember(pat => pat.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(pat => pat.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(pat => pat.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(pat => pat.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(pat => pat.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(pat => pat.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(pat => pat.City, opt => opt.MapFrom(x => x.User.City));
            CreateMap<PatientForCreationDto, Patient>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
