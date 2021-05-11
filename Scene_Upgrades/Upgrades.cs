using Godot;
using System;

public class Upgrades : Panel
{
	PackedScene upgrade_tab = ResourceLoader.Load<PackedScene>("res://Scene_Upgrades/UpgradeTab.tscn");
	VBoxContainer container;

	public override void _Ready()
	{
		container = GetNode<VBoxContainer>("ScrollContainer/Content");
		container.AddChild(upgrade_tab.Instance());
	}

}
