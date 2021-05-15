using Godot;
using System;

public class VisionMode : Node
{
    private Timer _cooldownTimer;
    private CanvasModulate _modulate;
    private AudioStreamPlayer _streamPlayer;

    private Color Dark = new Color("111111");
    private Color NightVision = new Color("37bf62");

    private bool _isNightVisionOn;

    public override void _Ready()
    {
        base._Ready();
        _cooldownTimer = GetNode<Timer>("CooldownTimer");
        _modulate = GetNode<CanvasModulate>("Modulate");
        _streamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

        _modulate.Color = Dark;
        _isNightVisionOn = false;
    }

    public void CycleVisionMode()
    {
        if (!_cooldownTimer.IsStopped())
            return;

        if (!_isNightVisionOn)
        {
            SetNightVisionOn();
        }
        else
        {
            SetNightVisionOff();
        }
    }

    private void SetNightVisionOff()
    {
        _modulate.Color = Dark;
        _streamPlayer.Stream = GD.Load<AudioStream>("res://SFX/nightvision_off.wav");
        _streamPlayer.Play();
        _cooldownTimer.Start();
        _isNightVisionOn = false;
    }

    private void SetNightVisionOn()
    {
        _modulate.Color = NightVision;
        _streamPlayer.Stream = GD.Load<AudioStream>("res://SFX/nightvision.wav");
        _streamPlayer.Play();
        _isNightVisionOn = true;
    }
}
