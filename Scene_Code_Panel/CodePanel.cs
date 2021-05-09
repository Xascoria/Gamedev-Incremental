using Godot;
using System;

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
		// for (int i =0; i < max_lines_count; i++){
		// 	label.Text += "Line " + (i+1) + (i == max_lines_count - 1? "":"\n");
		// }

	}

	//This get the height of a single line
	private double GetLineHeight(){
		int line_separation = (int) label.GetConstant("line_separation");
		return label_font.GetHeight() + line_separation;
	}

	int test = 12;
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventKey){
			InputEventKey key_event = (InputEventKey) @event;
			if (key_event.IsPressed()){
				String bruh = "";
				for (int i = 0; i < test; i++){
					bruh += "m k";
				}
				label.Text += bruh + "\n";
				GD.Print(label.RectSize.x);
				GD.Print("string size: " + ((Font) label.GetFont("normal_font", nameof(RichTextLabel))).GetStringSize(bruh));
				test += 1;
				GD.Print(label.GetLineCount());
			}
		}
	}


}
