# SefEnum

> **Uma alternativa moderna, performática e tipada ao Smart Enum tradicional no .NET**

O **SefEnum** nasce de um problema clássico em sistemas reais: a **Obsessão por Primitivos** (*Primitive Obsession*). Ele propõe uma solução idiomática, alinhada ao .NET moderno (C# 12 / .NET 9), combinando **tipagem forte**, **semântica de valor**, **alta performance** e **integração nativa com o ecossistema ASP.NET**.

---

## 1. O Fim da *Primitive Obsession*

Em muitos sistemas, conceitos de domínio importantes — como `TipoPessoa`, `StatusPedido` ou `CategoriaCliente` — são representados por `string` ou `int`.

### O problema

```csharp
if (tipo == "fisica") { ... }
```

- `string` aceita qualquer valor (`"Banana"`, `"123"`, `null`)
- Regras de negócio ficam espalhadas pelo código
- Erros só aparecem **em tempo de execução**

### A proposta do SefEnum

O SefEnum **eleva conceitos de negócio ao status de Tipos de Primeira Classe**.

Você não passa mais:
> "uma string que você espera que represente algo"

Você passa:
> **o próprio conceito de domínio**

O compilador passa a entender o que é um `TipoPessoa`. Isso reduz estados inválidos, elimina condicionais defensivas e torna o código autoexplicativo.

---

## 2. Por que `readonly record struct` e não `class`?

Historicamente, bibliotecas de *Smart Enum* utilizam `abstract class`. O SefEnum dá um passo além ao adotar **`readonly record struct`**.

### 🚀 Performance (Stack vs Heap)

- `struct` é alocado na **stack**
- Evita pressão no **Garbage Collector**
- Em APIs de alta carga (ex: 10.000 req/s), isso representa **ganho real e mensurável**

### 🧠 Semântica de valor

- Comparações são feitas por **conteúdo**, não por referência
- `tipoA == tipoB` funciona naturalmente
- Não é necessário sobrescrever `Equals` ou `GetHashCode`

O resultado é um tipo:
- Imutável
- Leve
- Seguro
- Expressivo

---

## 3. FrozenSet no .NET 9 — Estado da Arte

O SefEnum utiliza **`FrozenSet<T>`**, introduzido nas versões mais recentes do .NET.

### ⚡ Hash otimizado

O `FrozenSet`:
- Analisa os valores **no momento da criação**
- Gera uma função de hash otimizada para aquele conjunto específico
- Oferece buscas **O(1)** mais rápidas que um `HashSet` tradicional

### 🔒 Thread-safe por natureza

Por ser **imutável**, o `FrozenSet`:
- É inerentemente thread-safe
- Elimina qualquer risco de *race condition*
- Não exige locks ou sincronizações adicionais

---

## 4. Integração com o Ecossistema ASP.NET (O verdadeiro diferencial)

O SefEnum não se limita a validar valores internamente. Ele foi pensado para **conversar nativamente com o framework**.

### 🔁 `IParsable`

Ao implementar `IParsable<T>`:

- A validação acontece **na entrada da requisição**
- Requests inválidos são rejeitados automaticamente

```http
GET /api/clientes?tipo=invalido
```

➡️ O ASP.NET nem chega a executar sua lógica de negócio.

### 📦 `JsonConverter`

- Para o mundo externo, o tipo continua parecendo uma `string`
- Nenhuma quebra de contrato de API
- O backend trabalha com **tipagem forte**

Isso permite evoluir a arquitetura **sem impactar consumidores**.

---

## 5. Para que tipo de sistema isso faz sentido?

O SefEnum brilha especialmente em:

- Sistemas complexos
- Domínios ricos
- APIs de alta performance
- Arquiteturas orientadas a domínio (DDD)
- Ambientes onde **clareza e segurança** são tão importantes quanto performance

---

## 6. O SefEnum é o sucessor do Smart Enum?

Talvez não no sentido histórico — mas **tecnicamente, sim**.

O SefEnum:
- Abandona herança em favor de **tipos de valor**
- Explora recursos modernos do runtime
- Integra-se profundamente ao ASP.NET
- Reduz custos cognitivos e operacionais

Se o *Smart Enum* foi uma resposta às limitações do passado, o **SefEnum é uma resposta ao .NET que temos hoje**.

---

## Considerações finais

> *"Foi escrito por uma IA? Sim… até aqui."* 😄

A ideia, no entanto, é profundamente humana: **reduzir ambiguidade, tornar o domínio explícito e deixar o compilador trabalhar a seu favor**.

Se você acredita que tipos contam histórias — o SefEnum ajuda a contá-las melhor.


## 💡 Apoio ao Projeto

Este projeto é mantido de forma independente.
Se ele gerou valor real para você ou sua equipe, considere apoiar sua continuidade e evolução.
A contribuição é voluntária e ajuda a manter melhorias contínuas.

### Formas de apoio
- Pix: cd8d5dc8-2fb2-4939-a4bf-359f3089a996
- Crypto: USDT -> 0x1e92451a401d54f696bb6e48faf31de073c285cf (BSC Network)
