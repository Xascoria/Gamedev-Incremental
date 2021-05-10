using Godot;
using System;
using System.Collections.Generic;

public class Main : Control
{
	CodePanel code_panel;
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
		code_panel = GetNode<CodePanel>("CodePanel");
		work_in_progress = GetNode<Label>("CodeDetailsPanel/CurrentWork");
		line_count = GetNode<Label>("CodeDetailsPanel/LineCount");
		/*#endregion*/
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

}
