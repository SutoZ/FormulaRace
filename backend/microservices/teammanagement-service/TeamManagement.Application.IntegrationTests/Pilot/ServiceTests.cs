using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces;
using TeamManagementService.Application.Interfaces.Services;
using TeamManagementService.Domain.Models;

namespace TeamManagement.Application.IntegrationTests.Pilot;

[TestFixture]
public class ServiceTests : BaseIntegrationTest
{
    private IPilotService _pilotService = null!;
    private IUnitOfWork _unitOfWork = null!;

    [SetUp]
    public void TestSpecificSetUp()
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
        var team = new Team { Name = "Integration Test Team", Active = true };
        _dbContext.Teams.Add(team);
        await _dbContext.SaveChangesAsync();

        // 2. Create the DTO for the new pilot.
        var createDto = new PilotCreateDto("Lando Norris", "4", "NOR", "British", team.Id);

        // Act
        var resultDto = await _pilotService.CreateAsync(createDto, CancellationToken.None);

        // Manually save changes to simulate the end of a business transaction,
        // as the service layer doesn't commit the transaction itself.
        await _unitOfWork.SaveChangesAsync();

        // Assert
        // 1. Check the returned DTO from the service.
        Assert.That(resultDto, Is.Not.Null);
        Assert.That(resultDto.Name, Is.EqualTo("Lando Norris"));

        // 2. Verify directly against the database that the pilot was created.
        var pilotInDb = await _dbContext.Pilots.AsNoTracking().FirstOrDefaultAsync(p => p.Name == "Lando Norris");
        Assert.That(pilotInDb, Is.Not.Null);
        Assert.That(pilotInDb!.Number, Is.EqualTo("4"));
        Assert.That(pilotInDb.TeamId, Is.EqualTo(team.Id));
    }

    [Test]
    public async Task GetByIdAsync_WhenPilotExists_ShouldReturnPilotDetails()
    {
        // Arrange
        // 1. Seed a pilot into the database.
        var team = new Team { Name = "McLaren", Active = true };
        var pilot = new TeamManagementService.Domain.Models.Pilot("Oscar Piastri", "81", "PIA", "Australian", 0) { Team = team, Active = true };
        _dbContext.Pilots.Add(pilot);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _pilotService.GetByIdAsync(pilot.Id, CancellationToken.None);

        // Assert
        Assert.That(result.Value, Is.TypeOf<PilotDetailsDto>());
        var pilotDto = result.AsT0;
        Assert.That(pilotDto.Name, Is.EqualTo("Oscar Piastri"));
        Assert.That(pilotDto.Id, Is.EqualTo(pilot.Id));
    }

    [Test]
    public async Task UpdateAsync_WithValidData_ShouldUpdatePilotInDatabase()
    {
        // Arrange
        var team = new Team { Name = "Update Team", Active = true };
        var pilot = new TeamManagementService.Domain.Models.Pilot("Initial Name", "1", "INI", "Initial", 0) { Team = team, Active = true };
        _dbContext.Pilots.Add(pilot);
        await _dbContext.SaveChangesAsync();

        var updateDto = new PilotUpdateDto(pilot.Id, "Updated Name", "2", "UPD", "Updated", team.Id);

        // Act
        await _pilotService.UpdateAsync(pilot.Id, updateDto, CancellationToken.None);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var updatedPilotInDb = await _dbContext.Pilots.AsNoTracking().FirstOrDefaultAsync(p => p.Id == pilot.Id);
        Assert.That(updatedPilotInDb, Is.Not.Null);
        Assert.That(updatedPilotInDb!.Name, Is.EqualTo("Updated Name"));
        Assert.That(updatedPilotInDb.Number, Is.EqualTo("2"));
    }

    private async Task<TeamManagementService.Domain.Models.Pilot> SeedPilotAsync(string name = "Test Pilot")
    {
        var team = new Team { Name = "Test Team", Active = true, OwnerName = "Test Owner" };
        var pilot = new TeamManagementService.Domain.Models.Pilot(name, "99", "TST", "Testland", team.Id) { Team = team, Active = true };

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
}
