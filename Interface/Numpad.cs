using Godot;
using System;
using System.Linq;
using Godot.Collections;
using System.Threading.Tasks;

public class Numpad : Popup
{
    [Signal()]
    public delegate void CombinationCorrect();

    private const string OK_TEXT = "OK";
    private const string INCORRECT_MESSAGE = "INCORRECT";
    private const string CORRECT_MESSAGE = "CORRECT";

    public Array<int> combination = new Array<int>(new int[] { 1, 2, 3, 4 });
    private Array<int> guess = new Array<int>();

    private Label display;
    private TextureRect statusLight;
    private GridContainer buttonGrid;
    private AudioStreamPlayer audioPlayer;

    public override void _Ready()
    {
        base._Ready();

        display = GetNode<Label>("VBoxContainer/DisplayContainer/Display");
        statusLight = GetNode<TextureRect>("VBoxContainer/ButtonContainer/GridContainer/StatusLight");
        buttonGrid = GetNode<GridContainer>("VBoxContainer/ButtonContainer/GridContainer");
        audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

        ConnectButtons();
        ResetLock();
    }

    private void ConnectButtons()
    {
        foreach (Node child in buttonGrid.GetChildren())
        {
            if (child is Button button)
            {
                button.Connect("pressed", this, "ButtonPressed", new Godot.Collections.Array
                {
                    button.Text
                });
            }
        }
    }

    private void ButtonPressed(string button)
    {
        GD.Print("EnterValue");
        if (button == OK_TEXT)
        {
            CheckGuess();
        }
        else
        {
            EnterValue(button.ToInt());
        }
    }

    private void EnterValue(int value)
    {
        audioPlayer.Stream = GD.Load<AudioStream>("res://SFX/twoTone1.ogg");
        audioPlayer.Play();

        guess.Add(value);
        UpdateDisplay();
    }

    private void CheckGuess()
    {
        audioPlayer.Stream = GD.Load<AudioStream>("res://SFX/threeTone1.ogg");
        audioPlayer.Play();

        if (guess.SequenceEqual(combination))
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    private void UpdateDisplay()
    {
        display.Text = string.Join<int>("", guess);
    }

    private void ResetLock()
    {
        guess.Clear();
        UpdateDisplay();

        statusLight.Texture = GD.Load<Texture>("res://GFX/Interface/PNG/dotWhite.png");
    }

    private async void Correct()
    {
        statusLight.Texture = GD.Load<Texture>("res://GFX/Interface/PNG/dotGreen.png");
        display.Text = CORRECT_MESSAGE;
        await Task.Delay(500);
        EmitSignal(nameof(CombinationCorrect));
        ResetLock();
    }

    private async void Incorrect()
    {
        statusLight.Texture = GD.Load<Texture>("res://GFX/Interface/PNG/dotRed.png");
        display.Text = INCORRECT_MESSAGE;
        await Task.Delay(1000);
        ResetLock();
    }

}
