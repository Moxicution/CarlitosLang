using CarlitosLangParser.Exceptions;

namespace LanguageParser;

public class CarlitosLangVisitor : CarlitosLangBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();

    public override object? VisitAssignment(CarlitosLangParser.AssignmentContext context)
    {
        var varName = context?.ID()?.GetText();
        var value = Visit(context?.expression() ?? throw new InvalidOperationException());

        if (varName == null || !Variables.ContainsKey(varName)) return null;

        Variables[varName] = value;

        Console.WriteLine($"{varName} - {value}");

        return null;
    }

    public override object? VisitConstant(CarlitosLangParser.ConstantContext context)
    {
        if (context.INTEGER() is { } itgr)
            return int.Parse(itgr.GetText() ?? string.Empty);

        if (context.FLOAT() is { } flt)
            return float.Parse(flt.GetText() ?? string.Empty);

        if (context.STRING() is { } str)
            return str.GetText()?[1..^1];

        if (context.BOOL() is { } bln)
            return bln.GetText() == "true";

        if (context.NULL() is not null)
            return null;

        throw new NotImplementedException();
    }

    public override object? VisitIDExpression(CarlitosLangParser.IDExpressionContext context)
    {
        var varName = context.ID().GetText();

        if (!Variables.ContainsKey(varName))
        {
            throw new VariableNotDefinedException($"Variable {varName} is not defined");
        }

        return Variables[varName];
    }

    public override object? VisitBooleanExpression(CarlitosLangParser.BooleanExpressionContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitComparisonExpression(CarlitosLangParser.ComparisonExpressionContext context)
    {
        var v0 = context?.expression(0);
        var v1 = context?.expression(1);
        var op = context?.compareOp()?.GetText();

        if (v0 == null || v1 == null || op == null)
            throw new InvalidOperationException();

        var left = Visit(v0);
        var right = Visit(v1);

        return op switch
        {
            //"==" => IsEqual(left, right),
            //"!=" => NotEqual(left, right),
            ">" => GreaterThan(left, right),
            "<" => LessThan(left, right),
            //">=" => GreaterThanOrEqual(left, right),
            //"<=" => LessThanOrEqual(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private bool IsEqual(object? left, object? right)
    {
        throw new NotImplementedException();
    }

    private bool NotEqual(object? left, object? right)
    {
        throw new NotImplementedException();
    }

    private static bool GreaterThan(object? left, object? right)
    {
        switch (left)
        {
            case int l when right is int r:
                return l > r;
            case float lf when right is float rf:
                return lf > rf;
            case int lInt when right is float rFloat:
                return lInt > rFloat;
            case float lFloat when right is int rInt:
                return lFloat > rInt;
        }

        if (left?.GetType() == null || right?.GetType() == null)
        {
            throw new ValuesCannotdNotBeComparedException("Cannot compare these values.");
        }

        throw new ValuesCannotdNotBeComparedException(
            $"Cannot compare these values of type {left.GetType()} and {right.GetType()}.");
    }

    private static bool LessThan(object? left, object? right)
    {
        switch (left)
        {
            case int l when right is int r:
                return l < r;
            case float lf when right is float rf:
                return lf < rf;
            case int lInt when right is float rFloat:
                return lInt < rFloat;
            case float lFloat when right is int rInt:
                return lFloat < rInt;
        }

        if (left?.GetType() == null || right?.GetType() == null)
        {
            throw new ValuesCannotdNotBeComparedException("Cannot compare these values.");
        }

        throw new ValuesCannotdNotBeComparedException(
            $"Cannot compare these values of type {left.GetType()} and {right.GetType()}.");
    }

    private bool GreaterThanOrEqual(object? left, object? right)
    {
        throw new NotImplementedException();
    }

    private bool LessThanOrEqual(object? left, object? right)
    {
        throw new NotImplementedException();
    }

    public override object? VisitMultiplicativeExpression(CarlitosLangParser.MultiplicativeExpressionContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitAdditiveExpression(CarlitosLangParser.AdditiveExpressionContext context)
    {
        var v0 = context?.expression(0);
        var v1 = context?.expression(1);
        var op = context?.addOp()?.GetText();

        if (v0 == null || v1 == null || op == null)
            throw new InvalidOperationException();

        var left = Visit(v0);
        var right = Visit(v1);

        return op switch
        {
            "+" => Add(left, right),
            "-" => Subtract(left, right),
            _ => throw new NotImplementedException()
        };
    }

    public override object? VisitWhileBlock(CarlitosLangParser.WhileBlockContext context)
    {
        Func<object?, bool> condition = context.WHILE()?.GetText() == "while"
                ? IsTrue
                : IsFalse
            ;

        var expression = context?.expression();
        var contextBlock = context?.block();
        var contextExpression = context?.expression();
        var contextElseIfBlock = context?.elseIfBlock();

        if (condition(Visit(expression)))
        {
            do
            {
                Visit(contextBlock);
            } while (condition(Visit(contextExpression)));
        }
        else
        {
            Visit(contextElseIfBlock);
        }

        return null;
    }

    private static bool IsTrue(object? value)
    {
        if (value is bool b)
            return b;

        throw new ValueIsNotABooleanException("Value is not boolean.");
    }

    private static bool IsFalse(object? value) => !IsTrue(value);

    private static object? Add(object? left, object? right)
    {
        switch (left)
        {
            case int l when right is int r:
                return l + r;
            case float lf when right is float rf:
                return lf + rf;
            case int lint when right is float rFloat:
                return lint + rFloat;
            case float lFloat when right is int rInt:
                return lFloat + rInt;
        }

        if (left is string || right is string)
            return $"{left}{right}";

        if (left?.GetType() == null || right?.GetType() == null)
        {
            throw new ValuesCannotBeAddedException("Cannot add these values.");
        }

        throw new ValuesCannotBeAddedException(
            $"Cannot add these values of type {left.GetType()} and {right.GetType()}.");
    }

    private static object? Subtract(object? left, object? right)
    {
        throw new NotImplementedException();
    }
}