using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeavour
{
    public abstract class CModel
    {
        public abstract bool CheckFirstTypeLimits();
        public abstract bool CheckSecondTypeLimits(double tfValue);
        public abstract double TargetFunction();
        public abstract void Save();// cохрянять bestvalue из _VarParameters
        public CVar[] _VarParameters;

    }
}
