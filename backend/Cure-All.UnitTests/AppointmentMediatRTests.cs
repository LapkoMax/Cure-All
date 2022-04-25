using AutoMapper;
using Cure_All.BusinessLogic.RequestFeatures;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Appointment;
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
    public class AppointmentMediatRTests
    {
        [Fact]
        public async Task GetAppointmentCommand_ShouldReturnCorrectAppointment()
        {
            var command = new GetAppointmentCommand()
            {
                appointmentId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAppointmentAsync(command.appointmentId, false)).Returns(GetTestAppointment(command.appointmentId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<AppointmentDto>(It.IsAny<Appointment>())).Returns(GetTestAppointmentDto(command.appointmentId));

            var commandHandler = new GetAppointmentCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDto);
            Assert.IsAssignableFrom<AppointmentDto>(appDto);
            Assert.Equal(command.appointmentId, appDto.Id);
        }

        [Fact]
        public async Task GetAppointmentCommand_ShouldReturnNull()
        {
            var command = new GetAppointmentCommand()
            {
                appointmentId = Guid.NewGuid()
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAppointmentAsync(command.appointmentId, false)).Returns(GetTestAppointment(command.appointmentId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<AppointmentDto>(It.IsAny<Appointment>())).Returns(GetTestAppointmentDto(command.appointmentId));

            var commandHandler = new GetAppointmentCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(appDto);
        }

        [Fact]
        public async Task GetAllAppointmentsForDoctorCommand_ShouldReturnAppointments()
        {
            var command = new GetAllAppointmentsForDoctorCommand()
            {
                userId = "178f5e28-e855-4190-b632-588c3c95547e"
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync("178f5e28-e855-4190-b632-588c3c95547e", false)).Returns(GetTestDoctor());

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForDoctorAsync(new Guid("178f5e28-e855-4190-b632-588c3c95547e"), false)).Returns(GetTestAppointmentsForDoctor(new Guid("178f5e28-e855-4190-b632-588c3c95547e")));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(new Guid("178f5e28-e855-4190-b632-588c3c95547e")));

            var commandHandler = new GetAllAppointmentsForDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Single(appDtos);
        }

        [Fact]
        public async Task GetAllAppointmentsForDoctorCommand_ShouldReturnEmptyArray()
        {
            var command = new GetAllAppointmentsForDoctorCommand()
            {
                userId = "cef789eb-d5dd-4d72-84f4-5a73aee21656"
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync("cef789eb-d5dd-4d72-84f4-5a73aee21656", false)).Returns(Task.FromResult(new Doctor { Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656") }));

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForDoctorAsync(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"), false)).Returns(GetTestAppointmentsForDoctor(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")));

            var commandHandler = new GetAllAppointmentsForDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Empty(appDtos);
        }

        [Fact]
        public async Task GetAppointmentsForDoctorCommand_ShouldReturnAppointments()
        {
            var command = new GetAppointmentsForDoctorCommand()
            {
                userId = "178f5e28-e855-4190-b632-588c3c95547e",
                parameters = new AppointmentParameters { }
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync("178f5e28-e855-4190-b632-588c3c95547e", false)).Returns(GetTestDoctor());

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForDoctorAsync(new Guid("178f5e28-e855-4190-b632-588c3c95547e"), false)).Returns(GetTestAppointmentsForDoctor(new Guid("178f5e28-e855-4190-b632-588c3c95547e")));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(new Guid("178f5e28-e855-4190-b632-588c3c95547e")));

            var commandHandler = new GetAppointmentsForDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Single(appDtos);
        }

        [Fact]
        public async Task GetAppointmentsForDoctorCommand_ShouldReturnEmptyArray()
        {
            var command = new GetAppointmentsForDoctorCommand()
            {
                userId = "cef789eb-d5dd-4d72-84f4-5a73aee21656",
                parameters = new AppointmentParameters { }
            };

            var doctorMock = new Mock<IDoctorRepository>();
            doctorMock.Setup(repo => repo.GetDoctorByUserIdAsync("cef789eb-d5dd-4d72-84f4-5a73aee21656", false)).Returns(Task.FromResult(new Doctor { Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656") }));

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForDoctorAsync(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"), false)).Returns(GetTestAppointmentsForDoctor(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Doctor).Returns(doctorMock.Object);
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")));

            var commandHandler = new GetAppointmentsForDoctorCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Empty(appDtos);
        }

        [Fact]
        public async Task GetAllAppointmentsForPatientCommand_ShouldReturnAppointments()
        {
            var command = new GetAllAppointmentsForPatientCommand()
            {
                patientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791")
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForPatientAsync(command.patientCardId, false)).Returns(GetTestAppointmentsForPatient(command.patientCardId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForPatient(command.patientCardId));

            var commandHandler = new GetAllAppointmentsForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Equal(2, appDtos.Count());
        }

        [Fact]
        public async Task GetAllAppointmentsForPatientCommand_ShouldReturnEmptyArray()
        {
            var command = new GetAllAppointmentsForPatientCommand()
            {
                patientCardId = Guid.NewGuid()
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForPatientAsync(command.patientCardId, false)).Returns(GetTestAppointmentsForDoctor(command.patientCardId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(command.patientCardId));

            var commandHandler = new GetAllAppointmentsForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Empty(appDtos);
        }

        [Fact]
        public async Task GetAppointmentsForPatientCommand_ShouldReturnAppointments()
        {
            var command = new GetAppointmentsForPatientCommand()
            {
                patientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791"),
                parameters = new AppointmentParameters { }
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForPatientAsync(command.patientCardId, false)).Returns(GetTestAppointmentsForPatient(command.patientCardId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForPatient(command.patientCardId));

            var commandHandler = new GetAppointmentsForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Equal(2, appDtos.Count());
        }

        [Fact]
        public async Task GetAppointmentsForPatientCommand_ShouldReturnEmptyArray()
        {
            var command = new GetAppointmentsForPatientCommand()
            {
                patientCardId = Guid.NewGuid(),
                parameters = new AppointmentParameters { }
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAllAppointmentsForPatientAsync(command.patientCardId, false)).Returns(GetTestAppointmentsForDoctor(command.patientCardId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDto>>(It.IsAny<IEnumerable<Appointment>>())).Returns(GetTestAppointmentsDtoForDoctor(command.patientCardId));

            var commandHandler = new GetAppointmentsForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var appDtos = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(appDtos);
            Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(appDtos);
            Assert.Empty(appDtos);
        }

        [Fact]
        public async Task CreateNewAppointmentCommand_ShouldCreateNewAppointment()
        {
            var command = new CreateNewAppointmentCommand()
            {
                appointment = new AppointmentForCreationDto { }
            };

            var appId = Guid.NewGuid();

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.CreateAppointment(new Appointment { Id = appId }));
            appMock.Setup(repo => repo.GetAppointmentAsync(appId, false)).Returns(Task.FromResult(new Appointment { Id = appId, Doctor = new Doctor { UserId = Guid.NewGuid().ToString() } }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Appointment>(It.IsAny<AppointmentForCreationDto>())).Returns(new Appointment { Id = appId });

            var commandHandler = new CreateNewAppointmentCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result.AppointmentId == appId);
        }

        [Fact]
        public async Task DeleteAppointmentCommand_ShouldDeleteCorrectly()
        {
            var command = new DeleteAppointmentCommand()
            {
                appointmentId = Guid.NewGuid()
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.DeleteAppointment(new Appointment { Id = (Guid)command.appointmentId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var commandHandler = new DeleteAppointmentCommandHandler(mockRepoManager.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task EditAppointmentCommand_ShouldEditCorrectly()
        {
            var appointmentId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656");

            var command = new EditAppointmentCommand
            {
                appointmentId = appointmentId,
                appointment = new AppointmentForEditingDto { }
            };

            var appMock = new Mock<IAppointmentRepository>();
            appMock.Setup(repo => repo.GetAppointmentAsync(appointmentId, true)).Returns(GetTestAppointment(appointmentId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Appointment).Returns(appMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Appointment>(It.IsAny<AppointmentForEditingDto>())).Returns(new Appointment { Id = appointmentId });

            var commandHandler = new EditAppointmentCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(result, appointmentId);
        }

        private async Task<IEnumerable<Appointment>> TestAppointments()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    DoctorId = new Guid("178f5e28-e855-4190-b632-588c3c95547e"),
                    PatientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791")
                },
                new Appointment
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    DoctorId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c"),
                    PatientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791")
                },
                new Appointment
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    DoctorId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0"),
                    PatientCardId = new Guid("9d141813-55fd-4937-bcab-b4b8ba667ba6")
                }
            };
        }

        private async Task<Appointment> GetTestAppointment(Guid appointmentId)
        {
            var apps = await TestAppointments();
            return apps.Where(app => app.Id == appointmentId).FirstOrDefault();
        }

        private async Task<IEnumerable<Appointment>> GetTestAppointmentsForDoctor(Guid doctorId)
        {
            var apps = await TestAppointments();
            return apps.Where(app => app.DoctorId == doctorId);
        }

        private async Task<IEnumerable<Appointment>> GetTestAppointmentsForPatient(Guid patientCardId)
        {
            var apps = await TestAppointments();
            return apps.Where(app => app.PatientCardId == patientCardId);
        }

        private async Task<Doctor> GetTestDoctor()
        {
            return new Doctor
            {
                Id = new Guid("178f5e28-e855-4190-b632-588c3c95547e")
            };
        }

        private IEnumerable<AppointmentDto> TestAppointmentsDto()
        {
            return new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    DoctorId = new Guid("178f5e28-e855-4190-b632-588c3c95547e"),
                    PatientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791")
                },
                new AppointmentDto
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    DoctorId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c"),
                    PatientCardId = new Guid("20ae1c64-6fb2-4d04-9859-234f866c9791")
                },
                new AppointmentDto
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    DoctorId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0"),
                    PatientCardId = new Guid("9d141813-55fd-4937-bcab-b4b8ba667ba6")
                }
            };
        }

        private AppointmentDto GetTestAppointmentDto(Guid appointmentId)
        {
            var apps = TestAppointmentsDto();

            return apps.Where(app => app.Id == appointmentId).SingleOrDefault();
        }

        private IEnumerable<AppointmentDto> GetTestAppointmentsDtoForDoctor(Guid doctorId)
        {
            var apps = TestAppointmentsDto();
            return apps.Where(app => app.DoctorId == doctorId);
        }

        private IEnumerable<AppointmentDto> GetTestAppointmentsDtoForPatient(Guid patientCardId)
        {
            var apps = TestAppointmentsDto();
            return apps.Where(app => app.PatientCardId == patientCardId);
        }
    }
}
