using SkillsService.Domain.Entities;

namespace SkillsService.Domain.Repositories;

public interface ISkillCatalogRepository
{
    Task<IEnumerable<SkillCatalog>> GetAllAsync(CancellationToken ct);
    Task<SkillCatalog?> GetByIdAsync(Guid id, CancellationToken ct);
}
