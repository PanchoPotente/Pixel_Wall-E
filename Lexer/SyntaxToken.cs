public class SyntaxToken
{
    public string Text;
    public TokenType Type;
    public SyntaxKind Kind;
    public object Value;

    public SyntaxToken(string text, SyntaxKind kind, object value)
    {
        Text = text;
        Kind = kind;
        Value = value;

    }
}
 public enum TokenType
 {
    NumericOperation,
    BooleanOperation
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
    EndOfLineToken
}