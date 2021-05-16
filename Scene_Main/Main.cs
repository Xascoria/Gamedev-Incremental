using Godot;
using System;
using System.Collections.Generic;

public class Main : Control
{
	CodePanel code_panel;
	Upgrades upgrade_panel;
	Autoproductions auto_prod;
	Label work_in_progress;
	Label line_count;
	List<String> paths_to_scripts = new List<string>();
	Godot.File file = new Godot.File();

	int file_index = 0;
	int line_index = 0;
	int line_count_num = 0;
	string cur_script_str = null;
	ScriptProcessor string_processor = new ScriptProcessor();
	public override void _Ready()
	{
		// On ready
		/*#region*/
		code_panel = GetNode<CodePanel>("MainPanel/CodePanel");
		upgrade_panel = GetNode<Upgrades>("MainPanel/UpgradePanel");
		auto_prod = GetNode<Autoproductions>("AutoProd");

		work_in_progress = GetNode<Label>("CodeDetailsPanel/CurrentWork");
		line_count = GetNode<Label>("CodeDetailsPanel/LineCount");
		/*#endregion*/

		//TODO: panel selections
		upgrade_panel.Visible = false;
		auto_prod.Connect(nameof(Autoproductions.CharFromBuildings), this, nameof(OnAutoProduct));
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventKey){
			InputEventKey key_event = (InputEventKey) @event;
			if (key_event.IsPressed()){
				_OnAnyKeyPressed();
			}
		}
	}

	private void _OnAnyKeyPressed(){
		String new_string = string_processor.GetScriptText(5);
		code_panel.AddText(new_string);
		line_count_num += GetNLCountInString(new_string);
		line_count.Text = "LINE COUNT: " + line_count_num;
	}

	private int GetNLCountInString(String input){
		int output = 0;
		for (int i = 0; i < input.Length;i++){
			if (input[i] == '\n'){
				output += 1;
			}
		}
		return output;
	}

	private void OnAutoProduct(int chars_count){
		GD.Print(chars_count);
	}

}
