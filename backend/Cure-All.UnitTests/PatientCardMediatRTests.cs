using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.PatientCard;
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
    public class PatientCardMediatRTests
    {
        private IEnumerable<PatientCard> patientCardsTest = new List<PatientCard>()
        {
            new PatientCard
            {
                Id = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3"),
                PatientId = new Guid("7fc2cc89-0b4c-4b15-a534-e4574b64a16f")
            }
        };

        [Fact]
        public async Task GetPatientCardCommand_ShouldReturnCorrectPatientCard()
        {
            var command = new GetPatientCardCommand()
            {
                patientCardId = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3")
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.GetPatientCardByIdAsync(command.patientCardId, false)).Returns(TestPatientCard());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientCardDto>(It.IsAny<PatientCard>())).Returns(TestPatientCardDto());

            var commandHandler = new GetPatientCardCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientCardDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(patientCardDto);
            Assert.IsAssignableFrom<PatientCardDto>(patientCardDto);
            Assert.Equal(command.patientCardId, patientCardDto.Id);
        }

        [Fact]
        public async Task GetPatientCardCommand_ShouldReturnNull()
        {
            var command = new GetPatientCardCommand()
            {
                patientCardId = Guid.NewGuid()
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.GetPatientCardByIdAsync(command.patientCardId, false)).Returns(Task.FromResult<PatientCard>(null));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientCardDto>(It.IsAny<PatientCard>())).Returns((PatientCardDto)null);

            var commandHandler = new GetPatientCardCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientCardDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(patientCardDto);
        }

        [Fact]
        public async Task GetPatientCardForPatientCommand_ShouldReturnCorrectPatientCard()
        {
            var command = new GetCardForPatientCommand()
            {
                patientId = new Guid("7fc2cc89-0b4c-4b15-a534-e4574b64a16f")
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.GetPatientCardForPatientAsync(command.patientId, false)).Returns(TestPatientCard());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientCardDto>(It.IsAny<PatientCard>())).Returns(TestPatientCardDto());

            var commandHandler = new GetCardForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientCardDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(patientCardDto);
            Assert.IsAssignableFrom<PatientCardDto>(patientCardDto);
            Assert.Equal(command.patientId, patientCardDto.PatientId);
        }

        [Fact]
        public async Task GetPatientCardForPatientCommand_ShouldReturnNull()
        {
            var command = new GetCardForPatientCommand()
            {
                patientId = Guid.NewGuid()
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.GetPatientCardByIdAsync(command.patientId, false)).Returns(Task.FromResult<PatientCard>(null));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<PatientCardDto>(It.IsAny<PatientCard>())).Returns((PatientCardDto)null);

            var commandHandler = new GetCardForPatientCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var patientCardDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(patientCardDto);
        }

        [Fact]
        public async Task CreatePatientCardForPatientCommand_ShouldCreateNewPatientCard()
        {
            var command = new CreatePatientCardForPatientCommand()
            {
                patientId = new Guid("7fc2cc89-0b4c-4b15-a534-e4574b64a16f")
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.CreatePatientCardForPatient(command.patientId, new PatientCard { PatientId = command.patientId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var commandHandler = new CreatePatientCardForPatientCommandHandler(mockRepoManager.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(id == Guid.Empty);
        }

        [Fact]
        public async Task DeletePatientCommand_ShouldDeleteCorrectly()
        {
            var command = new DeletePatientCardCommand()
            {
                patientId = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3")
            };

            var patientCardMock = new Mock<IPatientCardRepository>();
            patientCardMock.Setup(repo => repo.GetPatientCardForPatientAsync(command.patientId, true)).Returns(TestPatientCard());
            patientCardMock.Setup(repo => repo.DeletePatientCard(new PatientCard { PatientId = command.patientId })).Callback(() => { patientCardsTest = new List<PatientCard>(); });

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.PatientCard).Returns(patientCardMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var commandHandler = new DeletePatientCardCommandHandler(mockRepoManager.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        private async Task<PatientCard> TestPatientCard()
        {
            return patientCardsTest.FirstOrDefault();
        }

        private PatientCardDto TestPatientCardDto()
        {
            return new PatientCardDto
            {
                Id = new Guid("df6755c4-12eb-40b8-9bf4-f8d3a58d8ea3"),
                PatientId = new Guid("7fc2cc89-0b4c-4b15-a534-e4574b64a16f")
            };
        }
    }
}
