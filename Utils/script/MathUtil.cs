using UnityEngine;
using System.Collections;

public class MathUtil  {
    // Point in Triangle
    public static float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}

    static double sign(Point p1, Point p2, Point p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    static bool PointInTriangle(Point pt, Point v1, Point v2, Point v3)
    {
        bool b1, b2, b3;

        b1 = sign(pt, v1, v2) < 0.0f;
        b2 = sign(pt, v2, v3) < 0.0f;
        b3 = sign(pt, v3, v1) < 0.0f;

        return ((b1 == b2) && (b2 == b3));
    }


    public static float CatmullRom(float p0, float p1, float p2,float p3, float t)
    {
        float qt;
        qt = 0.5f * ((2f * p1) +
                     (-p0 + p2) * t +
                     (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
                     (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
        return qt;
    }

    public static Vector3 CatmullRom(
        Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {        
        float[] q = new float[3] { 0f, 0f, 0f };
        for(int i=0;i<3;i++)
        {            
           q[i] = CatmullRom(p0[i], p1[i], p2[i], p3[i],t);
        }
        Vector3 qt;
        qt.x = q[0];
        qt.y = q[1];
        qt.z = q[2];        
        return qt;
    }
}
