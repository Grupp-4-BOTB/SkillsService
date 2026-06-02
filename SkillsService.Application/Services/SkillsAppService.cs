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

    public async Task<IEnumerable<UserSkill>>GetUserSkillsAsync(string ownerId, CancellationToken ct)
    {
        var owner = OwnerId.From(ownerId);
        return await _userSkillRepository.GetByOwnerIdAsync(owner, ct);
    }

    public async Task<UserSkill> AddUserSkillAsync(string ownerId, Guid skillCatalogId, CancellationToken ct)
    {
        var owner = OwnerId.From(ownerId);
        var existing = await _userSkillRepository.GetByOwnerIdAsync(owner, ct);
        if(existing.Any(s => s.SkillCatalogId == skillCatalogId))
            throw new DomainException("User already has this skill.");

        var userSkill = UserSkill.Create(owner, skillCatalogId);
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

    public async Task<UserSkill> AddCustomSkillAsync(string ownerId, string skillName, CancellationToken ct)
    {
        var owner = OwnerId.From(ownerId);

        var catalogSkill = SkillCatalog.Create(skillName);
        await _catalogRepository.AddAsync(catalogSkill, ct);

        var userSkill = UserSkill.Create(owner, catalogSkill.Id);
        await _userSkillRepository.AddAsync(userSkill, ct);
        return userSkill;
    }
}
