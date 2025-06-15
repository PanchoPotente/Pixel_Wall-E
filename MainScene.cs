using Godot;
using System;

public partial class MainScene : Node2D
{
	[Export] TileMapLayer tileMap;
	[Export] Camera2D camera;
	[Export] CodeEdit codeEdit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateCanvas();
		AdjustCamera();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void UpdateCanvas()
	{
		for (int i = 0; i < Canvas.WorkZone.GetLength(0); i++)
		{
			for (int j = 0; j < Canvas.WorkZone.GetLength(1); j++)
			{
				tileMap.SetCell(new Vector2I(i,j),1, new Vector2I((int)Canvas.WorkZone[i,j],0));
			}
		}
	}
	private void AdjustCamera()
	{
		Vector2 viewportSize = GetViewportRect().Size;
		float zoomFactor = viewportSize.Y / Canvas.WorkZone.GetLength(0);
		camera.Zoom = new Vector2(zoomFactor,zoomFactor);
	}
	public void ExecuteButton()
	{
		Reset();
		string code = codeEdit.Text;
		Lexer lexer = new Lexer(code);
		SyntaxToken[][] syntaxTokens = new SyntaxToken[0][];
		syntaxTokens = lexer.GetAllLines();
		GD.Print(syntaxTokens.Length);

		
		ExpressionParser parser = new ExpressionParser(syntaxTokens);
		
		{
			for (int i = 0; i < syntaxTokens.Length; i++)
			{
				parser.ParseLine();
			}
		}
		UpdateCanvas();
		PrintTokens(syntaxTokens);

	}
	private void Reset()
	{
		Canvas.Reset();
		TagIndex.Reset();
		Variable.Reset();
	}
	public void PrintTokens(SyntaxToken[][] syntaxTokens)
	{
		for (int i = 0; i < syntaxTokens.Length; i++)
		{
			string line = "";
			for (int j = 0; j < syntaxTokens[i].Length; j++)
			{
				line += $" {syntaxTokens[i][j].Text} {syntaxTokens[i][j].Kind} ";
			}
			GD.Print(line);
		}
	}
	public void PrintCanvas()
	{

		for (int i = 0; i < Canvas.WorkZone.GetLength(0); i++)
		{
			string line = ""; 
			for (int j = 0; j < Canvas.WorkZone.GetLength(1); j++)
			{
				line += $" {Canvas.WorkZone[i,j]}  ";
			}
			GD.Print(line);
		}
	}
}
