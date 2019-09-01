using Endeavour;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndeavourTest
{
    public partial class Form1 : Form
    {
        public class SUMSQR : CModel
        {
            public Random rnd;
            int _K;
            public SUMSQR(int k)
            {
                rnd = new Random(DateTime.Now.Millisecond);
                this._VarParameters = new CVar[k];
                _K = k;
                for (int i = 0; i < k; i++)
                {
                    this._VarParameters[i] = new CVar("Var parameter # " + i.ToString(), -200, 200, 1, -200+rnd.NextDouble()*400);
                }
            }
            public override double TargetFunction()
            {
                double rez = 0;
                for (int i = 0; i < _K; i++)
                {
                    rez += Math.Pow(0 - this._VarParameters[i].Value(), 2);
                }

                return rez;
            }

            public override void Save()
            {
                //
            }

            public override bool CheckFirstTypeLimits()
            {
              /*  if (this._VarParameters[0].Value() < 6 || this._VarParameters[1].Value() < 6)
                {
                    return false;
                }*/
                return true;
            }

            public override bool CheckSecondTypeLimits(double tfValue)
            {
                
                    return true;
               
            }

        }

        public class Global : CModel
        {
            public Random rnd;
            double x=0;
            double y = 0;
            public Global()
            {
                rnd = new Random(DateTime.Now.Millisecond);
                this._VarParameters = new CVar[2]; 
                this._VarParameters[0] = new CVar("x", -3*Math.PI, 3 * Math.PI, -2.3);
                this._VarParameters[1] = new CVar("y", -3 * Math.PI, 3 * Math.PI,-4.2);

            }
            public override double TargetFunction()
            {
               /* x = this._VarParameters[0].Value();
                y = this._VarParameters[1].Value();*/

                return Math.Pow(Math.Sin((System.Double)(x * y * y + 2 * x * x)), 0.2e1) + Math.Pow(Math.Sin((System.Double)(x * x - 12 * x * y + 4 * y)), 0.2e1);

            }

            public override void Save()
            {
                //
            }

            public override bool CheckFirstTypeLimits()
            {
                double cg1 = 0;
                double cg2 = 0;    

                x = this._VarParameters[0].Value();
                y = this._VarParameters[1].Value();


                cg1 = Math.Log(Math.Pow(x, 0.4e1) + 0.1e1) + 0.8e1 * Math.Sin(x * x - y);
                if (cg1 > 0.01) return false;
                cg2 = (System.Double)(y * y) - x * x + x + Math.Sin(x) - (System.Double)(5 * y);
                if (cg2 > -1.2) return false;

                return true;
            }

            public override bool CheckSecondTypeLimits(double tfValue)
            {
                return true;
            }

        }

        public class Rastrigin : CModel
        {
            public Random rnd;
            double _K;
            public Rastrigin(int k)
            {
                _K = k;
                rnd = new Random(DateTime.Now.Millisecond);
                this._VarParameters = new CVar[k];
                for (int i = 0; i < k; i++)
                {
                    this._VarParameters[i] = new CVar("x"+i.ToString(), -5.12, 5.12, -5.12 + rnd.NextDouble() * 10.24);
                }

               

            }
            public override double TargetFunction()
            {
                double rez = 0;
                double A = 10.0;
                foreach (CVar v in _VarParameters)
                {
                    rez += v.Value() * v.Value() - A * Math.Cos(2 * Math.PI * v.Value());
                }
                return rez+A*_K;
            }

            public override void Save()
            {
                //
            }

            public override bool CheckFirstTypeLimits()
            {
                return true;
            }

            public override bool CheckSecondTypeLimits(double tfValue)
            {
                return true;
            }

        }

        public class SineRoot : CModel
        {
            public Random rnd;
            //Минимум равный 0 в точке, где xi=420.9687.
            int _K = 0;
            public SineRoot(int k)
            {
                _K = k;
                rnd = new Random(DateTime.Now.Millisecond);
                this._VarParameters = new CVar[k];
                for (int i = 0; i < k; i++)
                {
                    this._VarParameters[i] = new CVar("x" + i.ToString(), -500, 500, -500 + rnd.NextDouble() * 1000);
                }



            }
            public override double TargetFunction()
            {
                double rez = 0;
                double A = 418.9829;
                foreach (CVar v in _VarParameters)
                {
                    rez +=- v.Value()* Math.Sin(Math.Sqrt( Math.Abs(v.Value()) ));
                }
                return rez + A*_K;
            }

            public override void Save()
            {
                //
            }

            public override bool CheckFirstTypeLimits()
            {
                return true;
            }

            public override bool CheckSecondTypeLimits(double tfValue)
            {
                return true;
            }

        }


       
        CEndeavour algopt;
        EVF algform;
        CSTD nrm;
       


        public Form1()
        {
            InitializeComponent();
            nrm = new CSTD();

            /*
                       algopt.AddMessageEventHandler(algform.handlerMessage);
                       algopt.AddFinishEventHandler(algform.handlerFinish);
                       algform.AddStopEventHandler(algopt.Stop);
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Global maple = new Global();
             CCantileverBeam balka = new CCantileverBeam("balka1.mdl", 4.0, 120, 0.005);
             SUMSQR test = new SUMSQR(100);
             Rastrigin rst = new Rastrigin(3);     
             SineRoot sr = new SineRoot(5);

            algopt = new CEndeavour(test);

            algopt.RandomScanAllowed = true;
            algopt.AllowReport=true;
            algopt.AllowASPPMode = true;
            algopt.StepChance = 0.025;
            algform = new EVF(algopt);
          //  algform.CloseFormAfterFinish();

            algform.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            algform.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             CDMW mv = new CDMW(5);
            for (int i = 0; i < 10; i++)
            {
                mv.Add(i);
            }
            double d = mv.AtIndexLIFO(0);
            double mm = mv.AtIndexFIFO(0);
            //nrm.GetToFile_CLT("CLT_01_200.txt", 200, 1000, 0, 1);
            nrm.GetToFile_NRM("ShNRM.txt", 1000);
            MessageBox.Show("Готово!");
            //clt12=38ms1
            //BM=16ms
            /*double rr = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                rr += nrm.BM();
            }
            sw.Stop();
            MessageBox.Show((sw.ElapsedMilliseconds).ToString());*/
        }
    }
}
// сделать ввод вывод массива им

// сделать отчёт !!!!!!!!!!!!!!!!!!!!!!!!!
// сделать уточнение решения
// сделать обзор окрестности