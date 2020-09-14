using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.ApplicationContext
{
    public interface IRaceContext
    {
        DbSet<Pilot> Pilots { get; set; }
        DbSet<Team> Teams { get; set; }
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}