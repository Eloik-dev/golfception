// Inspiré de https://www.youtube.com/watch?v=p2-uQEG9-3Y
Shader "HoleShader/Hole"
{
	SubShader 
	{
		Tags {"Queue" = "Geometry+1"}

		ColorMask 0
		ZWrite On

		Pass {}
	}
}
