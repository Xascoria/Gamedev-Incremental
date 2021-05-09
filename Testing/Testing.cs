using Godot;
using System;

public class Testing : Panel
{
	RichTextLabel label;
	String content = "";

	int something = 2;
	[Signal]
	public delegate void MySignal();

	public override void _Ready()
	{
		label = GetNode<RichTextLabel>("RichTextLabel");

		Godot.File file = new Godot.File();
		file.Open("res://Testing/Testing.cs", File.ModeFlags.Read);
		content = file.GetAsText();
		file.Close();

		label.Text = "Line1";
		label.GetVScroll().Connect("value_changed", this, "ScrollLineLock");
		//this.Connect(nameof(MySignal), this, "ScrollLineLock");

		Font font = (Font) label.GetFont("normal_font", nameof(RichTextLabel));
		int line_separation = (int) label.GetConstant("line_separation");
		label.RectSize = new Vector2(label.RectSize.x, (font.GetHeight()+line_separation)*8);

	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventKey){
			InputEventKey key_event = (InputEventKey) @event;
			if (key_event.IsPressed()){
				label.Text += "\nLine" + something;
				something += 1;
				EmitSignal(nameof(MySignal));
			}
		}
	}

	public void ScrollLineLock(float scroll_bar_pos){
		//label.Get("normal_font");
		Font font = (Font) label.GetFont("normal_font", nameof(RichTextLabel));
		int line_separation = (int) label.GetConstant("line_separation");
		float new_val = scroll_bar_pos - (scroll_bar_pos % (font.GetHeight() + line_separation));
		//GD.Print(font.GetHeight() + line_separation);
		float delta = 0.00001f;
		if (Math.Abs(label.GetVScroll().Value - new_val) > delta){
			label.GetVScroll().Value = (Double) new_val;
		}
		GD.Print("bruhbruh");
	} 

}
