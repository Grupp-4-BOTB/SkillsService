using SkillsService.Domain.Exceptions;

namespace SkillsService.Domain.ValueObjects;

public record OwnerId
{
    public string Value { get; }
    public OwnerId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("OwnerId cannot be null or empty.");

        Value = value.Trim();
    }

    public static OwnerId From(string value) => new OwnerId(value);
    public override string ToString() => Value;
}
