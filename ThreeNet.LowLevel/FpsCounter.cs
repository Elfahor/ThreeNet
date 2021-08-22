using System.Diagnostics;

namespace ThreeNet.LowLevel
{
	public class FpsCounter
	{
		public float AverageFps => frames.Average;

		private readonly CircularQueue frames;
		private readonly Stopwatch timer;

		public FpsCounter(int smoothAmount)
		{
			frames = new CircularQueue(smoothAmount);
			timer = new();
		}

		public void StartFrame()
		{
			timer.Restart();
		}

		public void EndFrame()
		{
			timer.Stop();
			float fps = 1f / (timer.Elapsed.Ticks / 10_000_000f);
			frames.Insert(fps);
		}
	}
}
