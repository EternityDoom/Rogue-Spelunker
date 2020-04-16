using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShape : Shape
{
    public override eType type => eType.SQUARE;



    public override float ComputeMass(float density)
    {
        return 4.0f * radius * radius * density;
    }
}
