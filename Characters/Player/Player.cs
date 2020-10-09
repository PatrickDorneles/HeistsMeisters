using Godot;
using System;

public class Player : TemplateCharacter
{

    public PlayerMovementController PlayerMovementController;
    public PlayerActionController PlayerActionController;

    public override void _Ready()
    {
        base._Ready();
        
        PlayerActionController = new PlayerActionController(this);
        PlayerMovementController = new PlayerMovementController(Speed, MaxSpeed, Friction);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
        LookAt(GetGlobalMousePosition());

        PlayerActionController.HandlePlayerMovementActions();
        
        MoveAndSlide(PlayerMovementController.GetMotion());
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        
        PlayerActionController.HandleVisionModeToggle(@event);
    }
}
