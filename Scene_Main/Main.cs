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

	int index = 0;
	int line_index = 0;
	int line_count_num = 0;
	const int code_block_visible_line = 31;
	string cur_script_str = null;
	public override void _Ready()
	{
		// On ready
		/*#region*/
		code_panel = GetNode<CodePanel>("CodePanel");
		work_in_progress = GetNode<Label>("CodeDetailsPanel/CurrentWork");
		line_count = GetNode<Label>("CodeDetailsPanel/LineCount");
		/*#endregion*/

		GetAllScripts();
		file.Open(paths_to_scripts[0], File.ModeFlags.Read);
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
		String new_code_line = file.GetLine();
		code_panel.AddLine(new_code_line.Substring(0,new_code_line.Length-1));
		line_count_num += 1;
		line_count.Text = "LINE COUNT: " + line_count_num;
	}

	// Getting all the .cs in the proj
	/*#region*/
	private void GetAllScripts(){
		List<String> unprocessed_dirs = new List<string>();
		unprocessed_dirs.Add("res:/");
		while (unprocessed_dirs.Count != 0){
			_HelperGetScripts(unprocessed_dirs[0], unprocessed_dirs);
			unprocessed_dirs.RemoveAt(0);
		}
	}
	
	private void _HelperGetScripts(String path, List<string> unprocessed_dirs){
		Godot.Directory dir = new Godot.Directory();
		dir.Open(path);
		dir.ListDirBegin();
		String filename = dir.GetNext();
		while (filename.Length != 0){
			if (dir.CurrentIsDir())
			{
				if (filename[0] != '.'){
					unprocessed_dirs.Add(path + "/" + filename);
				}
			}
			else
			{
				if (filename.Substring(filename.Length-3).Equals(".cs")){
					paths_to_scripts.Add(path + "/" + filename);
				}
			}
			filename = dir.GetNext();
		}
	}
	/*#endregion*/


}
