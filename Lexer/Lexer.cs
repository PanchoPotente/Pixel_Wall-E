using System.Collections.Generic;
using System.Linq;

class Lexer
{
    private readonly string _text;
    private int _position;
    private char Current
    {
        get
        {
            return Peek(0);
        }
    }
    public Lexer(string text)
    {
        _position = 0;
        _text = NormalizeLine(text);
    }
    private char Peek(int offset)
    {
        if(_position + offset >= _text.Length) return '\0';
        else return _text[_position + offset];
    }
    private void Next() => _position++;
    public SyntaxToken GetToken()
    {
        if(char.IsWhiteSpace(Current)) SkipWhiteSpace();

        if(char.IsLetter(Current))
        {
            string word = CompleteWord();
            if(Index.IsKeyWord(word)) return new SyntaxToken(word, Index.GetKind(word), null);
            else if(Peek(1) == '\0') return new SyntaxToken(word, SyntaxKind.TagToken, null);
            else return new SyntaxToken(word, SyntaxKind.VariableToken, null);
        }

        if(char.IsDigit(Current))
        {
            string number = CompleteNumber();
            return new SyntaxToken(number, SyntaxKind.NumberToken, null);
            
        }
        
        if (Current == '+') return new SyntaxToken("+", SyntaxKind.PlusToken, null);
        else if (Current == '-') return new SyntaxToken("-", SyntaxKind.MinusToken, null);
        else if (Current == '*')
        {
            if(Peek(1) == '*')
            {
                Next();
                return new SyntaxToken("**", SyntaxKind.DoubleStarToken, null);
            }
            else return new SyntaxToken("*", SyntaxKind.StarToken, null);
        }
        else if (Current == '/') return new SyntaxToken("/", SyntaxKind.SlashToken, null);
        else if (Current == '%') return new SyntaxToken("%", SyntaxKind.PorcentualToken, null);
        else if (Current == '(') return new SyntaxToken("(", SyntaxKind.OpenParenthesisToken, null);
        else if (Current == ')') return new SyntaxToken(")", SyntaxKind.CloseParenthesisToken, null);
        else if (Current == '<')
        {
            if (Peek(1) == '-')
            {
                Next(); 
                return new SyntaxToken("<-", SyntaxKind.ArrowToken, null);
            }
            else if (Peek(1) == '=') 
            {
                Next();
                return new SyntaxToken("<=", SyntaxKind.LessEqualsToken, null);
            }
            else return new SyntaxToken("<", SyntaxKind.LessEqualsToken, null);
        }
        else if (Current == '=' && Peek(1) == '=') 
        {
            Next();
            return new SyntaxToken("==", SyntaxKind.EqualsToken, null);
        }        
        else if (Current == '&' && Peek(1) == '&')
        {
            Next();
            return new SyntaxToken("&&", SyntaxKind.AndToken, null);
        }
        else if (Current == '|' && Peek(1) == '|') 
        {
            Next();
            return new SyntaxToken("||", SyntaxKind.OrToken, null);
        }
        else if (Current == '>')
        {
            if( Peek(1) == '=')
            { 
                Next();
                return new SyntaxToken(">=", SyntaxKind.GreatEqualsToken, null);
            }
            else return new SyntaxToken(">", SyntaxKind.GreatToken, null);
        } 
        else if(Current == ',') return new SyntaxToken(",", SyntaxKind.ComaToken, null);
        else if (Current == '"') return CompleteString();
        else if (Current == '!' && Peek(1) == '=')
        {
            Next();
            return new SyntaxToken("!=", SyntaxKind.NotEqualsToken, null);
        }
        else if(Current == '\0') return new SyntaxToken("\0", SyntaxKind.EndOfLineToken, null);
        else throw new SyntacticError($"La expresion {Current} es un token Invalido");
        
    }

    private void SkipWhiteSpace()
    {
        while(char.IsWhiteSpace(Current))
        {
            Next();
        }
    }

    private string CompleteWord()
    {
        string word = Current + "";
        while (char.IsLetterOrDigit(Peek(1)) || Peek(1) == '_')
        {
            word += Peek(1);
            Next();
        }
        return word;
    }

    private string CompleteNumber()
    {
        string number = "" + Current;
        while (char.IsDigit(Peek(1)))
        {
            number += Peek(1);
            Next();
        }
        return number;
    }
    private SyntaxToken CompleteString()
    {
        Next();
        string word = "";
        while(Peek(1) != '"')
        {
            word += Current;
            Next();
            if(Peek(1) == '\0') throw new SyntacticError("Se esperaba un \" ");
        }
        return new SyntaxToken(word, SyntaxKind.StringToken, null);
    }
    private string NormalizeLine(string line)
    {
        int i = 0;
        while (line[line.Length - 1 - i] == ' ')
        {
            i++;
        }
        return line.Substring( 0, line.Length - i);
    }

    public SyntaxToken[] GetLine()
    {
        List<SyntaxToken> TokenLine = new List<SyntaxToken>(0);
        SyntaxToken token;
        do
        {
            token = GetToken();
            TokenLine.Add(token);
            Next();
        }
        while (token.Kind != SyntaxKind.EndOfLineToken);
        return TokenLine.ToArray();
    }
}