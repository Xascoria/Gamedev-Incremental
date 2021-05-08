using Godot;
using System;

public class CodePanel : Panel
{
	const int max_line = 33;
	RichTextLabel[] line_references = new RichTextLabel[33];
	int current_line_ptr = 0;

	public override void _Ready()
	{
		for (int i = 0; i < max_line; i++)
		{
			line_references[i] = GetNode<RichTextLabel>("Line" + (i+1));
			line_references[i].Text = "";
		}
	}

	public void AddLine(String new_input){
		line_references[current_line_ptr].Text = new_input;
	}

	//TODO: Gonna make this janky as workaround for godot's lack of visible lines methods
	//The pain
	private void CheckLineFit(int line_index, string removed_so_far){
		if (line_references[line_index].GetVisibleLineCount() == 1){
			if (removed_so_far.Length != 0){
				
			}
			return;
		}
		
	}

}
