using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.DoctorsScedule;
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
    public class DoctorSceduleMediatRTests
    {
        [Fact]
        public async Task CreateDoctorSceduleCommand_ShouldCreateNewDoctorDayOffs()
        {
            var command = new CreateDoctorSceduleCommand()
            {
                doctorScedules = new List<DoctorSceduleForCreationDto> { new DoctorSceduleForCreationDto { DoctorId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656") } }
            };

            var doctorSceduleId = Guid.NewGuid();

            var doctorDSceduleMock = new Mock<IDoctorsSceduleRepository>();
            doctorDSceduleMock.Setup(repo => repo.CreateDoctorsScedule(new DoctorsScedule { Id = doctorSceduleId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.DoctorsScedule).Returns(doctorDSceduleMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<DoctorsScedule>>(It.IsAny<IEnumerable<DoctorSceduleForCreationDto>>())).Returns(new List<DoctorsScedule> { new DoctorsScedule { Id = doctorSceduleId } });

            var commandHandler = new CreateDoctorSceduleCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Contains(doctorSceduleId, id);
        }
    }
}
