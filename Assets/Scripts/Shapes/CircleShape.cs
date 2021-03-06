﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : Shape
{
    public override eType type => eType.CIRCLE;



    public override float ComputeMass(float density)
    {
        return Mathf.PI * radius * radius * density;
    }
}
