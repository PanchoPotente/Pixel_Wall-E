using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

class ExpressionParser
{

    private readonly SyntaxToken[][] code;
    private int line = 0;
    private SyntaxToken[] tokens {get => code[line];}
    private int position = 0;

    SyntaxToken current {get => tokens[position];}
    public ExpressionParser(SyntaxToken[][] code)
    {
        this.code = code;
    }

    void MatchKind(SyntaxKind kind)
    {
        if (current.Kind == kind) Next();
        else throw new SemanticError("Token " + current.Kind + " invalido. Se esperaba " + kind + ".");
    }
    void Next() => position++;
    void NextLine() => SetLine(line + 1);
    void SetLine(int n)
    {
        line = n;
        position = 0;
    }


    public void ParseLine()
    {
        if(current.IsInstruction())
        {
            Instruction instruction = ParseInstruction();
            instruction.Execute();
            NextLine();
        }
        else if(current.Kind == SyntaxKind.VariableToken)
        {
            SyntaxToken variable = current;
            MatchKind(SyntaxKind.VariableToken);
            MatchKind(SyntaxKind.ArrowToken);
            Expression expression = ParseExpression();
            Variable.Asing(variable.Text, expression);
            NextLine();
        }
        else if(current.Kind ==  SyntaxKind.GoToToken)
        {
            MatchKind(SyntaxKind.GoToToken);
            string tagName = current.Text;
            MatchKind(SyntaxKind.CapturedTagToken);
            MatchKind(SyntaxKind.CloseCorcheteToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression = ParseExpression();
            if(expression.GetValue() is bool)
            {
                if((bool)expression.GetValue()) SetLine(TagIndex.GetLine(tagName));
            }
            else throw new ExecutionError("Se esperaba un valor de tipo bool");
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            NextLine();
        }
        else if(current.Kind == SyntaxKind.TagToken)
        {
            NextLine();
        }
        else if(current.Kind == SyntaxKind.EndOfLineToken)
        {
            NextLine();
        }
    }



    public Instruction ParseInstruction()
    {
        if(current.Kind == SyntaxKind.SpawnToken)
        {
            MatchKind(SyntaxKind.SpawnToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression posX = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression posY = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new SpawnInstruction(posX, posY);
        }
        else if(current.Kind == SyntaxKind.FillToken)
        {
            MatchKind(SyntaxKind.FillToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new FillInstruction();
        }
        else if(current.Kind == SyntaxKind.ColorToken)
        {
            MatchKind(SyntaxKind.ColorToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression color = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new ColorInstruction(color);
        }
        else if(current.Kind == SyntaxKind.SizeToken)
        {
            MatchKind(SyntaxKind.SizeToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression size = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new SizeInstruction(size);
        }
        else if(current.Kind == SyntaxKind.DrawLineToken)
        {
            MatchKind(current.Kind);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression dirX = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression dirY = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression distance = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new DrawLineInstruction(dirX,dirY,distance);
        }
        else if(current.Kind == SyntaxKind.DrawCircleToken)
        {
            MatchKind(current.Kind);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression dirX = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression dirY = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression radius = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new DrawCircleInstruction(dirX,dirY,radius);
        }
        else if(current.Kind == SyntaxKind.DrawRectangleToken)
        {
            MatchKind(current.Kind);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression dirX = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression dirY = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression distance = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression width = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression height = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            MatchKind(SyntaxKind.EndOfLineToken);
            return new DrawRectangleInstruction(dirX,dirY,distance,width,height);
        }
        else throw new SemanticError("Instruccion Invalida");
    }

    private Expression ParseFunction()
    {
        switch (current.Kind)
        {
            case SyntaxKind.GetActualXToken:
            MatchKind(SyntaxKind.GetActualXToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new GetActualXFunction();

            case SyntaxKind.GetActualYToken:
            MatchKind(SyntaxKind.GetActualYToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new GetActualYFunction();

            case SyntaxKind.GetCanvasSizeToken:
            MatchKind(SyntaxKind.GetCanvasSizeToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new GetCanvasSizeFunction();

            case SyntaxKind.GetColorCountToken:
            MatchKind(SyntaxKind.GetColorCountToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression1 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression2 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression3 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression4 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression5 = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new GetColorCountFunction(expression1,expression2,expression3,expression4,expression5);

            case SyntaxKind.IsBrushColorToken:
            MatchKind(SyntaxKind.IsBrushColorToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression6 = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new IsBrushColorFunction(expression6);

            case SyntaxKind.IsBrushSizeToken:
            MatchKind(SyntaxKind.IsBrushSizeToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression7 = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new IsBrushSizeFunction(expression7);

            case SyntaxKind.IsCanvasColorToken:
            MatchKind(SyntaxKind.IsCanvasColorToken);
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression8 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression9 = ParseExpression();
            MatchKind(SyntaxKind.ComaToken);
            Expression expression10 = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return new IsCanvasColorFunction(expression8,expression9,expression10);
            default:
            throw new ExecutionError("Funcion invalida o no definida");
        }
    }
    private Expression PrimaryExpresion()
    {
        SyntaxToken token = current;
        if (token.Kind == SyntaxKind.OpenParenthesisToken)
        {
            MatchKind(SyntaxKind.OpenParenthesisToken);
            Expression expression = ParseExpression();
            MatchKind(SyntaxKind.CloseParenthesisToken);
            return expression;
        }
        else if(token.Kind == SyntaxKind.NumberToken)
        {
            MatchKind(SyntaxKind.NumberToken);
            if(int.TryParse(token.Text, out int result))
            {
            return new NumberExpression(result);
            }
            else throw new ExecutionError("El valor tiene que ser un numero entero");
        }
        else if(token.Kind == SyntaxKind.MinusToken)
        {
            MatchKind(SyntaxKind.MinusToken);
            return new BinaryOpExpression(new NumberExpression(0),token,ParseExpression());
        }
        else if(token.IsFunction())
        {
            return ParseFunction();
        }
        else if (token.Kind == SyntaxKind.StringToken)
        {
            MatchKind(SyntaxKind.StringToken);
            return new StringExpression(token.Text);
        }
        else if(token.Kind == SyntaxKind.VariableToken)
        {
            MatchKind (SyntaxKind.VariableToken);
            return new VariableExpression(token.Text);
        }
        else throw new SemanticError($"Factor inesperado :{token.Text}");
    }

    private Expression PowerExpression()
    {
        Expression expression = PrimaryExpresion();
        while (current.Kind == SyntaxKind.DoubleStarToken)
        {
            SyntaxToken token = current;
            MatchKind(SyntaxKind.DoubleStarToken);
            expression = new BinaryOpExpression(expression,token, PrimaryExpresion());
        } 
        return expression;
    }

    private Expression MultDivExpression()
    {
        Expression expression = PowerExpression();
        while (current.Kind == SyntaxKind.StarToken || current.Kind == SyntaxKind.SlashToken)
        {
            SyntaxToken token = current;
            MatchKind(token.Kind);
            expression = new BinaryOpExpression(expression, token, PowerExpression());
        }
        return expression;
    }
    private Expression SumDiffExpression()
    {
        Expression expression = MultDivExpression();
        while (current.Kind == SyntaxKind.PlusToken || current.Kind == SyntaxKind.MinusToken)
        {
            SyntaxToken token = current;
            MatchKind(token.Kind);
            expression = new BinaryOpExpression(expression, token, MultDivExpression());
        }
        return expression; 
    }
    private Expression CompExpression()
    {
        Expression expression = SumDiffExpression();
        while (current.Kind == SyntaxKind.LessToken || current.Kind == SyntaxKind.LessEqualsToken
        || current.Kind == SyntaxKind.EqualsToken || current.Kind == SyntaxKind.NotEqualsToken
        || current.Kind == SyntaxKind.GreatToken || current.Kind == SyntaxKind.GreatEqualsToken)
        {
            SyntaxToken token = current;
            MatchKind(token.Kind);
            expression = new BinaryOpExpression(expression, token, SumDiffExpression());
        }
        return expression;
    }

    private Expression OrExpression()
    {
        Expression expression = CompExpression();
        while (current.Kind == SyntaxKind.OrToken)
        {
            SyntaxToken token = current;
            MatchKind(token.Kind);
            expression = new BinaryOpExpression(expression, token, CompExpression());
        }
        return expression;
    }
    private Expression AndExpression()
    {
        Expression expression = OrExpression();
        while (current.Kind == SyntaxKind.AndToken)
        {
            SyntaxToken token = current;
            MatchKind(token.Kind);
            expression = new BinaryOpExpression(expression, token, OrExpression());
        }
        return expression;
    }

    public Expression ParseExpression()
    {
        return AndExpression();
    }
}
