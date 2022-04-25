using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Patient;
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
    public class PatientMediatRTests
    {
        [Fact]
        public async Task GetPatientCommand_ByPatientId_ShouldReturnCorrectPatient()
        {
            var command = new GetPatientCommand()
            {
                patientId = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3")
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.GetPatientByPatientIdAsync((Guid)command.patientId, false)).Returns(TestPatient());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientDto>(It.IsAny<Patient>())).Returns(TestPatientDto());

            var commandHandler = new GetPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(patientDto);
            Assert.IsAssignableFrom<PatientDto>(patientDto);
            Assert.Equal(command.patientId, patientDto.Id);
        }

        [Fact]
        public async Task GetPatientCommand_ByPatientId_ShouldReturnNull()
        {
            var command = new GetPatientCommand()
            {
                patientId = Guid.NewGuid()
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.GetPatientByPatientIdAsync((Guid)command.patientId, false)).Returns(Task.FromResult<Patient>(null));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientDto>(It.IsAny<Patient>())).Returns((PatientDto)null);

            var commandHandler = new GetPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(patientDto);
        }

        [Fact]
        public async Task GetPatientCommand_ByPUserId_ShouldReturnCorrectPatient()
        {
            var command = new GetPatientCommand()
            {
                userId = "7fc2cc89-0b4c-4b15-a534-e4574b64a16f"
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.GetPatientByUserIdAsync(command.userId, false)).Returns(TestPatient());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientDto>(It.IsAny<Patient>())).Returns(TestPatientDto());

            var commandHandler = new GetPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(patientDto);
            Assert.IsAssignableFrom<PatientDto>(patientDto);
            Assert.Equal(command.userId, patientDto.UserId);
        }

        [Fact]
        public async Task GetPatientCommand_ByUserId_ShouldReturnNull()
        {
            var command = new GetPatientCommand()
            {
                userId = Guid.NewGuid().ToString()
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.GetPatientByUserIdAsync(command.userId, false)).Returns(Task.FromResult<Patient>(null));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientDto>(It.IsAny<Patient>())).Returns((PatientDto)null);

            var commandHandler = new GetPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(patientDto);
        }

        [Fact]
        public async Task CreatePatientCommand_ShouldCreateNewPatient()
        {
            var command = new CreatePatientCommand()
            {
                patient = new PatientForCreationDto { }
            };

            var patientId = Guid.NewGuid();

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.CreatePatient(new Patient { Id = patientId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Patient>(It.IsAny<PatientForCreationDto>())).Returns(new Patient { Id = patientId });

            var commandHandler = new CreatePatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(id == patientId);
        }

        [Fact]
        public async Task DeletePatientCommand_ShouldDeleteCorrectly()
        {
            var command = new DeletePatientCommand()
            {
                patientId = Guid.NewGuid()
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.DeletePatient(new Patient { Id = (Guid)command.patientId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var commandHandler = new DeletePatientCommandHandler(mockRepoManager.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task EditDoctorCommand_ShouldEditCorrectly()
        {
            var patientId = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3");

            var command = new EditPatientCommand
            {
                patientId = patientId,
                patient = new PatientForEditingDto { }
            };

            var patientMock = new Mock<IPatientRepository>();
            patientMock.Setup(repo => repo.GetPatientByPatientIdAsync(patientId, true)).Returns(TestPatient());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Patient).Returns(patientMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Patient>(It.IsAny<PatientForEditingDto>())).Returns(new Patient { Id = patientId });

            var commandHandler = new EditPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(result, patientId);
        }

        private async Task<Patient> TestPatient()
        {
            return new Patient
            {
                Id = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3"),
                UserId = "7fc2cc89-0b4c-4b15-a534-e4574b64a16f"
            };
        }

        private PatientDto TestPatientDto()
        {
            return new PatientDto
            {
                Id = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3"),
                UserId = "7fc2cc89-0b4c-4b15-a534-e4574b64a16f"
            };
        }
    }
}
