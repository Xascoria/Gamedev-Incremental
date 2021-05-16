using Godot;
using System;

public class Autoproductions : Node
{
	[Signal]
	public delegate void CharFromBuildings(int char_count);
	private Timer refresh_timer;
	private double refresh_rate = 0.2; //Refresh the number 5 times every second
	private double char_increase_rate = 1;
	public override void _Ready()
	{
		refresh_timer = GetNode<Timer>("RefreshTimer");
		refresh_timer.Connect("timeout", this, nameof(onRefreshTimeout));
		changeCharRate(1);
		refresh_timer.OneShot = false;
		refresh_timer.Start();
	}

	public void changeCharRate(double new_rate){
		int threshold = get_threshold();
		if (char_increase_rate >= threshold && new_rate > threshold){
			//If both is over the threshold, then there's no point in changing the refresh rate of the timer
			//Since the emit char calculation is done in timeout
			char_increase_rate = new_rate;
			return;
		}

		if (new_rate >= get_threshold()){
			refresh_timer.WaitTime = (float) refresh_rate;
		}
		else 
		{
			refresh_timer.WaitTime = (float) (1/char_increase_rate);
		}
		char_increase_rate = new_rate;

	}

	public void onRefreshTimeout(){
		if (char_increase_rate != 0){
			if (char_increase_rate >= get_threshold()){
				
			}
			else
			{
				EmitSignal(nameof(CharFromBuildings), 1);
			}
		}
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
				changeCharRate(char_increase_rate + 0.2);
			}
		}
	}

}
