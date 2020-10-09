using Godot;
using System;

public enum PlayerAction
{
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    Sprint,
    SwitchVision,
}

public static class PlayerActionMethods
{
    public static string ToActString(this PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.MoveUp: return "player_move_up";
            case PlayerAction.MoveDown: return "player_move_down";
            case PlayerAction.MoveLeft: return "player_move_left";
            case PlayerAction.MoveRight: return "player_move_right";
            case PlayerAction.Sprint: return "player_sprint";
            case PlayerAction.SwitchVision: return "player_switch_vision";
            default: return string.Empty;
        }
    }
}