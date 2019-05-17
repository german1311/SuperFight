using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchfight.presentation.Engines
{
    public interface IEngine
    {
        string Name { get; }
        Result Find(string word);
    }
}
