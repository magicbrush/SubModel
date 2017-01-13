using UnityEngine;
using System.Collections;

public class ColorUtils {
	public static float colorDifference (Color A, Color B)
	{
		Vector3 ALab = ColorConversion.rgb2lab (A);
		Vector3 BLab = ColorConversion.rgb2lab (B);
		float Diff = (ALab - BLab).magnitude;

		return Diff;
	}

}
