using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.DoctorDayOff;
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
    public class DoctorDayOffMediatRTests
    {
        [Fact]
        public async Task CreateDoctorDayOffsCommand_ShouldCreateNewDoctorDayOffs()
        {
            var command = new CreateDoctorDayOffsCommand()
            {
                doctorDayOffs = new List<DoctorDayOffForCreationDto> { new DoctorDayOffForCreationDto { DoctorId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656") } }
            };

            var doctorDayOffId = Guid.NewGuid();

            var doctorDayOffMock = new Mock<IDoctorDayOffRepository>();
            doctorDayOffMock.Setup(repo => repo.CreateDoctorDayOffs(new DoctorDayOffs { Id = doctorDayOffId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.DoctorDayOff).Returns(doctorDayOffMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<DoctorDayOffs>>(It.IsAny<IEnumerable<DoctorDayOffForCreationDto>>())).Returns(new List<DoctorDayOffs> { new DoctorDayOffs { Id = doctorDayOffId } });

            var commandHandler = new CreateDoctorDayOffsCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Contains(doctorDayOffId, id);
        }
    }
}
