using SDL2;
using System;
using ThreeNet.LowLevel;

namespace ThreeNet.Test
{
	internal class TestApp : App
	{
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
					break;
			}
		}

		protected override void OnExit()
		{
			
		}

		protected override void OnInitialize()
		{
			
		}

		protected override void OnLoop()
		{
			
		}

		protected override void OnRender()
		{
			SDL.SDL_RenderClear(renderer);
			SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 0);
			SDL.SDL_RenderDrawRect(renderer, IntPtr.Zero);
			SDL.SDL_RenderPresent(renderer);
		}
	}
}
