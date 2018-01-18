using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tools
{
	class Vector3
	{
		public long x { get; set; }
		public long y { get; set; }
		public long z { get; set; }

		public Vector3(long _x, long _y, long _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}
		public Vector3(string _x, string _y, string _z)
		{
			x = long.Parse(_x);
			y = long.Parse(_y);
			z = long.Parse(_z);
		}
		public Vector3()
		{
			x = 0;
			y = 0;
			z = 0;
		}

		public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
		{
			Vector3 result = new Vector3(0, 0, 0);
			result.x = lhs.x + rhs.x;
			result.y = lhs.y + rhs.y;
			result.z = lhs.z + rhs.z;

			return result;
		}

		public static string operator +(string lhs, Vector3 rhs)
		{
			return lhs + rhs.x + "," + rhs.y + "," + rhs.z;
		}

		public static Vector3 operator *(Vector3 lhs, int rhs)
		{
			Vector3 result = new Vector3(0, 0, 0);
			result.x = lhs.x * rhs;
			result.y = lhs.y * rhs;
			result.z = lhs.z * rhs;

			return result;
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z);
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z);
		}

	}
}
