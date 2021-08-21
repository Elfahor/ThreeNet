using SDL2;
using System;

namespace ThreeNet.LowLevel
{
	public abstract class App
	{
		public string WindowTitle { get; init; }

		public IntPtr window;
		public IntPtr renderer;
		private bool running;

		public int Run()
		{
			int initResult = SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
			if (initResult >= 0)
			{
				int winCreationResult = SDL.SDL_CreateWindowAndRenderer(800,
														   600,
														   SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
														   out window,
														   out renderer);
				if (winCreationResult >= 0)
				{
					SDL.SDL_SetWindowTitle(window, WindowTitle);
					OnInitialize();

					running = true;
					while (running)
					{
						while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
						{
							OnEvent(e);
						}

						OnLoop();
						OnRender();
					}
					OnExit();


					return 0;
				}
				else
				{
					Console.WriteLine("Creating window {0}", winCreationResult);
					return winCreationResult;
				}
			}
			else
			{
				Console.WriteLine("Init returned {0}", initResult);
				return initResult;
			}
		}

		public void Exit()
		{
			running = false;
			SDL.SDL_Quit();
		}

		protected abstract void OnInitialize();
		protected virtual void OnEvent(SDL.SDL_Event e)
		{
			switch (e.type)
			{
				case SDL.SDL_EventType.SDL_QUIT:
					Exit();
					break;
			}
		}
		protected abstract void OnLoop();
		protected abstract void OnRender();
		protected abstract void OnExit();
	}
}
