using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using ThreeNet.LowLevel.Sdl;
using IntPair = System.Tuple<int, int>;

namespace ThreeNet
{
	public class Camera
	{
		private static readonly Lazy<Camera> lazy = new(() => new Camera());

		public static Camera MainCamera => lazy.Value;

		public Matrix4x4 ProjectionMatrix { get; set; }

		public List<Mesh> Scene { get; } = new();

		public void Render(Renderer renderer)
		{
			foreach (Mesh mesh in Scene)
			{
				RenderMesh(mesh, renderer);
			}
		}

		public void ComputeProjectMatrix(float fovDeg, float near, float far)
		{
			float S = 1 / MathF.Tan(fovDeg/2 * (MathF.PI / 180));
			ProjectionMatrix = new Matrix4x4(
				S, 0, 0, 0,
				0, S, 0, 0,
				0, 0, -(far / (far - near)), -1,
				0, 0, -((far * near) / (far - near)), 0
				);
		}

		private static void RenderMesh(Mesh mesh, Renderer renderer)
		{
			Matrix4x4 M2W = mesh.Transform.GetTransformMatrix();
			IntPair[] screenVerts = new IntPair[mesh.vertices.Length];
			for (int i = 0; i < mesh.vertices.Length; i++)
			{
				Vector3 localVert = mesh.vertices[i];
				Vector3 worldVert = Vector3.Transform(localVert, M2W);
				V2F v = mesh.Material!.Vertex(in worldVert);
				IntPair screenPos = Clip2Screen(v.position, 800, 600);
				//renderer.SetPixel(screenPos, Color.White);
				screenVerts[i] = screenPos;

				if (i == 0)
				{
					//Console.WriteLine($"screen pos: {x} {y}, transform.rot: {mesh.Transform.Rotation}");
				}
			}
			for (int i = 0; i < mesh.triangles.Length; i+=3)
			{
				int v1 = mesh.triangles[i];
				int v2 = mesh.triangles[i + 1];
				int v3 = mesh.triangles[i + 2];
				IntPair vert1 = screenVerts[v1 - 1];
				IntPair vert2 = screenVerts[v2 - 1];
				IntPair vert3 = screenVerts[v3 - 1];
				renderer.DrawLine(vert1, vert2, Color.White);
				renderer.DrawLine(vert1, vert3, Color.White);
				renderer.DrawLine(vert2, vert3, Color.White);
			}
		}

		public static IntPair Clip2Screen(Vector3 clip, int width, int height)
		{
			float x = (clip.X + 1) * (width * 0.5f);
			float y = (clip.Y + 1) * (height * 0.5f);
			return new((int)x, (int)y);
		}
	}
}
