﻿using AutoMapper;
using Hahn.Assesment.Application.DTOs.SeverityDtos;
using Hahn.Assesment.Application.Services.AlertApp;
using Hahn.Assesment.Domain.Entities;
using Hahn.Assesment.Persistence.Repositories.Interfaces;
using Hahn.Assesment.Persistence.Services.AlertApi;
using Moq;

namespace Hahn.Assesment.Application.Tests.Services
{
    public class AlertAppServiceTests
    {
        private readonly Mock<IAlertApiService> _alertApiServiceMock;
        private readonly Mock<IAlertRepository> _alertRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AlertAppService _alertAppService;

        public AlertAppServiceTests()
        {
            _alertApiServiceMock = new Mock<IAlertApiService>();
            _alertRepositoryMock = new Mock<IAlertRepository>();
            _mapperMock = new Mock<IMapper>();
            _alertAppService = new AlertAppService(_alertApiServiceMock.Object, _alertRepositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task GetAlertAsync_ShouldReturnMappedAlertDto()
        {
            // Arrange
            var alertEntity = new AlertEntity { Id = Guid.NewGuid() };
            var alertDto = new AlertDto { Id = alertEntity.Id };

            _alertRepositoryMock.Setup(repo => repo.GetAlertAsync()).ReturnsAsync(alertEntity);
            _mapperMock.Setup(m => m.Map<AlertDto>(alertEntity)).Returns(alertDto);

            // Act
            var result = await _alertAppService.GetAlertAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(alertEntity.Id, result.Id);
            _alertRepositoryMock.Verify(repo => repo.GetAlertAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<AlertDto>(alertEntity), Times.Once);
        }
    }
}
