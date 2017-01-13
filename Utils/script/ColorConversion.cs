using UnityEngine;
using System.Collections;

public class ColorConversion {
	public static Vector3 rgb2xyz(Color RGB)
	{
		float r, g, b;
		r = RGB.r;
		g = RGB.g;
		b = RGB.b;

		if (r > 0.04045f){ r = Mathf.Pow((r + 0.055f) / 1.055f, 2.4f); }
		else { r = r / 12.92f; }
		if ( g > 0.04045f){ g = Mathf.Pow((g + 0.055f) / 1.055f, 2.4f); }
		else { g = g / 12.92f; }
		if (b > 0.04045f){ b = Mathf.Pow((b + 0.055f) / 1.055f, 2.4f); }
		else {
			b = b / 12.92f; }
		r = r * 100f;
		g = g * 100f;
		b = b * 100f;

		Vector3 xyz = new Vector3(0f,0f,0f);
		xyz.x = r * 0.4124f + g * 0.3576f + b * 0.1805f;
		xyz.y = r * 0.2126f + g * 0.7152f + b * 0.0722f;
		xyz.z = r * 0.0193f + g * 0.1192f + b * 0.9505f;

		return xyz;
	}

	public static Vector3 xyz2lab(Vector3 xyz)
	{
		const float REF_X = 95.047f; // Observer= 2°, Illuminant= D65
		const float REF_Y = 100.000f; 
		const float REF_Z = 108.883f; 
		float x = xyz.x / REF_X;   
		float y = xyz.y / REF_Y;  
		float z = xyz.z / REF_Z;  

		if ( x > 0.008856f ) { x = Mathf.Pow( x , 1f/3f ); }
		else { x = ( 7.787f * x ) + ( 16f/116f ); }
		if ( y > 0.008856f ) { y = Mathf.Pow( y , 1f/3f ); }
		else { y = ( 7.787f * y ) + ( 16f/116f ); }
		if ( z > 0.008856f ) { z = Mathf.Pow( z , 1f/3f ); }
		else { z = ( 7.787f * z ) + ( 16f/116f ); }

		Vector3 lab = new Vector3(0f, 0f, 0f);
		lab.x = ( 116f * y ) - 16f;
		lab.y = 500f * ( x - y );
		lab.z = 200f * ( y - z );

		return lab;
	}

	public static Vector3 lab2xyz( Vector3 lab )
	{
		const float REF_X = 95.047f; // Observer= 2°, Illuminant= D65
		const float REF_Y = 100.000f; 
		const float REF_Z = 108.883f; 
		float y = (lab.x + 16f) / 116f;
		float x = lab.y / 500f + y;
		float z = y - lab.z / 200f;

		if ( Mathf.Pow( y , 3f ) > 0.008856f ) { y = Mathf.Pow( y , 3f ); }
		else { y = ( y - 16f / 116f ) / 7.787f; }
		if ( Mathf.Pow( x , 3f ) > 0.008856f ) { x = Mathf.Pow( x , 3f ); }
		else { x = ( x - 16f / 116f ) / 7.787f; }
		if ( Mathf.Pow( z , 3f ) > 0.008856f ) { z = Mathf.Pow( z , 3f ); }
		else { z = ( z - 16f / 116f ) / 7.787f; }

		Vector3 xyz = new Vector3(0f, 0f, 0f);
		xyz.x = REF_X * x;     
		xyz.y = REF_Y * y; 
		xyz.z = REF_Z * z; 

		return xyz;
	}

	public static Color xyz2rgb(Vector3 XYZ)
	{
		//X from 0 to  95.047      (Observer = 2°, Illuminant = D65)
		//Y from 0 to 100.000
		//Z from 0 to 108.883
		float x = XYZ.x / 100f;        
		float y = XYZ.y / 100f;        
		float z = XYZ.z / 100f;        

		float r  = x * 3.2406f + y * -1.5372f + z * -0.4986f;
		float g  = x * -0.9689f + y * 1.8758f + z * 0.0415f;
		float b  = x * 0.0557f + y * -0.2040f + z * 1.0570f;

		if ( r > 0.0031308f ) {
            r = 1.055f * Mathf.Pow( r , ( 1f / 2.4f ) ) - 0.055f; }
		else { r = 12.92f * r; }
		if ( g > 0.0031308f ) {
            g = 1.055f * Mathf.Pow( g , ( 1f / 2.4f ) ) - 0.055f; }
		else { g = 12.92f * g; }
		if ( b > 0.0031308f ) {
            b = 1.055f * Mathf.Pow( b , ( 1f / 2.4f ) ) - 0.055f; }
		else { b = 12.92f * b; }
		return new Color(r,g,b);
	}

	public static Vector3 rgb2lab(Color C)
	{
		Vector3 XYZ = rgb2xyz (C);
		Vector3 lab = xyz2lab (XYZ);
		return lab;
	}
	public static Color lab2rgb(Vector3 lab)
	{
		Vector3 XYZ = lab2xyz (lab);
		Color C = xyz2rgb (XYZ);
		return C;
	}

    public static Vector3 rgb2hsv(Color rgb)
        // rgb [0,1] to hsv [0,1]
    {
        Vector3 hsv;
        float H, S, V;

        float Min = Mathf.Min(Mathf.Min(rgb.r, rgb.g),rgb.b);
        //Min. value of RGB
        float Max = Mathf.Max(Mathf.Max(rgb.r, rgb.g), rgb.b);
        //Max. value of RGB
        float del_Max = Max - Min; //Delta RGB value 

        V = Max;

        H = 0f;
        S = 0f;
        if (del_Max == 0) 
        //This is a gray, no chroma...
        {
            H = 0;  //HSV results from 0 to 1
            S = 0;
        }
        else 
        //Chromatic data...
        {
            S = del_Max / Max;

            float del_R = 
                (((Max - rgb.r) / 6f) + (del_Max / 2f)) / del_Max;
            float del_G = 
                (((Max - rgb.g) / 6f) + (del_Max / 2f)) / del_Max;
            float del_B = 
                (((Max - rgb.b) / 6f) + (del_Max / 2f)) / del_Max;

            if (rgb.r == Max) H = del_B - del_G;
            else if (rgb.g == Max) H = (1f / 3f) + del_R - del_B;
            else if (rgb.b == Max) H = (2f / 3f) + del_G - del_R;

            if (H < 0f) H += 1;
            if (H > 1f) H -= 1;
        }

        hsv = new Vector3(H, S, V);
        return hsv;
    }

    public static Color hsv2rgb(Vector3 hsv)
    // hsv [0,1] to rgb [0,1]
    {
        float H, S, V;
        H = hsv.x;
        S = hsv.y;
        V = hsv.z;

        float R, G, B;
        if (S == 0) //HSV from 0 to 1
        {
            R = V * 255f;
            G = V * 255f;
            B = V * 255f;
        }
        else
        {
            float h = H * 6f;
            if (h == 6f) h = 0f;      //H must be < 1
            int i = (int)h;           //Or ... i = floor( h )
            float v1 = V * (1 - S);
            float v2 = V * (1 - S * (h - (float)i));
            float v3 = V * (1 - S * (1 - (h - (float)i)));
        
            if (i == 0) {
                R = V; G = v3; B = v1; }
            else if (i == 1) {
                R = v2; G = V; B = v1; }
            else if (i == 2) {
                R = v1; G = V; B = v3; }
            else if (i == 3) {
                R = v1; G = v2; B = V; }
            else if (i == 4) {
                R = v3; G = v1; B = V; }
            else {
                R = V; G = v1; B = v2; }
        }

        Color rgb = new Color(R, G, B, 1.0f);
        return rgb;
    }

    public static Vector3 rgb2cmy(Color rgb)
    // rgb [0,1] to cmy [0,1]
    {
        Vector3 cmy = new Vector3(0f,0f,0f);
        cmy.x = 1.0f - rgb.r;
        cmy.y = 1.0f - rgb.g;
        cmy.z = 1.0f - rgb.b;
        return cmy;
    }
    public static Color cmy2rgb(Vector3 cmy)
    // cmy [0,1] to rgb [0,1]
    {
        Color rgb = new Color();
        rgb.r = 1.0f - cmy.x;
        rgb.g = 1.0f - cmy.y;
        rgb.b = 1.0f - cmy.z;
        return rgb;
    }

}
