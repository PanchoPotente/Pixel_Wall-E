using System;
using System.Collections.Generic;

class Parser
{

    SyntaxToken current;
    List<SyntaxToken> syntaxTokens;
    int position = 1;

    public Parser(List<SyntaxToken> syntaxTokens)
    {
        this.syntaxTokens = syntaxTokens;
        current = syntaxTokens[0];
    }


    void MatchKind(SyntaxKind key)
    {
        if(key == current.Kind)  
            current = syntaxTokens[position++];
        else
            throw new Exception("Token " + current.Kind + " invalido. Se esperaba " + key + ".");
    }

    /*public void Language()
    {
        if(current.Kind == SyntaxKind.SpawnToken)   {
            ComandoSpawn();
            ListInsrucciones();
        }
        else  {
            throw new Exception("Se esperaba un Spawn.");
        }
    }

    public void ComandoSpawn()
    {
        if(current.Kind == SyntaxKind.SpawnToken)  {
            Match(SyntaxKind.SpawnToken);
            PosSpawn();
        }
        else
            throw new Exception("Se esperaba un Spawn.");
    }

    public void ListInsrucciones()
    {
        if(current.Kind == SyntaxKind.NewLineToken || current.Kind == SyntaxKind.EndOfFileToken)  {
            ListInsruccionesD();
        }
    }

    public void PosSpawn()
    {
        if(current.Kind == SyntaxKind.OpenParenthesisToken)   {
            Match(SyntaxKind.OpenParenthesisToken);
            Expression();
            Match(SyntaxKind.ComaToken);
            Expression();
            Match(SyntaxKind.CloseParenthesisToken);
        }
        else 
            throw new Exception("Se esperaba un (.");
    }

    public void Expression()
    {

    }

    public void ListInsruccionesD()
    {
        if(current.Kind == SyntaxKind.NewLineToken)  {
            Match(SyntaxKind.NewLineToken);
            Instruction();
            ListInsruccionesD();
        }
        else if(current.Kind == SyntaxKind.EndOfFileToken)  {}
        else
            throw new Exception("Se esperaba cambio de linea.");
    }

    public void Instruction()
    {

    }*/

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