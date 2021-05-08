using Godot;
using System;

public class Testing : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Let's start");
		Godot.File file = new Godot.File();
		file.Open("res://Testing/Testing.cs", File.ModeFlags.Read);
		var content = file.GetAsText();
		GD.Print(content);
		file.Close();

	}


}
