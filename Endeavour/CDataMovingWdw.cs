using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeavour
{
   public class CDMW
    {
        private Queue<double> Data; 
        private int _Length;
       
        public void Add(double val)
        {
            if (Data.Count < _Length)
            {
                Data.Enqueue(val);
            }
            else
            {
                Data.Dequeue();
                Data.Enqueue(val);
            }
           
        }
        public CDMW(int length)
        {
            Data = new Queue<double>();
            _Length = length;

            for (int i = 0; i < length; i++)
            {
                Data.Enqueue(0);
            }
        }

        public double AvgValue()
        {
            return Data.Average();
        }
        public double Sum()
        {
            return Data.Sum();
        }

        public double AtIndexLIFO(int index)
        {
            if (index >= _Length|| index<0)
            {
                throw new System.ArgumentException(" Index out of range", "CDMW");
            }
            return Data.ElementAt(_Length-index-1);
        }

        public double AtIndexFIFO(int index)
        {
            if (index >= _Length || index < 0)
            {
                throw new System.ArgumentException(" Index out of range", "CDMW");
            }

            return Data.ElementAt(index);
        }


    }
}
