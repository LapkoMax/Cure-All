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
                .ForMember(doc => doc.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(doc => doc.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(doc => doc.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(doc => doc.City, opt => opt.MapFrom(x => x.User.City))
                .ForMember(doc => doc.YearsOfExperience, opt => opt.MapFrom(x => (int)(DateTime.Now.Subtract(x.WorkStart).Days / 365)));
            CreateMap<Patient, PatientDto>()
                .ForMember(doc => doc.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(doc => doc.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(doc => doc.DateOfBurth, opt => opt.MapFrom(x => x.User.DateOfBurth))
                .ForMember(doc => doc.ZipCode, opt => opt.MapFrom(x => x.User.ZipCode))
                .ForMember(doc => doc.Country, opt => opt.MapFrom(x => x.User.Country))
                .ForMember(doc => doc.City, opt => opt.MapFrom(x => x.User.City));
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
