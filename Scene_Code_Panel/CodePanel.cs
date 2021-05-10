using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public class CodePanel : Panel
{
	//This class is going to need detail documentation, cause I ain't gonna think about this shit afterwards
	//THAT MEANS **YOU**
	RichTextLabel label;
	Font label_font;
	const int max_lines_count = 33;

	public override void _Ready()
	{
		label = GetNode<RichTextLabel>("CodeBlock");
		label_font = (Font) label.GetFont("normal_font", nameof(RichTextLabel));

		Setup();
	}

	private void Setup()
	{
		label.Text = "";
		//Readjust label size to fit all the lines on screen
		label.RectSize = new Vector2(label.RectSize.x, (float) GetLineHeight() * max_lines_count);
	}

	//This get the height of a single line
	private double GetLineHeight(){
		int line_separation = (int) label.GetConstant("line_separation");
		return label_font.GetHeight() + line_separation;
	}

	public void AddText(String new_text){
		label.Text += new_text;
		ReformatLines();
	}

	//Take the string stored at the richtextlabel, slice it according to visible lines and put it back in.
	private void ReformatLines()
	{
		//Doesn't seems to be working properly
		// if (label.GetLineCount() == label.GetVisibleLineCount()){
		// 	return;
		// }
		String last_char = " ";
		if (label.Text.Length > 0){
			last_char = label.Text.Substr(label.Text.Length-1, 1);
		}
		//Original lines in the text block
		String[] lines = label.Text.Split("\n");
		//Formatted lines
		List<String> formatted_lines = new List<string>();

		for (int i = 0; i < lines.Length; i++){
			if (label_font.GetStringSize(lines[i]).x > label.RectSize.x){
				StringBuilder str_builder = new StringBuilder(lines[i]);
				while (label_font.GetStringSize( str_builder.ToString() ).x > label.RectSize.x)
				{
					//Remove the first visible line
					int end = -1;
					for (int j = 0; j < str_builder.Length; j++){
						if (label_font.GetStringSize(str_builder.ToString(0, j)).x > label.RectSize.x){
							end = j -1;
							break;
						}
					}
					formatted_lines.Add(str_builder.ToString(0, end));
					str_builder.Remove(0, end);
				}
				//Add whatever that is remained of the lines
				formatted_lines.Add(str_builder.ToString());
			}
			else
			{
				formatted_lines.Add(lines[i]);
			}
		}

		label.Text = "";
		//Put the formatted visible lines it
		for (int i = Math.Max(0, formatted_lines.Count - max_lines_count + 1); i < formatted_lines.Count; i++){
			label.Text += formatted_lines[i];
			if (i != formatted_lines.Count - 1 || last_char.Equals("\n")){
				label.Text += '\n';
			}
		}

	}

	int test = 1;
	// public override void _Input(InputEvent @event)
	// {
	// 	base._Input(@event);

	// 	if (@event is InputEventKey){
	// 		InputEventKey key_event = (InputEventKey) @event;
	// 		if (key_event.IsPressed()){
	// 			String bruh = "";
	// 			for (int i = 0; i < test; i++){
	// 				bruh += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	// 			}
	// 			label.Text += bruh + "\n";
	// 			test += 2;
	// 			ReformatLines();
	// 			CallDeferred(nameof(tester2));
	// 		}
	// 	}
	// }

	// public void tester2(){
	// 	GD.Print(label.GetVisibleLineCount());
	// }


}
