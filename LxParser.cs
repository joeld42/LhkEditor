
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using Superpower;
using Superpower.Util;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace LhkEditor.LxParser;


// pretty much a direct copy of Superpower's json example
enum LxToken
{
    [Token(Example = "{")] LBracket,

    [Token(Example = "}")] RBracket,

    [Token(Example = "[")] LSquareBracket,

    [Token(Example = "]")] RSquareBracket,

    [Token(Example = ":")] Colon,

    [Token(Example = ",")] Comma,

    [Token(Example = "=")] Equals,

    [Token(Example = "//")] Comment,

    Identifier,
    String,
    Number,

}

static class LxTokenizer
{

    static TextParser<Unit> LxStringToken { get; } =
        from open in Character.EqualTo('"')
        from content in Character.EqualTo('\\').IgnoreThen(Character.AnyChar).Value(Unit.Value).Try()
            .Or(Character.Except('"').Value(Unit.Value))
            .IgnoreMany()
        from close in Character.EqualTo('"')
        select Unit.Value;

    private static TextParser<Unit> LxIdentifierToken { get; } =
        from first in Character.LetterOrDigit
        from rest in (Character.LetterOrDigit.Or(Character.EqualTo('_'))).IgnoreMany()
        select Unit.Value;

    static TextParser<Unit> LxNumberToken { get; } =
        from sign in Character.EqualTo('-').OptionalOrDefault()
        from first in Character.Digit
        from rest in Character.Digit.Or(Character.In('.', 'e', 'E', '+', '-')).IgnoreMany()
        select Unit.Value;


    public static Tokenizer<LxToken> Instance { get; } =
        new TokenizerBuilder<LxToken>()
            .Ignore(Span.WhiteSpace)
            .Ignore(Comment.CPlusPlusStyle)
            .Match(Identifier.CStyle, LxToken.Identifier)
            .Match(Character.EqualTo('{'), LxToken.LBracket)
            .Match(Character.EqualTo('}'), LxToken.RBracket)
            .Match(Character.EqualTo(':'), LxToken.Colon)
            .Match(Character.EqualTo(','), LxToken.Comma)
            .Match(Character.EqualTo('['), LxToken.LSquareBracket)
            .Match(Character.EqualTo(']'), LxToken.RSquareBracket)
            .Match(Character.EqualTo('='), LxToken.Equals)
            .Match(LxStringToken, LxToken.String)
            .Match(LxNumberToken, LxToken.Number, requireDelimiters: true)
            .Match(LxIdentifierToken, LxToken.Identifier, requireDelimiters: true)
            .Build();
}

static class LxTextParsers
{
    public static TextParser<string> String { get; } =
        from open in Character.EqualTo('"')
        from chars in Character.ExceptIn('"', '\\')
            .Or(Character.EqualTo('\\')
                .IgnoreThen(
                    Character.EqualTo('\\')
                        .Or(Character.EqualTo('"'))
                        .Or(Character.EqualTo('/'))
                        .Or(Character.EqualTo('b').Value('\b'))
                        .Or(Character.EqualTo('f').Value('\f'))
                        .Or(Character.EqualTo('n').Value('\n'))
                        .Or(Character.EqualTo('r').Value('\r'))
                        .Or(Character.EqualTo('t').Value('\t'))
                        .Or(Character.EqualTo('u').IgnoreThen(
                            Span.MatchedBy(Character.HexDigit.Repeat(4))
                                .Apply(Numerics.HexDigitsUInt32)
                                .Select(cc => (char) cc)))
                        .Named("escape sequence")))
            .Many()
        from close in Character.EqualTo('"')
        select new string(chars);

    public static TextParser<string> Identifier { get; } =
        from chars in Character.AnyChar.Many()
        select new string(chars);


    public static TextParser<double> Number { get; } =
        from sign in Character.EqualTo('-').Value(-1.0).OptionalOrDefault(1.0)
        from whole in Numerics.Natural.Select(n => double.Parse(n.ToStringValue()))
        from frac in Character.EqualTo('.')
            .IgnoreThen(Numerics.Natural)
            .Select(n => double.Parse(n.ToStringValue()) * Math.Pow(10, -n.Length))
            .OptionalOrDefault()
        from exp in Character.EqualToIgnoreCase('e')
            .IgnoreThen(Character.EqualTo('+').Value(1.0)
                .Or(Character.EqualTo('-').Value(-1.0))
                .OptionalOrDefault(1.0))
            .Then(expsign => Numerics.Natural.Select(n => double.Parse(n.ToStringValue()) * expsign))
            .OptionalOrDefault()
        select (whole + frac) * sign * Math.Pow(10, exp);
}

static class LxFormatParser
{
    static TokenListParser<LxToken, object> LxString { get; } =
        Token.EqualTo(LxToken.String)
            .Apply(LxTextParsers.String)
            .Select(s => (object) s);

    static TokenListParser<LxToken, object> LxNumber { get; } =
        Token.EqualTo(LxToken.Number)
            .Apply(LxTextParsers.Number)
            .Select(n => (object) n);

    private static TokenListParser<LxToken, object> LxIdentifier { get; } =
        Token.EqualTo(LxToken.Identifier)
            .Apply(LxTextParsers.Identifier)
            .Select(ident => (object) ident);

    static TokenListParser<LxToken, object> LxObject { get; } =
        from begin in Token.EqualTo(LxToken.LBracket)
        from properties in LxIdentifier
            .Named("property name")
            .Then(name => Token.EqualTo(LxToken.Colon).Or(Token.EqualTo(LxToken.Equals))
                .IgnoreThen(Parse.Ref(() => LxValue!)
                    .Select(value => KeyValuePair.Create((string) name, value))))
            .ManyDelimitedBy(Token.EqualTo(LxToken.Comma).OptionalOrDefault(),
                end: Token.EqualTo(LxToken.RBracket))
        select (object) new Dictionary<string, object>(properties);

    static TokenListParser<LxToken, object> LxArray { get; } =
        from begin in Token.EqualTo(LxToken.LSquareBracket)
        from values in Parse.Ref(() => LxValue!)
            .ManyDelimitedBy(Token.EqualTo(LxToken.Comma).OptionalOrDefault(),
                //end: Token.EqualTo( LxToken.Comma ).Optional().IgnoreThen( Token.EqualTo(LxToken.RSquareBracket) ) )
                end: Token.EqualTo(LxToken.RSquareBracket))
        select (object) values;

    static TokenListParser<LxToken, object> LxTrue { get; } =
        Token.EqualToValue(LxToken.Identifier, "true").Value((object) true);

    static TokenListParser<LxToken, object> LxFalse { get; } =
        Token.EqualToValue(LxToken.Identifier, "false").Value((object) false);

    static TokenListParser<LxToken, object?> LxNull { get; } =
        Token.EqualToValue(LxToken.Identifier, "null").Value((object?) null);

    static TokenListParser<LxToken, object?> LxValue { get; } =
        LxString.AsNullable()
            .Or(LxNumber.AsNullable())
            .Or(LxObject.AsNullable())
            .Or(LxArray.AsNullable())
            .Or(LxTrue.AsNullable())
            .Or(LxFalse.AsNullable())
            .Or(LxNull)
            .Named("value");

    static TokenListParser<LxToken, object?> LxDocument { get; } = LxValue.AtEnd();

    public static bool TryParse(string lxtext, out object? value, [MaybeNullWhen(true)] out string error,
        out Position errorPosition)
    {
        var tokens = LxTokenizer.Instance.TryTokenize(lxtext);
        if (!tokens.HasValue)
        {
            value = null;
            error = tokens.ToString();
            errorPosition = tokens.ErrorPosition;
            return false;
        }

        var parsed = LxDocument.TryParse(tokens.Value);
        if (!parsed.HasValue)
        {
            value = null;
            error = parsed.ToString();
            errorPosition = parsed.ErrorPosition;
            return false;
        }

        value = parsed.Value;
        error = null;
        errorPosition = Position.Empty;
        return true;
    }

}