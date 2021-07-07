using UnityEngine;

namespace OctanGames.Extensions
{
	public static class ColorExtensions
	{
		public static Color SetAlpha(this Color color, float alpha)
		{
			color.a = alpha;
			return color;
		}
	}
}
