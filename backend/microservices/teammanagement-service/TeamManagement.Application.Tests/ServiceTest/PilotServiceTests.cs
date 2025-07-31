using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OneOf.Types;
using TeamManagement.Application.Helper.Builders;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Services;

namespace TeamManagement.Application.Tests.ServiceTest;

[TestFixture]
[Category("unit")]
public class PilotServiceTests
{
    private Mock<IPilotRepository> pilotRepositoryMock;
    private Mock<ILogger<PilotService>> _loggerMock;
    private Mock<IValidator<PilotDeleteDto>> _deleteValidatorMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IValidator<PilotFilterDto>> _getByIdValidatorMock;
    private PilotService pilotService;

    [SetUp]
    public void Setup()
    {
        // Arrange: Initialize mocks for each test
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        pilotRepositoryMock = new Mock<IPilotRepository>();
        _loggerMock = new Mock<ILogger<PilotService>>();
        _deleteValidatorMock = new Mock<IValidator<PilotDeleteDto>>();
        _getByIdValidatorMock = new Mock<IValidator<PilotFilterDto>>();

        // Setup the UnitOfWork mock to return the pilot repository mock
        _unitOfWorkMock.Setup(uow => uow.Pilot).Returns(pilotRepositoryMock.Object);

        // Instantiate the service with the mocked dependencies
        pilotService = new PilotService(
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _deleteValidatorMock.Object,
            _getByIdValidatorMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup: Reset all mocks after each test
        pilotRepositoryMock.Reset();
        _loggerMock.Reset();
        _deleteValidatorMock.Reset();
        _unitOfWorkMock.Reset();
        _getByIdValidatorMock.Reset();
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotExists_ReturnsPilotDetailsDto()
    {
        // Arrange
        var pilotId = 1;
        var pilotDetails = new PilotDetailsDtoBuilder().WithId(pilotId).WithName("Lewis Hamilton").WithCode("44").WithCode("HAM").WithNationality("British").Build();

        pilotRepositoryMock.Setup(repo => repo.GetByIdAsync(pilotDetails.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pilotDetails);

        // Act
        var result = await pilotService.GetByIdAsync(pilotId, CancellationToken.None);

        // Assert

        Assert.That(result.TryPickT0(out PilotDetailsDto pilotDto, out _), Is.True);

        Assert.That(pilotDto.Id, Is.EqualTo(pilotId));
        pilotRepositoryMock.Verify(repo => repo.GetByIdAsync(pilotId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var pilotId = 99;
        pilotRepositoryMock.Setup(repo => repo.GetByIdAsync(pilotId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new NotFound());

        // Act
        var result = await pilotService.GetByIdAsync(pilotId, CancellationToken.None);

        // Assert
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value, Is.TypeOf<NotFound>());
    }

    [Test]
    public async Task CreateAsync_WithValidDto_ReturnsPilotListDto()
    {
        // Arrange
        var createDto = new PilotCreateDtoBuilder().WithName("Charles Leclerc").WithNumber("16").WithCode("LEC").WithNationality("Monegasque").WithTeamId(1).Build();

        var pilot = new PilotBuilder()
            .WithName(createDto.Name)
            .WithNumber(createDto.Number)
            .WithCode(createDto.Code)
            .WithNationality(createDto.Nationality)
            .WithTeamId(createDto.TeamId)
            .Build();

        pilotRepositoryMock.Setup(repo => repo.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pilot);

        // Act
        var result = await pilotService.CreateAsync(createDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(pilot.Name, Is.EqualTo(result.Name));
        pilotRepositoryMock.Verify(repo => repo.CreateAsync(createDto, It.IsAny<CancellationToken>()), Times.Once);

        // We do not verify SaveChangesAsync because it's not the service's responsibility in your design.
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void CreateAsync_WithoutValidDto_ThrowsArgumentNullException()
    {
        // Arrange
        PilotCreateDto invalidDto = null;

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await pilotService.CreateAsync(invalidDto, CancellationToken.None));

        Assert.That(ex.ParamName, Is.EqualTo("createDto"));
    }

    [Test]
    public async Task CreateAsync_WithValidDto_ReturnPilotListDto()
    {
        // Arrange
        var createDto = new PilotCreateDtoBuilder()
            .WithName("Max Verstappen")
            .Build();

        var pilot = new PilotBuilder()
            .WithName(createDto.Name)
            .Build();

        pilotRepositoryMock.Setup(repo => repo.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pilot);
        //Act
        var result = await pilotService.CreateAsync(createDto, CancellationToken.None);

        //Assert
        Assert.That(result.Name, Is.EqualTo(createDto.Name));
    }

}