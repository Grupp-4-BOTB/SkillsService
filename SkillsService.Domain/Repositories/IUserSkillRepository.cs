using SkillsService.Domain.Entities;
using SkillsService.Domain.ValueObjects;

namespace SkillsService.Domain.Repositories;

public interface IUserSkillRepository
{
    Task<IEnumerable<UserSkill>> GetByOwnerIdAsync(OwnerId ownerId, CancellationToken ct);
    Task AddAsync(UserSkill userSkill, CancellationToken ct);
    Task<UserSkill?> GetByIdAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(UserSkill userSkill, CancellationToken ct);
}
