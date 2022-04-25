using AutoMapper;
using Cure_All.BusinessLogic.RequestFeatures;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Doctor;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cure_All.UnitTests
{
    public class DoctorMediatRTests
    {
        [Fact]
        public async Task GetDoctorsCommand_ShouldReturnAllDoctors()
        {
            var command = new GetDoctorsCommand()
            {
                doctorParameters = new DoctorParameters { }
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetAllDoctorsAsync(command.doctorParameters, false)).Returns(TestDoctors());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<DoctorDto>>(It.IsAny<IEnumerable<Doctor>>())).Returns(TestDoctorsDto());

            var commandHandler = new GetDoctorsCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var doctorsDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(doctorsDto);
            Assert.IsAssignableFrom<IEnumerable<DoctorDto>>(doctorsDto);
            Assert.Equal(TestDoctorsDto().Count(), doctorsDto.Count());
        }

        [Fact]
        public async Task GetDoctorCommand_ShouldReturnCorrectDoctor()
        {
            var command = new GetDoctorCommand()
            {
                doctorId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByDoctorIdAsync(command.doctorId, false)).Returns(GetTestDoctor(command.doctorId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<DoctorDto>(It.IsAny<Doctor>())).Returns(GetTestDoctorDto(command.doctorId));

            var commandHandler = new GetDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var doctorDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(doctorDto);
            Assert.IsAssignableFrom<DoctorDto>(doctorDto);
            Assert.Equal(command.doctorId, doctorDto.Id);
        }

        [Fact]
        public async Task GetDoctorCommand_ShouldReturnNull()
        {
            var command = new GetDoctorCommand()
            {
                doctorId = Guid.NewGuid()
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByDoctorIdAsync(command.doctorId, false)).Returns(GetTestDoctor(command.doctorId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<DoctorDto>(It.IsAny<Doctor>())).Returns(GetTestDoctorDto(command.doctorId));

            var commandHandler = new GetDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var doctorDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(doctorDto);
        }

        [Fact]
        public async Task GetDoctorFromUserCommand_ShouldReturnCorrectDoctor()
        {
            var command = new GetDoctorFromUserCommand()
            {
                userId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c").ToString()
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync(command.userId, false)).Returns(GetTestDoctorFromUser(command.userId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<DoctorDto>(It.IsAny<Doctor>())).Returns(GetTestDoctorDtoFromUser(command.userId));

            var commandHandler = new GetDoctorFromUserCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var doctorDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(doctorDto);
            Assert.IsAssignableFrom<DoctorDto>(doctorDto);
            Assert.Equal(command.userId, doctorDto.UserId);
        }

        [Fact]
        public async Task GetDoctorFromUserCommand_ShouldReturnNull()
        {
            var command = new GetDoctorFromUserCommand()
            {
                userId = Guid.NewGuid().ToString()
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync(command.userId, false)).Returns(GetTestDoctorFromUser(command.userId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<DoctorDto>(It.IsAny<Doctor>())).Returns(GetTestDoctorDtoFromUser(command.userId));

            var commandHandler = new GetDoctorFromUserCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var doctorDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(doctorDto);
        }

        [Fact]
        public async Task CreateDoctorCommand_ShouldCreateNewDoctor()
        {
            var command = new CreateDoctorCommand()
            {
                doctor = new DoctorForCreationDto { }
            };

            var doctorId = Guid.NewGuid();

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.CreateDoctor(new Doctor { Id = doctorId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Doctor>(It.IsAny<DoctorForCreationDto>())).Returns(new Doctor { Id = doctorId });

            var commandHandler = new CreateDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(id == doctorId);
        }

        [Fact]
        public async Task DeleteDoctorCommand_ShouldDeleteCorrectly()
        {
            var command = new DeleteDoctorCommand()
            {
                doctorId = Guid.NewGuid()
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.DeleteDoctor(new Doctor { Id = (Guid)command.doctorId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var commandHandler = new DeleteDoctorCommandHandler(mockRepoManager.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task EditDoctorCommand_ShouldEditCorrectly()
        {
            var doctorId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656");

            var command = new EditDoctorCommand
            {
                doctorId = doctorId,
                doctor = new DoctorForEditingDto { }
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByDoctorIdAsync(doctorId, true)).Returns(GetTestDoctor(doctorId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Doctor>(It.IsAny<DoctorForEditingDto>())).Returns(new Doctor { Id = doctorId });

            var commandHandler = new EditDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(result, doctorId);
        }

        private async Task<IEnumerable<Doctor>> TestDoctors()
        {
            return new List<Doctor>
            {
                new Doctor 
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    UserId = new Guid("178f5e28-e855-4190-b632-588c3c95547e").ToString()
                },
                new Doctor
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    UserId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c").ToString()
                },
                new Doctor
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    UserId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0").ToString()
                }
            };
        }

        private async Task<Doctor> GetTestDoctor(Guid doctorId)
        {
            var doctors = await TestDoctors();
            return doctors.Where(doc => doc.Id == doctorId).FirstOrDefault();
        }

        private async Task<Doctor> GetTestDoctorFromUser(string userId)
        {
            var doctors = await TestDoctors();
            return doctors.Where(doc => doc.UserId == userId).FirstOrDefault();
        }

        private IEnumerable<DoctorDto> TestDoctorsDto()
        {
            return new List<DoctorDto>
            {
                new DoctorDto
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    UserId = new Guid("178f5e28-e855-4190-b632-588c3c95547e").ToString()
                },
                new DoctorDto
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    UserId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c").ToString()
                },
                new DoctorDto
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    UserId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0").ToString()
                }
            };
        }

        private DoctorDto GetTestDoctorDto(Guid doctorId)
        {
            var doctors = TestDoctorsDto();

            return doctors.Where(doc => doc.Id == doctorId).SingleOrDefault();
        }

        private DoctorDto GetTestDoctorDtoFromUser(string userId)
        {
            var doctors = TestDoctorsDto();
            return doctors.Where(doc => doc.UserId == userId).FirstOrDefault();
        }
    }
}
