using System;
using System.Linq;

namespace ThreeNet.LowLevel
{
	internal class CircularQueue
	{
		private readonly float[] ele;
		private int front;
		private int rear;
		private readonly int max;
		private int count;

		public CircularQueue(int size)
		{
			ele = new float[size];
			front = 0;
			rear = -1;
			max = size;
			count = 0;
		}

		public void Insert(float item)
		{
			if (count == max)
			{
				Delete();
				return;
			}
			else
			{
				rear = (rear + 1) % max;
				ele[rear] = item;

				count++;
			}
		}

		public void Delete()
		{
			if (count == 0)
			{
				Console.WriteLine("Queue is Empty");
			}
			else
			{
				front = (front + 1) % max;

				count--;
			}
		}

		public float Average => ele.Sum() / ele.Length;
	}
}
