using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctanGames.Managers
{
	public interface ITaskGenerator
	{
		string GetNextTask();
	}
}
