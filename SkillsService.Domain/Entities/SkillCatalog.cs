namespace SkillsService.Domain.Entities;

public class SkillCatalog
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private SkillCatalog()
    {
    }

    public static SkillCatalog Create(string name)
    {
        return new SkillCatalog
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}
