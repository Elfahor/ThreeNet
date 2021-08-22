using System;
using System.Drawing;
using System.Numerics;

namespace ThreeNet
{
	public abstract class Shader
	{
		public static Lazy<Shader> Default { get; } = new Lazy<Shader>(() => new DefaultShader());

		public abstract V2F Vertex(in Vector3 vert);
		public abstract Color Fragment(in V2F vert);

		public static Vector3 WorldToClipPos(Vector3 worldPos) => Vector3.Transform(worldPos, Camera.MainCamera.ProjectionMatrix);
	}
}
