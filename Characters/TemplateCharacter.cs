using Godot;
using System;

public class TemplateCharacter : KinematicBody2D
{
    [Export()]
    public readonly int Speed = 20;
    
    [Export()]
    public readonly int MaxSpeed = 200;
    
    [Export()]
    public readonly float Friction = 0.25f;
}
