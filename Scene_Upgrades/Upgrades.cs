using Godot;
using System;

public class Upgrades : Panel
{
	PackedScene upgrade_tab = ResourceLoader.Load<PackedScene>("res://Scene_Upgrades/UpgradeTab.tscn");
	VBoxContainer container;

	public override void _Ready()
	{
		container = GetNode<VBoxContainer>("ScrollContainer/Content");
		// for (int i = 0; i < 30; i++)
		// 	container.AddChild(upgrade_tab.Instance());
		for (int i = 0; i < 15;i ++){
			AddUpgrade();
		}
	}

	//Place holder, implement upgrade details later
	public void AddUpgrade(){
		container.AddChild(upgrade_tab.Instance());
	}

}
