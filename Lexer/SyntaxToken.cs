using Godot.Collections;

public class SyntaxToken
{
    public string Text;
    public SyntaxKind Kind;
    public object Value;

    public SyntaxToken(string text, SyntaxKind kind, object value)
    {
        Text = text;
        Kind = kind;
        Value = value;

    }

    public bool IsInstruction()
    {
        if(Kind == SyntaxKind.SpawnToken || Kind == SyntaxKind.ColorToken || Kind == SyntaxKind.SizeToken
        || Kind == SyntaxKind.DrawLineToken || Kind == SyntaxKind.DrawCircleToken || Kind == SyntaxKind.DrawRectangleToken) 
        return true;
        else return false;
    }
    public bool IsNumericOperation()
    {
        if(Kind == SyntaxKind.PlusToken || Kind == SyntaxKind.MinusToken || Kind == SyntaxKind.StarToken
        || Kind == SyntaxKind.DoubleStarToken || Kind == SyntaxKind.EqualsToken || Kind == SyntaxKind.NotEqualsToken
        || Kind == SyntaxKind.LessToken || Kind == SyntaxKind.LessToken || Kind == SyntaxKind.PorcentualToken ) 
        return true;
        else return false;
    }
    public bool IsBooleanOperation()
    {
        if(Kind == SyntaxKind.AndToken || Kind == SyntaxKind.OrToken) return true; 
        else return false;
    }
    public bool IsFunction()
    {
        if(Kind == SyntaxKind.GetActualXToken || Kind == SyntaxKind.GetActualYToken || Kind == SyntaxKind.GetCanvasSizeToken
        || Kind == SyntaxKind.IsBrushColorToken || Kind == SyntaxKind.IsBrushSizeToken || Kind == SyntaxKind.IsCanvasColorToken 
        || Kind == SyntaxKind.GetColorCountToken)
        return true; 
        else return false;
    }
}

 public enum SyntaxKind
 {
    NumberToken,
    SpawnToken,
    ColorToken,
    SizeToken,
    DrawLineToken,
    DrawCircleToken,
    DrawRectangleToken,
    FillToken,
    GetActualXToken,
    GetActualYToken,
    GetCanvasSizeToken,
    GetColorCountToken,
    IsBrushColorToken,
    IsBrushSizeToken,
    IsCanvasColorToken,
    GoToToken,
    TagToken,
    VariableToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    DoubleStarToken,
    PorcentualToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    ArrowToken,
    LessEqualsToken,
    LessToken,
    EqualsToken,
    AndToken,
    OrToken,
    GreatEqualsToken,
    GreatToken,
    BadToken,
    ComaToken,
    NewLineToken,
    EndOfFileToken,
    StringToken,
    NotEqualsToken,
    EndOfLineToken,
    OpenCorcheteToken,
    CloseCorcheteToken,
    CapturedTagToken
}