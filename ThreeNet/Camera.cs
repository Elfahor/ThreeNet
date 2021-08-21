using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThreeNet
{
	public class Camera
	{
		private static readonly Lazy<Camera> lazy = new(() => new Camera());

		public static Camera MainCamera => lazy.Value;

		public Matrix4x4 ProjectionMatrix { get; set; }

		private List<Mesh> scene = new();

		public void Render()
		{
			foreach (Mesh mesh in scene)
			{
				RenderMesh(mesh);
			}
		}

		private void RenderMesh(Mesh mesh)
		{
			throw new NotImplementedException();
		}
	}
}
