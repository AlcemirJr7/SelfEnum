namespace SelfEnumPersonType.SmartEnum;

public record PersonRequest
{
    public string Name { get; set; } = string.Empty;
    public PersonType PersonType { get; set; }
}
