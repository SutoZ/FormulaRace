using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OneOf.Types;
using TeamManagement.Application.Helper.Builders;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagement.Application.IntegrationTests.Pilot;

[TestFixture]
public class ServiceTests : BaseIntegrationTest
{
    private IPilotService _pilotService = null!;
    private IUnitOfWork _unitOfWork = null!;

    [SetUp]
    public void Setup()
    {
        // The base SetUp creates the scope and dbContext.
        // We resolve the services we want to test from the DI container.
        _pilotService = _scope.ServiceProvider.GetRequiredService<IPilotService>();
        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Test]
    public async Task CreateAsync_WithValidData_ShouldAddPilotToDatabase()
    {
        // Arrange
        // 1. Seed a team to which the pilot can be assigned.
        var pilot = await SeedPilotAsync();

        // 2. Create the DTO for the new pilot.
        var createDto = new PilotCreateDtoBuilder().WithName("Lando Norris").WithNumber("4").WithNationality("British").WithCode("NOR").WithTeamId(pilot.Team.Id).Build();

        // Act
        var resultDto = await _pilotService.CreateAsync(createDto, CancellationToken.None);

        // Manually save changes to simulate the end of a business transaction,
        // as the service layer doesn't commit the transaction itself.

        await _unitOfWork.SaveChangesAsync();

        // Assert
        // 1. Check the returned DTO from the service.
        Assert.That(resultDto, Is.Not.Null);
        Assert.That(resultDto.Name, Is.EqualTo(createDto.Name));

        // 2. Verify directly against the database that the pilot was created.
        var pilotInDb = await _dbContext.Pilots.AsNoTracking().FirstOrDefaultAsync(p => p.Id == resultDto.Id);

        Assert.That(pilotInDb, Is.Not.Null);
        Assert.That(pilotInDb!.Number, Is.EqualTo(resultDto.Number));
        Assert.That(pilotInDb.Code, Is.EqualTo(resultDto.Code));
        Assert.That(pilotInDb.Nationality, Is.EqualTo(resultDto.Nationality));
        Assert.That(pilotInDb.TeamId, Is.EqualTo(pilot.Team.Id));
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotExists_ShouldReturnPilotDetails()
    {
        // Arrange
        // 1. Seed a pilot into the database.
        var pilot = await SeedPilotAsync("Oscar Piastri");

        // Act
        var result = await _pilotService.GetByIdAsync(pilot.Id, CancellationToken.None);

        // Assert
        Assert.That(result.Value, Is.TypeOf<PilotDetailsDto>());
        result.TryPickT0(out PilotDetailsDto detailsDto, out _);
        Assert.That(detailsDto.Name, Is.EqualTo(pilot.Name));
        Assert.That(detailsDto.Number, Is.EqualTo(pilot.Number));
        Assert.That(detailsDto.Code, Is.EqualTo(pilot.Code));
        Assert.That(detailsDto.Nationality, Is.EqualTo(pilot.Nationality));
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotDoesNotExist_ShouldReturnValidationException()
    {
        // Arrange
        // Use an ID that is not present in the database (e.g., -1 or a very large number)
        int nonExistentPilotId = -1;

        // Act && Assert
        Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _pilotService.GetByIdAsync(nonExistentPilotId, CancellationToken.None));
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        // Use an ID that is not present in the database (e.g., -1 or a very large number)
        int nonExistentPilotId = 99999;

        // Act
        var result = await _pilotService.GetByIdAsync(nonExistentPilotId, CancellationToken.None);

        result.TryPickT1(out NotFound notFound, out _);

        //Assert
        Assert.That(notFound, Is.Not.Null);
        Assert.That(notFound, Is.TypeOf<NotFound>());
    }
    [Test]
    public async Task UpdateAsync_WithValidData_ShouldUpdatePilotInDatabase()
    {
        // Arrange
        var pilot = await SeedPilotAsync("Original Pilot");
        var updateDto = new PilotUpdateDtoBuilder().WithName("Updated Name").WithNationality("HUN").WithCode("UPD").WithNumber("2").Build();

        // Act
        await _pilotService.UpdateAsync(pilot.Id, updateDto, CancellationToken.None);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var updatedPilotInDb = await _dbContext.Pilots.AsNoTracking().FirstOrDefaultAsync(p => p.Id == pilot.Id);

        Assert.That(updatedPilotInDb, Is.Not.Null);
        Assert.That(updatedPilotInDb.Name, Is.EqualTo(updateDto.Name));
        Assert.That(updatedPilotInDb.Code, Is.EqualTo(updateDto.Code));
        Assert.That(updatedPilotInDb.Number, Is.EqualTo(updateDto.Number));
        Assert.That(updatedPilotInDb.Nationality, Is.EqualTo(updateDto.Nationality));
    }

    private async Task<TeamManagementService.Domain.Models.Pilot> SeedPilotAsync(string name = "Test Pilot")
    {
        var team = new TeamBuilder().WithName("Test Team").WithOwnerName("Test Owner").Build();
        var pilot = new PilotBuilder().WithName(name).WithNumber("99").WithNationality("Test land").WithCode("TST").WithTeam(team).Build();

        _dbContext.Pilots.Add(pilot);
        await _dbContext.SaveChangesAsync();

        return pilot;
    }

    [Test]
    public async Task DeleteAsync_WhenPilotExists_ShouldSoftDeletePilotInDatabase()
    {
        // Arrange
        var pilot = await SeedPilotAsync("ToDetete");
        _dbContext.Entry(pilot).State = EntityState.Detached;

        // Act
        var result = await _pilotService.DeleteAsync(pilot.Id, CancellationToken.None);
        await _unitOfWork.SaveChangesAsync();

        // Assert

        // Verify soft delete by checking the 'Active' flag.
        // Use IgnoreQueryFilters to ensure we can see soft-deleted entities.
        var deletedPilotInDb = await _dbContext.Pilots.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == pilot.Id);
        Assert.That(deletedPilotInDb!.Active, Is.False);
    }

    [Test]
    public async Task DeleteAsync_WhenPilotDoesNotExist_ShouldThrowValidationException()
    {
        // Arrange
        var notExistingPilotId = -1;

        // Act && Assert
        Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _pilotService.DeleteAsync(notExistingPilotId, CancellationToken.None));
    }

    [Test]
    public async Task DeleteAsync_WhenPilotDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var notExistingPilotId = 9999;

        // Act 
        var result = await _pilotService.DeleteAsync(notExistingPilotId, CancellationToken.None);

        //Assert
        result.TryPickT1(out NotFound notFound, out _);
        Assert.That(notFound, Is.Not.Null);
        Assert.That(notFound, Is.TypeOf<NotFound>());
    }
}
