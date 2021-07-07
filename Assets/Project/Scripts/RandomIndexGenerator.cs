using OctanGames.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctanGames
{
	public class RandomIndexGenerator
	{
		private int _count = 0;

		private Queue<int> _indices;
		private List<int> _cache;

		public RandomIndexGenerator()
		{
		}

		public RandomIndexGenerator(int size)
		{
			Randomize(size);
		}

		public void Randomize(int size)
		{
			if (size != _count)
			{
				_cache = new List<int>(size);
				for (int i = 0; i < size; i++)
				{
					_cache.Add(i);
				}
			}
			else
			{
				for (int i = 0; i < _count; i++)
				{
					_cache[i] = i;
				}
			}

			_indices = new Queue<int>(_cache.Shuffle());
		}

		public int GetNextRandomIndex()
		{
			int randomIndex = _indices.Dequeue();
			_indices.Enqueue(randomIndex);
			return randomIndex;
		}
	}
}
