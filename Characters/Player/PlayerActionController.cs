using Godot;
using System;

public class PlayerActionController
{

    private Player _player;

    public PlayerActionController(Player player)
    {
        this._player = player;
    }

    public void HandleVisionModeToggle(InputEvent @event)
    {
        if (@event.IsActionPressed(PlayerAction.SwitchVision.ToActString()))
            _player.GetTree().CallGroup("Interface", "CycleVisionMode");
    }

    public void HandlePlayerMovementActions()
    {
        var isSprinting = Input.IsActionPressed(PlayerAction.Sprint.ToActString());

        if (
            Input.IsActionPressed(PlayerAction.MoveUp.ToActString()) && Input.IsActionPressed(PlayerAction.MoveDown.ToActString())
            || (!Input.IsActionPressed(PlayerAction.MoveUp.ToActString()) && !Input.IsActionPressed(PlayerAction.MoveDown.ToActString()))
        )
        {
            _player.PlayerMovementController.Move(PlayerMovement.StopYAxis, isSprinting);
        }
        else if(Input.IsActionPressed(PlayerAction.MoveUp.ToActString()))
        {
            _player.PlayerMovementController.Move(PlayerMovement.MoveUp, isSprinting);
        }
        else if(Input.IsActionPressed(PlayerAction.MoveDown.ToActString()))
        {
            _player.PlayerMovementController.Move(PlayerMovement.MoveDown, isSprinting);
        }
        
        if (
            Input.IsActionPressed(PlayerAction.MoveLeft.ToActString()) && Input.IsActionPressed(PlayerAction.MoveRight.ToActString())
            || (!Input.IsActionPressed(PlayerAction.MoveLeft.ToActString()) && !Input.IsActionPressed(PlayerAction.MoveRight.ToActString()))
        )
        {
            _player.PlayerMovementController.Move(PlayerMovement.StopXAxis, isSprinting);
        }
        else if(Input.IsActionPressed(PlayerAction.MoveLeft.ToActString()))
        {
            _player.PlayerMovementController.Move(PlayerMovement.MoveLeft, isSprinting);
        }
        else if(Input.IsActionPressed(PlayerAction.MoveRight.ToActString()))
        {
            _player.PlayerMovementController.Move(PlayerMovement.MoveRight, isSprinting);
        }
    }
    
}
