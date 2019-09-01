using System;

namespace Endeavour
{
    public class CSTD
    {
        Random _R;

        public CSTD()
        {
            _R = new Random(DateTime.Now.Millisecond);
        }

        public double BM()
        {
            double r1 = _R.NextDouble();
            double r2 = _R.NextDouble();
            return Math.Sqrt(-2 * Math.Log(r1)) * Math.Cos(2 * Math.PI * r2);
        }

        public double BM(double m, double s)
        {
            return m + s * BM();
        }

        public double CLT12()
        {
            double x = 0;
            for (int i = 0; i < 12; i++)
            {
                x += _R.NextDouble();
            }

            return x - 6;
        }

        public double CLT(int n, double m, double s)
        {
            double x = 0;
            double d = 0;
            for (int i = 0; i < n; i++)
            {
                x += _R.NextDouble();
            }
            d = (x - (double)n/2) / Math.Sqrt((double)n/ 12);

            return m + s * d;
        }

        public void GetToFile_BM(string filename, int qty, double m, double s)
        {

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filename))
            {

                for (int i = 0; i < qty; i++)
                    file.WriteLine(BM(m,s).ToString());
                   
            }
    
         }

        public void GetToFile_CLT(string fileName, int qty, int n, double m, double s)
        {

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName))
            {

                for (int i = 0; i < qty; i++)
                    file.WriteLine(CLT(n, m, s).ToString());
                    
            }

        }

        public double Nrm()
        {
            double rez = 0;
            for (int j = 0; j < 5; j++)
                 rez += _R.NextDouble();
            rez = 0.7746 * (2 * rez - 5);
            rez += 0.01 * (rez * rez * rez - 3 * rez);

            return rez;
        }

        public void GetToFile_NRM(string filename, int qty)
        {

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filename))
            {

                for (int i = 0; i < qty; i++)
                    file.WriteLine(Nrm().ToString());

            }

        }

    }


}

