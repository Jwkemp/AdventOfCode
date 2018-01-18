using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Day20
	{
		public static long Part1()
		{
			List<Particle> particles = new List<AdventOfCode.Day20.Particle>();
			string[] inputs = Properties.Resources.input_D20.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			foreach ( string s in inputs )
			{
				particles.Add(new Particle(s));
			}
			
			foreach (Particle p in particles)
			{
				p.Tick(1000);
			}

			return GetClosestParticle(particles);
		}

		public static int Part2()
		{
			List<Particle> particles = new List<AdventOfCode.Day20.Particle>();
			string[] inputs = Properties.Resources.input_D20.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in inputs)
			{
				particles.Add(new Particle(s));
			}

			for (int i = 0; i < 1000; ++i)
			{
				foreach (Particle p in particles)
				{
					p.Tick(1);					
				}
				CollisionCheck(ref particles);
				particles = CleanUpParticles(particles);
			}
			return particles.Count;
		}

		private static List<Particle> CleanUpParticles(List<Particle> particles)
		{
			return particles.FindAll(p => p.active == true).ToList();
		}

		private static long GetClosestParticle(List<Particle> particles)
		{
			long minDistance = long.MaxValue;
			long distance = 0;
			long minParticle = 0;
			for (int i = 0; i < particles.Count; ++i)
			{
				distance = particles[i].DistanceFromOrigin();
				if (distance < minDistance)
				{
					minDistance = distance;
					minParticle = i;
				}
			}
			return minParticle;
		}

		private static void CollisionCheck( ref List<Particle> particles)
		{
			foreach (Particle p in particles)
			{
				for ( int j = particles.IndexOf(p)+1; j < particles.Count; ++j )
				{
					if ( p.position == particles[j].position )
					{
						// collision
						p.active = false;
						particles[j].active = false;
						j = 9999;				
					}
				}				
			}
		}

		private class Particle
		{
			public Vector3 position { get; private set; }
			Vector3 velocity;
			Vector3 acceleration;
			int time;
			public bool active;

			public Particle(string input)
			{
				int index = 0;
				int rangeLower = 0;
				int rangeUpper = 0;
				string[] strVector;
				active = true;

				index = input.IndexOf("p=");
				rangeLower = input.IndexOf('<', index);
				rangeUpper = input.IndexOf('>', rangeLower);
				strVector = input.Substring(rangeLower + 1, rangeUpper - rangeLower - 1).Split(',');
				position = new Vector3(strVector[0], strVector[1], strVector[2]);

				index = input.IndexOf("v=");
				rangeLower = input.IndexOf('<', index);
				rangeUpper = input.IndexOf('>', rangeLower);
				strVector = input.Substring(rangeLower + 1, rangeUpper - rangeLower - 1).Split(',');
				velocity = new Vector3(strVector[0], strVector[1], strVector[2]);

				index = input.IndexOf("a=");
				rangeLower = input.IndexOf('<', index);
				rangeUpper = input.IndexOf('>', rangeLower);
				strVector = input.Substring(rangeLower + 1, rangeUpper - rangeLower - 1).Split(',');
				acceleration = new Vector3(strVector[0], strVector[1], strVector[2]);
				
				time = 0;
			}

			public void Tick(int t)
			{
				time += t;
				velocity += (acceleration * t);
				position += (velocity * t);
			}

			public long DistanceFromOrigin()
			{				
				return Math.Abs(position.x) + Math.Abs(position.y) + Math.Abs(position.z);
			}

		}

		private class Vector3
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
}
