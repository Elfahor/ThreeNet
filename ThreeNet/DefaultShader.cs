using System.Drawing;
using System.Numerics;

namespace ThreeNet
{
	public class DefaultShader : Shader
	{
		public override V2F Vertex(in Vector3 vert)
		{
			V2F v = new();
			v.position = WorldToClipPos(vert);
			return v;
		}

		public override Color Fragment(in V2F vert)
		{
			return Color.White;
		}

	}
}
