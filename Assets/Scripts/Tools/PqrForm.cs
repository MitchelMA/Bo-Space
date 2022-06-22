using System;
using UnityEngine;
/// <summary>
/// Structure for the linear formula `px + qy = r`
/// </summary>
public struct PqrForm
{
    /// <summary>The p constant in the linear formula `px + qy = r`</summary>
    public float P;
    /// <summary>the q constant in the linear formula `px + qy = r`</summary>
    public float Q;
    /// <summary>the r constant in the linear formula `px + qy = r`</summary>
    public float R;
    
    /// <summary>
    /// Method to get the point (x, 0) where x is an arbitrary number and 0 the y-value.<br/>
    /// This method can also return `null`. When null is returned, the line doesn't intersect with the x-axis.<br/>
    /// This method can also return `+Infinity` meaning that the line has no slope, but also has a `R` value of 0 
    /// </summary>
    public Vector2? Root
    {
        get
        {
            if (float.IsInfinity(GetX(0)))
            {
                if (R == 0)
                {
                    return new Vector2(float.PositiveInfinity, 0);
                }

                return null;
            }

            return new Vector2(GetX(0), 0);
        }
    }

    /// <summary>
    /// Method to get the point (0, y) where y is an arbitrary number and 0 is the x-value.<br/>
    /// This method can also return `null`. When null is returned, the line doesn't intersect with the y-axis.<br/>
    /// This method can also return `+Infinity` meaning that the line is equal to `x = 0`.
    /// </summary>
    public Vector2? YIcept
    {
        get
        {
            if (float.IsInfinity(GetY(0)))
            {
                if (R == 0)
                {
                    return new Vector2(0, float.PositiveInfinity);
                }

                return null;
            }

            return new Vector2(0, GetY(0));
        }
    }
    /// <summary>
    /// Method to get the slope of a formula
    /// </summary>
    /// <value>The slope of the formula</value>
    public float Slope
    {
        get
        {
            if (Q == 0)
            {
                return float.PositiveInfinity;
            }

            return -P / Q;
        }
    }
    
    /// <summary>
    /// Angle in radians of the slope
    /// </summary>
    public float Angle => Mathf.Atan2(Slope, 1);

    /// <summary>
    /// Constructor to create a line between point a to point b
    /// </summary>
    /// <param name="point1">Point a</param>
    /// <param name="point2">Point b</param>
    public PqrForm(Vector2 point1, Vector2 point2)
    {
        float deltaY = point2.y - point1.y;
        float deltaX = point2.x - point1.x;
        float rc = deltaY / deltaX;

        if (float.IsNaN(rc))
        {
            rc = 0;
        }

        if (!float.IsInfinity(rc))
        {
            float r = point1.y - rc * point1.x;
            P = -rc;
            Q = 1;
            R = r;
            return;
        }

        P = 1;
        Q = 0;
        R = point1.x;
    }

    /// <summary>
    /// Constructor to create a `px + pq = r` formula from a `y = ax + b` formula
    /// </summary>
    /// <param name="a">The a variable in the `y = ax + b` formula</param>
    /// <param name="b">The b variable in the `y = ax + b` formula</param>
    public PqrForm(float a, float b)
    {
        P = -a;
        Q = 1;
        R = b;
        // fallback for when the slope is infinite
        // basically an `px = r` formula
        if (float.IsInfinity(a))
        {
            P = 1;
            Q = 0;
            R = b;
        }
    }

    /// <summary>
    /// Constructor to create a formula from a slope and an arbitrary point through which the formula should pass
    /// </summary>
    /// <param name="slope">The slope of the formula</param>
    /// <param name="point">The point that should be on the formula</param>
    public PqrForm(float slope, Vector2 point)
    {
        float b = point.y - point.x * slope;
        P = -slope;
        Q = 1;
        R = b;
        // fallback for when the slope is infinite
        // basically an `px = r` formula
        if (float.IsInfinity(slope))
        {
            P = 1;
            Q = 0;
            R = point.x;
        }
    }

    /// <summary>
    /// Constructor to copy a formula
    /// </summary>
    /// <param name="copy">The formula that gets copied</param>
    public PqrForm(PqrForm copy)
    {
        P = copy.P;
        Q = copy.Q;
        R = copy.R;
    }
    
    /// <summary>
    /// Method to get the X value of a formula at the given Y-position
    /// </summary>
    /// <param name="y">Y-position you want to know the X-value of</param>
    /// <returns>
    /// The X-position.<br/>
    /// When the line has a slope of 0, this method will return +Infinity
    /// </returns>
    public float GetX(float y)
    {
        if (P != 0)
        {
            return (R - Q * y) / P;
        }

        return float.PositiveInfinity;
    }
    
    /// <summary>
    /// Method to get the Y value of a formula at the given X-position
    /// </summary>
    /// <param name="x">X-position you want to know the Y-value of</param>
    /// <returns>
    /// The Y-position.<br/>
    /// When the slope of the formula is `Infinity`, this method will return +Infinity
    /// </returns>
    public float GetY(float x)
    {
        if (Q != 0)
        {
            return (R - P * x) / Q;
        }

        return float.PositiveInfinity;
    }
    
    /// <summary>
    /// Method to get the formula that will intersect this formula at a right-angle
    /// </summary>
    /// <returns>A formula that intersects with a right-angle</returns>
    public PqrForm Perpendicular()
    {
        return new PqrForm
        {
            P = Q,
            Q = -P,
            R = R,
        };
    }

    /// <summary>
    /// Method to get a slope from a given angle
    /// </summary>
    /// <param name="angle">The angle in radians</param>
    /// <returns>The slope that gets calculated with the given angle</returns>
    public static float SlopeFromAngle(float angle)
    {
        if(Mathf.Abs(Mathf.Abs(angle) - Mathf.PI/2) < 0.0001f)
        {
            return float.PositiveInfinity;
        }
        return Mathf.Tan(angle);
    }

    /// <summary>
    /// Method to get an angle from a given slope
    /// </summary>
    /// <param name="slope">The given slope</param>
    /// <returns>The angle in radians calculated with the given slope</returns>
    public static float AngleFromSlope(float slope)
    {
        return Mathf.Atan2(slope, 1);
    }

    /// <summary>
    /// Method to get the intersection-point of two formula's
    /// </summary>
    /// <param name="form1">Formula 1</param>
    /// <param name="form2">Formula 2</param>
    /// <returns>
    /// The point at which the two formula's intersect
    /// </returns>
    public static Vector2? Intersect(PqrForm form1, PqrForm form2, double tolerance)
    {
        // if clauses to test if the lines actually intersect;
        float slopeDiff = Mathf.Abs(form1.Slope - form2.Slope);
        if (float.IsInfinity(form1.Slope) && float.IsInfinity(form2.Slope))
        {
            slopeDiff = 0;
        }
        if(slopeDiff <= tolerance)
        {
            if (Math.Abs(form1.R / form1.Q - form2.R / form2.Q) < tolerance)
            {
                return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            }

            return null;
        }


        // the pqr variables of the first formula
        float p = form1.P;
        float q = form1.Q;
        float r = form1.R;

        // pqr variables of the second formula
        float a = form2.P;
        float b = form2.Q;
        float c = form2.R;
        float x = 0, y = 0;

        if (a != 0 && q != 0 && b == 0)
        {
            x = c / a;
            y = r / q - (p * c) / (q * a);
            return new Vector2(x, y);
        }
        if (b != 0 && p != 0 && a == 0)
        {
            y = c / b;
            x = r / p - (q * c) / (p * b);
            return new Vector2(x, y);
        }
        if ((q != 0 && a != 0 && p == 0) || (p != 0 && b != 0 && q == 0))
        {
            // in this case, it is best to switch the formula's
            return Intersect(form2, form1, tolerance);
        }

        // get the x value
        float xFactor = q / b;
        x = (r - xFactor * c) / (p - xFactor * a);

        // get te y value
        float yFactor = p / a;
        y = (r - yFactor * c) / (q - yFactor * b);

        return new Vector2(x, y);
    }

    /// <summary>
    /// Method to get the string that represents this formula
    /// </summary>
    /// <returns>The string that represents this formula</returns>
    public override string ToString()
    {
        return $"{P}x + {Q}y = {R}";
    }
}