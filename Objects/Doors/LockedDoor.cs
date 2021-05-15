using Godot;
using System;
using System.ComponentModel;

public class LockedDoor : Door
{
    [Export]
    [Description("Number string with the containing password")]
    private string password;

    private Numpad numpad;

    public override void _Ready()
    {
        base._Ready();

        numpad = GetNode<Numpad>("CanvasLayer/Numpad");

        numpad.combination = new Godot.Collections.Array<int>(password.ToInt());
    }

    protected override void OnDoorInputEvent(Viewport viewport, InputEvent @event, int shapeIndex)
    {
        if (!Input.IsMouseButtonPressed((int)ButtonList.Left) || !_canClick) return;

        numpad.PopupCentered();
    }

    protected override void OnDoorBodyExited(PhysicsBody2D body)
    {
        base.OnDoorBodyExited(body);

        if (numpad.Visible)
        {
            numpad.Hide();
        }
    }

    private void OnNumpadCombinationCorrect()
    {
        Open();

        if (numpad.Visible)
        {
            numpad.Hide();
        }
    }
}

