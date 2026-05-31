using SkillsService.Domain.ValueObjects;

namespace SkillsService.Domain.Entities;

public class UserSkill
{
    public Guid Id { get; private set; }
    public OwnerId OwnerId { get; private set; }
    public Guid SkillCatalogId { get; private set; }
    public SkillCatalog SkillCatalog { get; private set; } = null!;
    public DateTime CreatedAt {  get; private set; }

    private UserSkill()
    {
    }

    public static UserSkill Create(OwnerId ownerId, Guid skillCatalogId)
    {
        return new UserSkill
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            SkillCatalogId = skillCatalogId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
