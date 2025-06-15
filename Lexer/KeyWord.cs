using System.Collections.Generic;

class Index
{
    static readonly Dictionary<string,SyntaxKind> keyWords = new Dictionary<string, SyntaxKind> 
    {
        {"Spawn", SyntaxKind.SpawnToken},
        {"Color", SyntaxKind.ColorToken},
        {"Size", SyntaxKind.SizeToken},
        {"DrawLine", SyntaxKind.DrawLineToken},
        {"DrawCircle", SyntaxKind.DrawCircleToken},
        {"DrawRectangle", SyntaxKind.DrawRectangleToken},
        {"Fill", SyntaxKind.FillToken},
        {"GetActualX", SyntaxKind.GetActualXToken},
        {"GetActualY", SyntaxKind.GetActualYToken},
        {"GetCanvasSize", SyntaxKind.GetCanvasSizeToken},
        {"GetColorCount", SyntaxKind.GetColorCountToken},
        {"IsBrushColor", SyntaxKind.IsBrushColorToken},
        {"IsBrushSize", SyntaxKind.IsBrushSizeToken},
        {"IsCanvasColor", SyntaxKind.IsCanvasColorToken},
        {"GoTo", SyntaxKind.GoToToken}
    };

    public static bool IsKeyWord(string word)
    {   
        return keyWords.ContainsKey("word");
    }

    public static SyntaxKind GetKind(string word)
    {
        return keyWords[word];
    }
}