using System;
using Godot;
using System.Collections.Generic;
using System.Text;

public class ScriptProcessor
{
    private List<String> paths_to_scripts = new List<string>();
    private String cached_script_content = null;
    private int scripts_index = 0;
    private int string_index = 0;

    public ScriptProcessor()
    {
        GetAllScripts();
        Godot.File file = new File();
        file.Open(paths_to_scripts[scripts_index], File.ModeFlags.Read);
        cached_script_content = file.GetAsText();
		file.Close();
    }

    public String GetScriptText(int length){
        if (length + string_index > cached_script_content.Length){
            StringBuilder str_builder = new StringBuilder(cached_script_content.Substring(string_index));
            Godot.File file = new File();
            while (str_builder.Length < length){
                string_index = 0;
                scripts_index += 1;
                //Loop back to the first script
                if (scripts_index == paths_to_scripts.Count){
                    scripts_index = 0;
                }
                file.Open(paths_to_scripts[scripts_index], File.ModeFlags.Read);
                cached_script_content = file.GetAsText();
		        file.Close();
                if (cached_script_content.Length < length - str_builder.Length)
                {
                    str_builder.Append(cached_script_content);
                }
                else 
                {
                    int remaining_length = length - str_builder.Length;
                    str_builder.Append(cached_script_content.Substring(string_index, remaining_length));
                    string_index += remaining_length;
                }
            }
            return str_builder.ToString();
        }
        String output = cached_script_content.Substring(string_index, length);
        string_index += length;
        return output;
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