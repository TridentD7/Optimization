using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeavour
{
    public class CCantileverBeam : CModel
    {

        // Консольно закреплённая балка длинны beamLength
        // свободный конец нагружен массой load
        // допустимый прогиб allowdelt
        // подбирается сечение профиля так чтобы масса балки была минимальной

        const double g = 9.81;
        const double SigmaMax = 16e7;
        const double E = 2e11;
        const double Ro = 7800;

        const double bMin = 0.01;
        const double bMax = 0.3;

        const double hMin = 0.01;
        const double hMax = 0.3;

        double _L;
        double _P;
        double _BeamH = 0.021;
        double _BeamB = 0.011;
        double _AllowDelt;
        string _Name;
        double _bestTF = 1e38;


       
               
        public override double TargetFunction()
        {           
            return  _L * _BeamH * _BeamB * Ro; ;
        }

        public double Sigma()
        {
            return 6 * _P * _L / (_BeamB * _BeamH * _BeamH);
        }

        public double Delta()
        {
            return 4 * _L * _L * _L * _P / (E * _BeamB * _BeamH * _BeamH * _BeamH);
        }

        public double DeltaAtLoad(double Massa)// сервисная
        {
            if (Massa < _P / g && Massa > 0)
            {
                return 4 * _L * _L * _L * Massa * g / (E * _BeamB * _BeamH * _BeamH * _BeamH);
            }
            return -1;
        }

        public double ManualTarget(double h, double b)
        {
            _VarParameters[0].SetValue(h);
            _VarParameters[1].SetValue(b);
            double tf = this.TargetFunction(); ;

            if (CheckFirstTypeLimits() && CheckSecondTypeLimits(tf))
            {

                if (tf < _bestTF)
                {
                    _VarParameters[0].RememberBestValue();
                    _VarParameters[1].RememberBestValue();
                    Save();
                }
                return tf;
            }
            return -1;
        }

        public override bool CheckFirstTypeLimits()
        {

            _BeamH = _VarParameters[0].Value();
            _BeamB = _VarParameters[1].Value();

            if (Sigma() > SigmaMax)
                return false;

            if (Delta() > _AllowDelt)
                return false;

            return true; 
        }

        public override bool CheckSecondTypeLimits(double tfValue)
        {
            // вторично ЦФ не вычислять!!!!
            //if (tfValue < 10)
            //    return false;

            return true; 
        }


        public CCantileverBeam(string filename, double beamLength, double load, double allowdelt)
        {
            this._L = beamLength;
            this._P = load * g;
            this._AllowDelt = allowdelt;
            this._Name = filename;
            _VarParameters = new CVar[2];
            _VarParameters[0] = new CVar("Высота сечения балки", hMin, hMax, _BeamH);
            _VarParameters[1] = new CVar("Ширина сечения балки", bMin, bMax, _BeamB);


        }
        public CCantileverBeam(string filename)
        {
            if (File.Exists(filename))
           {
                this._Name = filename;
                FileInfo f = new FileInfo(filename);
                using (FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        this._L = br.ReadDouble();
                        this._P = br.ReadDouble();
                        this._AllowDelt = br.ReadDouble();

                        _BeamH = br.ReadDouble();
                        _BeamB = br.ReadDouble();

                        _VarParameters = new CVar[2];
                        _VarParameters[0] = new CVar("Высота сечения балки", hMin, hMax, _BeamH);
                        _VarParameters[1] = new CVar("Ширина сечения балки", bMin, bMax, _BeamB);

                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
                return;
            }
        }

        public override void Save()
        {
            FileInfo f = new FileInfo(_Name);
            using (FileStream fs = f.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write((double)_L);
                    bw.Write((double)_P);
                    bw.Write((double)_AllowDelt);

                    bw.Write((double)_VarParameters[0].BestValue());
                    bw.Write((double)_VarParameters[1].BestValue());
                }
            }
        }

    }
}
