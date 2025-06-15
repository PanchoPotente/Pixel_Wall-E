using Godot;
using System;

public partial class MainScene : Node2D
{
	[Export] TileMapLayer tileMap;
	[Export] Camera2D camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateCanvas();
		AdjustCamera();
		Canvas.ColorAsing("Red");
		Canvas.DrawRectangle(1,1,20,8,10);
		Canvas.ColorAsing("Black");
		Canvas.DrawLine(-1,0,12);
		UpdateCanvas();
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
}
