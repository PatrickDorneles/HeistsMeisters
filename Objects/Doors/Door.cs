using Godot;
using System;

enum DoorAnimation
{
    Open,
    Close
}

public class Door : Area2D
{

    private AnimationPlayer _animationPlayer;

    protected bool _canClick;
    protected bool _open;

    public override void _Ready()
    {
        base._Ready();

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _canClick = false;
        _open = false;
    }

    protected virtual void OnDoorBodyEntered(PhysicsBody2D body)
    {
        if (body is Player)
        {
            _canClick = true;
        }
        else if (body.CollisionLayer == 4)
        {
            Open();
        }

    }

    protected virtual void OnDoorBodyExited(PhysicsBody2D body)
    {
        _canClick &= !(body is Player);

        Close();
    }

    protected virtual void OnDoorInputEvent(Viewport viewport, InputEvent @event, int shapeIndex)
    {
        if (!Input.IsMouseButtonPressed((int)ButtonList.Left) || !_canClick) return;

        if (_open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    protected void Open()
    {
        if (_open) return;
        _open = true;
        _animationPlayer.Play(DoorAnimation.Open.ToString());
    }

    protected void Close()
    {
        if (!_open) return;
        _open = false;
        _animationPlayer.Play(DoorAnimation.Close.ToString());
    }
}

