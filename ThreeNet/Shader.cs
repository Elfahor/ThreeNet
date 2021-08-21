using System;
using System.Drawing;
using System.Numerics;

namespace ThreeNet
{
	public abstract class Shader
	{
		Mesh? owner;

		protected abstract V2F Vertex();
		protected abstract Color Fragment(V2F vert);

		public Vector3 WorldToClipPos()
		{
			throw new NotImplementedException();
		}
	}
}
