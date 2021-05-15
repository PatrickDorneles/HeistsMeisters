using Godot;
using System;
using System.Linq;

public abstract class CombinationGenerator : Node
{

    public static int[] GenerateCombination(int length)
    {
        var combination = new int[length];


        for (int index = 0; index < length; index++)
        {
            var random = GD.Randi() % 10;
            combination[index] = Convert.ToInt32(random);
        }

        return combination;
    }
}
