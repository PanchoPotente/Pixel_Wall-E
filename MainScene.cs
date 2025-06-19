using Godot;
using System;
using System.IO;
using System.Text;

public partial class MainScene : Node2D
{
	[Export] TileMapLayer tileMap;
	[Export] Camera2D camera;
	[Export] CodeEdit codeEdit;
	[Export] LineEdit lineEdit;
	[Export] Label label;
	[Export] LineEdit SaveDirectory;
	[Export] LineEdit LoadDirectory;


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
		try
		{
			syntaxTokens = lexer.GetAllLines();
			PrintTokens(syntaxTokens);
		}
		catch(Exception error)
		{
			ReportError($"{error.GetType()} linea: {lexer.line} {error.Message}");
		}

		ExpressionParser parser = new ExpressionParser(syntaxTokens);
		try
		{
			parser.Parse();
		}
		catch(Exception error)
		{
			ReportError($"{error.GetType()} linea: {parser.line} {error.Message}");
		}
		UpdateCanvas();
	}
	private void Reset()
	{
		Canvas.Reset();
		TagIndex.Reset();
		Variable.Reset();
		label.Text = "";
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

	public void ResizeButton()
	{
		if(int.TryParse(lineEdit.Text, out int result))
		{
			Canvas.CanvasSize = result;
			Canvas.Reset();
			AdjustCamera();
			UpdateCanvas();
		}
	}

	private void ReportError(string error)
	{
		label.Text = error;
	}
	public void SaveButton()
	{
		File.Create(SaveDirectory.Text);
	}
	public void LoadButton()
	{
		string code = File.ReadAllText(LoadDirectory.Text);
		codeEdit.Text = code;
	}
}
