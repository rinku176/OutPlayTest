using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOfBall : MonoBehaviour
{
    bool TryCalculateXPositionAtHeight(
    float h, //height of ball
    Vector2 p, // initial position
    Vector2 v, // initial velocity 
    float G, // gravitational value
    float w, // width of the space
    ref float xPosition)
    {
        if (G <= 0 || w <= 0)
        {
            float a, b, c;

            //h= p + vt -0.5gt^2
            //0.5gt^2 - vt + (p-h) = 0
            a = 0.5f * G;
            b = -v.y;
            c = p.y - h;

            //quadratic equation
            float quadEq = b * b - 4 * a * c;
            if (quadEq >= 0)
            {
                float sqrtEqn = Mathf.Sqrt(quadEq);

                float t1 = (-b + sqrtEqn) / (2 * a);
                float t2 = (-b - sqrtEqn) / (2 * a);

                float t = t1 > 0 ? t1 : t2;
                if (t < 0)
                {
                    return false;
                }

                xPosition = p.x + v.x * t;
                return (xPosition >= 0 && xPosition <= w);
            }
            else
                return false;

        }
        else
            return false;
    }

}
