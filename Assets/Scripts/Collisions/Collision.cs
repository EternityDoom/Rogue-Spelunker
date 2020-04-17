using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collision
{
    public static bool TestOverlap(Shape shapeA, Vector2 positionA, Shape shapeB, Vector2 positionB)
    {
        bool intersects = false;

        if (shapeA.type == Shape.eType.CIRCLE)
        {
            if (shapeB.type == Shape.eType.CIRCLE)
            {
                Circle circleA = new Circle(positionA, shapeA.radius);
                Circle circleB = new Circle(positionB, shapeB.radius);

                intersects = circleA.Contains(circleB);
            }
            else if (shapeB.type == Shape.eType.SQUARE)
            {
                Circle circleA = new Circle(positionA, shapeA.radius);
                Square squareB = new Square(positionB, shapeB.radius);

                intersects = circleA.Contains(squareB);
            }
        } else if (shapeA.type == Shape.eType.SQUARE)
        {
            if (shapeB.type == Shape.eType.CIRCLE)
            {
                Square squareA = new Square(positionA, shapeA.radius);
                Circle circleB = new Circle(positionB, shapeB.radius);

                intersects = squareA.Contains(circleB);
            }
            else if (shapeB.type == Shape.eType.SQUARE)
            {
                Square squareA = new Square(positionA, shapeA.radius);
                Square squareB = new Square(positionB, shapeB.radius);

                intersects = squareA.Contains(squareB);
            }
        }

        

        return intersects;
    }
}
