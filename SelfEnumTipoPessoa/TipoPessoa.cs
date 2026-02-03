using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SelfEnumTipoPessoa.SmartEnum;

[JsonConverter(typeof(TipoPessoaConverter))]
public readonly record struct TipoPessoa : IParsable<TipoPessoa>
{
    public static readonly FrozenSet<string> ValidTypes = TipoPessoaDef.Types;

    public string Value { get; } = string.Empty;

    private TipoPessoa(string value)
    {
        if (!ValidTypes.Contains(value))
            throw new ArgumentException($"Tipo de pessoa inválido: [{value}]. Tipos válidos: [{Descriptions}]");

        Value = value;
    }

    public static implicit operator string(TipoPessoa tipo) => tipo.Value;
    public static implicit operator TipoPessoa(string value) => new(value.ToLowerInvariant());

    public static readonly TipoPessoa Fisica = new(TipoPessoaDef.Fisica);
    public static readonly TipoPessoa Juridica = new(TipoPessoaDef.Juridica);

    public static readonly string Descriptions = string.Join(", ", ValidTypes);

    public static TipoPessoa Parse(string s, IFormatProvider? provider) => new(s.ToLowerInvariant());

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TipoPessoa result)
    {
        if (s != null && ValidTypes.Contains(s.ToLowerInvariant()))
        {
            result = new TipoPessoa(s.ToLowerInvariant());
            return true;
        }
        
        result = default;
        
        return false;
    }
}
