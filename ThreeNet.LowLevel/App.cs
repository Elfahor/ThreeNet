using SDL2;
using System;
using ThreeNet.LowLevel.Sdl;

namespace ThreeNet.LowLevel
{
	public abstract class App
	{
		public string WindowTitle { get; init; }

		public IntPtr window;
		public Renderer renderer;
		private bool running;

		protected App(string windowTitle)
		{
			WindowTitle = windowTitle ?? throw new ArgumentNullException(nameof(windowTitle));
		}

		public int Run()
		{
			int initResult = SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
			if (initResult >= 0)
			{
				window = SDL.SDL_CreateWindow(WindowTitle, 100, 100, 800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
				renderer = new(window);
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
				Console.WriteLine("Init returned {0}", initResult);
				return initResult;
			}
		}

		public void Exit()
		{
			running = false;
			SDL.SDL_DestroyWindow(window);
			SDL.SDL_Quit();
		}

		protected abstract void OnInitialize();
		protected virtual void OnEvent(SDL.SDL_Event e)
		{
			if (e.type == SDL.SDL_EventType.SDL_QUIT)
			{
				Exit();
			}
		}
		protected abstract void OnLoop();
		protected abstract void OnRender();
		protected abstract void OnExit();
	}
}
