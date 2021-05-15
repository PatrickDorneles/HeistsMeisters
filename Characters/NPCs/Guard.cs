using Godot;
using System;
using System.Linq;
using Godot.Collections;
using Array = Godot.Collections.Array;

public class Guard : PlayerDetection
{
    private Navigation2D _navigation;
    private Node _destinations;
    private Timer _timer;

    private Vector2 _motion;
    private Array _possibleDestinations;
    private Vector2[] _path;

    [Export()]
    private float _minimumDistanceToDestination = 5f;

    [Export()]
    private float _walkSpeed = 0.5f;


    public override void _Ready()
    {
        base._Ready();
        _navigation = (Navigation2D)GetTree().Root.FindNode("Navigation2D", true, false);
        _destinations = _navigation.GetNode<Node>("Destinations");
        _timer = GetNode<Timer>("Timer");

        GD.Randomize();

        _possibleDestinations = _destinations.GetChildren();

        MakePath();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Navigate();
    }

    public void _OnTimerTimeout()
    {
        MakePath();
    }

    private void MakePath()
    {
        var index = GD.Randi() % _possibleDestinations.Count;
        var newDestination = (Position2D)_possibleDestinations[Convert.ToInt32(index)];

        _path = _navigation.GetSimplePath(Position, newDestination.Position, false);
    }

    private void Navigate()
    {
        var distanceToDestination = Position.DistanceTo(_path[0]);
        if (distanceToDestination > _minimumDistanceToDestination)
        {
            Move();
        }
        else
        {
            UpdatePath();
        }
    }

    private void Move()
    {
        LookAt(_path[0]);

        _motion = (_path[0] - Position).Normalized() * MaxSpeed * _walkSpeed;

        if (IsOnWall())
        {
            MakePath();
        }

        MoveAndSlide(_motion);
    }

    private void UpdatePath()
    {
        if (_path.Length == 1)
        {
            if (_timer.IsStopped())
                _timer.Start();
        }
        else
            _path = _path.Where((value, index) => index != 0).ToArray();
    }

}
