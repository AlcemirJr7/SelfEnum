using System.Collections.Frozen;

namespace SelfEnumTipoPessoa.SmartEnum;

public readonly record struct TipoPessoaDef
{
    public const string Fisica = "fisica";
    public const string Juridica = "juridica";

    // FrozenSet para buscas O(1) ultra-rápidas e imutabilidade
    public static readonly FrozenSet<string> Types = new[]
    {
        Fisica,
        Juridica
    }.ToFrozenSet();
}
