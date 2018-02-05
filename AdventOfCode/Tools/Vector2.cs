using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tools
{
	class Vector2
	{
		public int x;
		public int y;

		public Vector2(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public static bool operator ==(Vector2 l, Vector2 r )
		{
			return l.x == r.x && l.y == r.y;
		}
		public static bool operator !=(Vector2 l, Vector2 r)
		{
			return l.x != r.x && l.y != r.y;
		}

		public override bool Equals(object obj)
		{
			Vector2 v = (Vector2)obj;
			if (x == v.x)
			{
				if ( y == v.y )
				{
					return true;
				}
			}
			return false;

			//return x == v.x && y == v.y;
		}

		public override int GetHashCode()
		{
			return 17 * x.GetHashCode() + y.GetHashCode();
		}
	}
}
