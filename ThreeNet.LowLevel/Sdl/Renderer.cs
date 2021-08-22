using SDL2;
using System;
using System.Drawing;
using IntPair = System.Tuple<int, int>;

namespace ThreeNet.LowLevel.Sdl
{
	public class Renderer
	{
		private readonly IntPtr renderer;
		
		public Renderer(IntPtr window)
		{
			renderer = SDL.SDL_CreateRenderer(window, -1, 0);
		}

		public void SetPixel(IntPair position, Color color)
		{
			SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
			SDL.SDL_RenderDrawPoint(renderer, position.Item1, position.Item2);
		}

		public void DrawLine(IntPair from, IntPair to, Color color)
		{
			SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
			SDL.SDL_RenderDrawLine(renderer, from.Item1, from.Item2, to.Item1, to.Item2);
		}

		public void Fill(Color color)
		{
			SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
			SDL.SDL_RenderClear(renderer);
		}

		public void Flip()
		{
			SDL.SDL_RenderPresent(renderer);
		}

		~Renderer()
		{
			SDL.SDL_DestroyRenderer(renderer);
		}

	}
}
