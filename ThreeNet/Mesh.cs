using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace ThreeNet
{
	public class Mesh
	{
		public Shader? Material { get => material ?? Shader.Default.Value; set => material = value; }

		public Transform Transform { get; set; }

		public Vector3[] vertices = Array.Empty<Vector3>();
		public int[] triangles = Array.Empty<int>();
		private Shader? material;

		public static Mesh LoadFromObj(string path)
		{
			Mesh result = new();
			List<Vector3> verts = new();
			List<int> tris = new();
			using (StreamReader sr = new(path))
			{
				string? l;
				while ((l = sr.ReadLine()) != null)
				{
					string[] line = l.Split(' ');
					switch (line[0])
					{
						case "v":
							verts.Add(new Vector3(
								float.Parse(line[1], CultureInfo.InvariantCulture),
								float.Parse(line[2], CultureInfo.InvariantCulture),
								float.Parse(line[3], CultureInfo.InvariantCulture)));
							break;
						case "f":
							tris.Add(int.Parse(line[1]));
							tris.Add(int.Parse(line[2]));
							tris.Add(int.Parse(line[3]));
							break;
					}
				}
			}
			result.vertices = verts.ToArray();
			result.triangles = tris.ToArray();
			result.Material = null;
			result.Transform = new();
			return result;
		}

		private Mesh()
		{
			Transform = new();
		}
	}
}
