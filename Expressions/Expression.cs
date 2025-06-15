using System;
using Godot;

public abstract class Expression
{
    public abstract void Evaluate();
    public abstract object GetValue();
}

public class NumberExpression : Expression
{
    private readonly int value;
    public NumberExpression(int value)
    {
        this.value = value;
    }

    public override object GetValue() => value;
    
    public override void Evaluate()
    {
    }
}

public class BooleanExpression : Expression
{
    private readonly bool value;
    public BooleanExpression(bool value)
    {
        this.value = value;
    }
    public override void Evaluate()
    {
    }
    public override object GetValue() => value;
}

public class BinaryOpExpression : Expression
{
    private readonly Expression left;
    private readonly SyntaxToken operation;
    private readonly Expression rigth;

    public BinaryOpExpression(Expression left, SyntaxToken operation, Expression rigth)
    {
        this.left = left;
        this.operation = operation;
        this.rigth = rigth;
    }

    public override void Evaluate()
    {
        var leftValue = left.GetValue();
        var rigthValue = rigth.GetValue();
        if(operation.Type == TokenType.NumericOperation)
        {
            if (leftValue is bool || rigthValue is bool)
            throw new ExecutionError("No es posible realizar la operacion con variables de tipo bool");
        }
        if(operation.Type == TokenType.BooleanOperation)
        {
            if (leftValue is int || rigthValue is int) 
            throw new ExecutionError("No es posible realizar la operacion con variables de tipo int");
        }
        if(leftValue is string || rigthValue is string) 
        throw  new ExecutionError("No es posible realizar la operacion con variables de tipo string");
    }
    public override object GetValue()
    {
        Evaluate();
        dynamic leftValue = left.GetValue();
        dynamic rigthValue = rigth.GetValue();
        switch(operation.Kind)
        {
            case SyntaxKind.PlusToken:
            return leftValue + rigthValue;
            case SyntaxKind.MinusToken:
            return leftValue - rigthValue;
            case SyntaxKind.StarToken:
            return leftValue * rigthValue;
            case SyntaxKind.SlashToken:
            return leftValue / rigthValue;
            case SyntaxKind.DoubleStarToken:
            return Math.Pow(leftValue,rigthValue);
            case SyntaxKind.EqualsToken:
            return leftValue == rigthValue;
            case SyntaxKind.NotEqualsToken:
            return leftValue != rigthValue;
            case SyntaxKind.LessToken:
            return leftValue < rigthValue;
            case SyntaxKind.LessEqualsToken:
            return leftValue <= rigthValue;
            case SyntaxKind.GreatToken:
            return leftValue > rigthValue;
            case SyntaxKind.GreatEqualsToken:
            return leftValue >= rigthValue;
            case SyntaxKind.AndToken:
            return leftValue && rigthValue;
            case SyntaxKind.OrToken:
            return leftValue || rigthValue;
            default: 
            throw new ExecutionError("Operacion invalida, no es posible retornar un valor");
            
        }
    }
}
public abstract class Function : Expression
{

}
public class GetActualXFunction : Function
{
    public GetActualXFunction()
    {

    }
    public override object GetValue() => Canvas.GetActualX();
    public override void Evaluate()
    {
    }

}
public class GetActualYFunction : Function
{
    public GetActualYFunction()
    {

    }
    public override object GetValue() => Canvas.GetActualY();
    public override void Evaluate(){}
}
public class GetCanvasSizeFunction : Function
{
    public GetCanvasSizeFunction(){}
    public override object GetValue() => Canvas.GetCanvasSize();
    public override void Evaluate(){}
}
public class GetColorCountFunction : Function
{
    private readonly Expression color;
    private readonly Expression startX;
    private readonly Expression startY;
    private readonly Expression endX;
    private readonly Expression endY;

    public GetColorCountFunction(Expression color, Expression startX, Expression startY, Expression endX, Expression endY)
    {
        this.color = color;
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
    }
    public override object GetValue()
    {
        Evaluate();
        return Canvas.GetColorCount((string)color.GetValue(), (int)startX.GetValue(), 
        (int)startY.GetValue(), (int)endX.GetValue(), (int)endY.GetValue());
    }
    public override void Evaluate()
    {
        if(color.GetValue() is string
        && startX.GetValue() is int
        && startY.GetValue() is int
        && endX.GetValue() is int
        && endY.GetValue() is int) return;
        else throw new ExecutionError("");
    }
}