using Godot;
using System;

public class Autoproductions : Node
{
	[Signal]
	public delegate void CharFromBuildings(int char_count);
	private Timer refresh_timer;
	private double refresh_rate = 0.2; //Refresh the number 5 times every second
	private double char_increase_rate = 1;
	private double production_left_over = 0;
	public override void _Ready()
	{
		refresh_timer = GetNode<Timer>("RefreshTimer");
		refresh_timer.Connect("timeout", this, nameof(onRefreshTimeout));
		refresh_timer.WaitTime = (float) refresh_rate;
		refresh_timer.OneShot = false;
		refresh_timer.Start();
	}

	public void changeCharRate(double new_rate){
		char_increase_rate = new_rate;
	}

	public void onRefreshTimeout(){
		production_left_over += (char_increase_rate/get_threshold());
		int char_output = (int) production_left_over;
		EmitSignal(nameof(CharFromBuildings), char_output);
		production_left_over -= char_output;
		GD.Print("prod_left: " + production_left_over);
		GD.Print(char_output);
	}

	//Return 5 when its 0.2
	private int get_threshold(){
		return (int) (1/refresh_rate);
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventKey){
			InputEventKey key_event = (InputEventKey) @event;
			if (key_event.IsPressed() && key_event.Scancode == (int) KeyList.W){
				changeCharRate(char_increase_rate + 0.5);
			}
		}
	}

}
