using SDL2;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Threading;
using ThreeNet.LowLevel;

namespace ThreeNet.Test
{
	internal class TestApp : App
	{
		private readonly FpsCounter fps = new(20);

		public TestApp() : base("3D Engine")
		{
		}

		protected override void OnEvent(SDL.SDL_Event e)
		{
			base.OnEvent(e);
			switch (e.type)
			{
				case SDL.SDL_EventType.SDL_KEYDOWN:
					if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
					{
						Exit();
					}
					else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_SPACE)
					{
						Vector3 pos = Camera.MainCamera.Scene[0].Transform.Position;
						pos.Z += 0.5f;
						Camera.MainCamera.Scene[0].Transform.Position = pos;
					}
					break;
			}
		}

		protected override void OnExit()
		{
			
		}

		protected override void OnInitialize()
		{
			Camera.MainCamera.ComputeProjectMatrix(90, 0.1f, 20);
			Mesh ship = Mesh.LoadFromObj("ship.obj");
			ship.Transform.Position = new Vector3(0, 0, -2);
			Camera.MainCamera.Scene.Add(ship);
		}

		protected override void OnLoop()
		{
			fps.StartFrame();
			Vector3 rot = Camera.MainCamera.Scene[0].Transform.Rotation;
			rot.Y += 0.001f;
			Camera.MainCamera.Scene[0].Transform.Rotation = rot;
		}

		protected override void OnRender()
		{
			renderer.Fill(Color.Black);
			Camera.MainCamera.Render(renderer);
			renderer.Flip();
			fps.EndFrame();
			Console.WriteLine($"FPS: {fps.AverageFps}");
		}
	}
}
