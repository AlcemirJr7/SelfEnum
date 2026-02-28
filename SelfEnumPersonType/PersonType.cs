using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SelfEnumPersonType.SmartEnum;


[JsonConverter(typeof(SelfEnumConverter<PersonType>))]
public readonly record struct PersonType : IParsable<PersonType>
{
    public const string Fisica = "fisica";
    public const string Juridica = "juridica";
    public const string Fantasma = "fantasma"; // vai que existe...

    public static readonly FrozenSet<string> ValidTypes = new[]
    {
        Fisica,
        Juridica,
        Fantasma
    }.ToFrozenSet();

    public string Value { get; } = string.Empty;

    private PersonType(string value)
    {
        if (!ValidTypes.Contains(value.ToLower()))
            throw new ArgumentException($"Tipo de pessoa inválido: [{value}]. Tipos válidos: [{Descriptions}]");

        Value = value.ToLower();
    }

    public static implicit operator string(PersonType tipo) => tipo.Value;
    public static implicit operator PersonType(string value) => new(value);
        
    public static readonly string Descriptions = string.Join(", ", ValidTypes);

    public static PersonType Parse(string s, IFormatProvider? provider) => new(s);

    public static bool TryParse(
        [NotNullWhen(true)] string? s, 
        IFormatProvider? provider, 
        [MaybeNullWhen(false)] out PersonType result)
    {
        if (s != null && ValidTypes.Contains(s.ToLower()))
        {
            result = new PersonType(s);
            return true;
        }
        
        result = default;
        
        return false;
    }
}
