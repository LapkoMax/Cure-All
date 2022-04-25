using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Illness;
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
    public class IllnessMediatRTests
    {
        [Fact]
        public async Task GetIllnesesCommand_ShouldReturnIllneses()
        {
            var command = new GetIllnesesCommand()
            { };

            var illMock = new Mock<IIllnessRepository>();
            illMock.Setup(repo => repo.GetAllIllnesesAsync(false)).Returns(TestIllneses());

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Illness).Returns(illMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var commandHandler = new GetIllnesesCommandHandler(mockRepoManager.Object);

            var illneses = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(illneses);
            Assert.IsAssignableFrom<IEnumerable<Illness>>(illneses);
            Assert.Equal((await TestIllneses()).Count(), illneses.Count());
        }

        [Fact]
        public async Task GetIllnessCommand_ShouldReturnCorrectIllness()
        {
            var command = new GetIllnessCommand()
            {
                illnessId  = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
            };

            var illMock = new Mock<IIllnessRepository>();
            illMock.Setup(repo => repo.GetIllnessByIdAsync(command.illnessId, false)).Returns(GetTestIllness(command.illnessId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Illness).Returns(illMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var commandHandler = new GetIllnessCommandHandler(mockRepoManager.Object);

            var illness = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(illness);
            Assert.IsAssignableFrom<Illness>(illness);
            Assert.Equal(command.illnessId, illness.Id);
        }

        [Fact]
        public async Task GetDoctorCommand_ShouldReturnNull()
        {
            var command = new GetIllnessCommand()
            {
                illnessId = Guid.NewGuid()
            };

            var illMock = new Mock<IIllnessRepository>();
            illMock.Setup(repo => repo.GetIllnessByIdAsync(command.illnessId, false)).Returns(GetTestIllness(command.illnessId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Illness).Returns(illMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var commandHandler = new GetIllnessCommandHandler(mockRepoManager.Object);

            var doctorDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(doctorDto);
        }

        [Fact]
        public async Task CreateNewIllnessCommand_ShouldCreateNewIllness()
        {
            var command = new CreateNewIllnessCommand()
            {
                illness = new IllnessForCreationDto { }
            };

            var illnessId = Guid.NewGuid();

            var illMock = new Mock<IIllnessRepository>();
            illMock.Setup(repo => repo.CreateIllness(new Illness { Id = illnessId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Illness).Returns(illMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Illness>(It.IsAny<IllnessForCreationDto>())).Returns(new Illness { Id = illnessId });

            var commandHandler = new CreateNewIllnessCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(id == illnessId);
        }

        private async Task<IEnumerable<Illness>> TestIllneses()
        {
            return new List<Illness>
            {
                new Illness
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
                },
                new Illness
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220")
                },
                new Illness
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89")
                }
            };
        }

        private async Task<Illness> GetTestIllness(Guid illnessId)
        {
            var illneses = await TestIllneses();
            return illneses.Where(ill => ill.Id == illnessId).FirstOrDefault();
        }
    }
}
