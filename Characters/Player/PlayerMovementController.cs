using Godot;
using System;

public class PlayerMovementController
{
    private Vector2 _motion;
    private int _speed;
    private int _maxSpeed;
    private float _friction;

    private const int SprintingSpeedMultiplier = 3;
    
    public PlayerMovementController(int speed, int maxSpeed, float friction)
    {
        this._speed = speed;
        this._maxSpeed = maxSpeed;
        this._friction = friction;
    }

    public Vector2 GetMotion()
    {
        return this._motion;
    }

    public void Move(PlayerMovement playerMovement, bool isSprinting)
    {
        var speedMultiplier = isSprinting ? SprintingSpeedMultiplier : 1;
        
        var baseSpeed = GetMovementBaseSpeed(playerMovement) * speedMultiplier;
        var maxMotionValue = GetMovementMaxMotionValue(playerMovement) * speedMultiplier;
        var minMotionValue = GetMovementMinMotionValue(playerMovement) * speedMultiplier;

        switch (playerMovement)
        {
            case PlayerMovement.MoveDown:
            case PlayerMovement.MoveUp:
                _motion.y = Mathf.Clamp(_motion.y + baseSpeed, minMotionValue, maxMotionValue);
                break;
            case PlayerMovement.MoveLeft:
            case PlayerMovement.MoveRight:
                _motion.x = Mathf.Clamp(_motion.x + baseSpeed, minMotionValue, maxMotionValue);
                break;
            case PlayerMovement.StopXAxis:
                _motion.x = Mathf.Lerp(_motion.x, 0, _friction);
                break;
            case PlayerMovement.StopYAxis:
                _motion.y = Mathf.Lerp(_motion.y, 0, _friction);
                break;
        }
        
    }

    private int GetMovementBaseSpeed(PlayerMovement playerMovement)
    {
        switch (playerMovement)
        {
            case PlayerMovement.MoveDown:
            case PlayerMovement.MoveRight:
                return this._speed;
            case PlayerMovement.MoveUp:
            case PlayerMovement.MoveLeft:
                return -this._speed;
            default:
                return 0;
        }
    }

    private int GetMovementMaxMotionValue(PlayerMovement playerMovement)
    {
        switch (playerMovement)
        {
            case PlayerMovement.MoveDown:
            case PlayerMovement.MoveRight:
                return this._maxSpeed;
            case PlayerMovement.MoveUp:
            case PlayerMovement.MoveLeft:
            default:
                return 0;
        }
    }
    
    private int GetMovementMinMotionValue(PlayerMovement playerMovement)
    {
        switch (playerMovement)
        {
            case PlayerMovement.MoveDown:
            case PlayerMovement.MoveRight:
            default:
                return 0;
            case PlayerMovement.MoveUp:
            case PlayerMovement.MoveLeft:
                return -this._maxSpeed;
        }
    }
}
