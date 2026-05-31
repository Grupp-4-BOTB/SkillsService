using SkillsService.Domain.Entities;
using SkillsService.Domain.Exceptions;
using SkillsService.Domain.Repositories;
using SkillsService.Domain.ValueObjects;

namespace SkillsService.Application.Services;

public class SkillsAppService
{
    private readonly ISkillCatalogRepository _catalogRepository;
    private readonly IUserSkillRepository _userSkillRepository;

    public SkillsAppService(ISkillCatalogRepository catalogRepository, IUserSkillRepository userSkillRepository)
    {
        _catalogRepository = catalogRepository;
        _userSkillRepository = userSkillRepository;
    }

    public async Task<IEnumerable<SkillCatalog>> GetCatalogAsync(CancellationToken ct)
    {
        return await _catalogRepository.GetAllAsync(ct);
    }

    public async Task<IEnumerable<UserSkill>>GetUserSkillsAsync(OwnerId ownerId, CancellationToken ct)
    {
        return await _userSkillRepository.GetByOwnerIdAsync(ownerId, ct);
    }

    public async Task<UserSkill> AddUserSkillAsync(OwnerId ownerId, Guid skillCatalogId, CancellationToken ct)
    {
        var existing = await _userSkillRepository.GetByOwnerIdAsync(ownerId, ct);
        if(existing.Any(s => s.SkillCatalogId == skillCatalogId))
            throw new DomainException("User already has this skill.");

        var userSkill = UserSkill.Create(ownerId, skillCatalogId);
        await _userSkillRepository.AddAsync(userSkill, ct);
        return userSkill;
    }

    public async Task DeleteUserSkillAsync(Guid id, CancellationToken ct)
    {
        var userSkill = await _userSkillRepository.GetByIdAsync(id, ct);

        if (userSkill is null)
            return;

        await _userSkillRepository.DeleteAsync(userSkill, ct);
    }
}
