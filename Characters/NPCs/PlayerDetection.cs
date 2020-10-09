using Godot;
using System;
using Array = Godot.Collections.Array;

public class PlayerDetection : TemplateCharacter
{
    private const float FovTolerance = 17.5f;
    private const int MaxDetectionRange = 600;
    
    private readonly Color _red = new Color(1,0.25f,0.25f);
    private readonly Color _white = new Color(1,1,1);

    private Light2D _torch;
    private Player _player;

    public override void _Ready()
    {
        base._Ready();

        _torch = GetNode<Light2D>("Torch");
        _player = (Player) GetNode("/root").FindNode("Player", true, false);
        
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (PlayerInFov() && PlayerInLos())
        {
            _torch.Color = _red;
        }
        else
        {
            _torch.Color = _white;
        }
        
    }

    private bool PlayerInFov()
    {
        var npcFacingDirection = new Vector2(1, 0).Rotated(GlobalRotation);
        var directionToPlayer = (_player.Position - GlobalPosition).Normalized();

        return Mathf.Abs(directionToPlayer.AngleTo(npcFacingDirection)) < Mathf.Deg2Rad(FovTolerance);
    }

    private bool PlayerInLos()
    {
        var space = GetWorld2d().DirectSpaceState;
        var distanceToPlayer = _player.GlobalPosition.DistanceTo(GlobalPosition);

        if (distanceToPlayer > MaxDetectionRange)
            return false;

        var losObstacle = space.IntersectRay(
            GlobalPosition,
            _player.GlobalPosition,
            new Array { this },
            CollisionMask
        );

        if (losObstacle == null)
            return false;
        
        return losObstacle[key: "collider"] == _player;
    }
}
