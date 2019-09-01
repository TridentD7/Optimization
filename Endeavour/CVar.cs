using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeavour
{
    public class CVar
    {
        
            private static int _ObjectNumber;
            private string _Name;
            private double _Min;
            private double _Max;
            private double _Value;
            private double _Code;
            private double _MaxMinusMin;
            private double _BestValue;
            private double _BestCode;
            private bool _bOnBorder = false;
            private double _Mashtab = 1.0;
            private double _dStepNearOptimum=0.01;



            public double GetMashtab()
            {
              return _Mashtab;
            }
            public bool GetBorderStatus()
            {
                return _bOnBorder;
            }

            public double BestValue()
            {
                return _BestValue;
            }

            public double BestCode()
            {
              return _BestCode;
            }

            public void RememberBestValue()
            {
             _BestValue = _Value;
             _BestCode = (_BestValue - _Min) / _MaxMinusMin;
            }

            public double Code()
            {
                return _Code;
            }

            public double Value()
            {
                return _Value;
            }

            public string Name()
            {
                return _Name;
            }

            public double Min()
            {
                return _Min;
            }

            public double Max()
            {
                return _Max;
            }

            public int ObjectNumber()
            {
                return _ObjectNumber;
            }

            public void SetCode(double Code)
            {
                if (Code <= 0)
                {
                    _Code = 0;
                    _bOnBorder = true;
                }


                if (Code >= 1)
                {
                    _Code = 1;
                    _bOnBorder = true;
                }

                if (Code < 1 && Code > 0)
                {
                    _Code = Code;
                    _bOnBorder = false;
                }


                _Value = _Min + _Code * _MaxMinusMin;

            }

            public void SetValue(double Value)
            {

                if (Value <= _Min)
                {
                    _Value = _Min;
                    _bOnBorder = true;
                }

                if (Value >= _Max)
                {
                    _Value = _Max;
                    _bOnBorder = true;

                }

                if (Value < _Max && Value > _Min)
                {
                    _Value = Value;
                    _bOnBorder = false;
                }

                _Code = (_Value - _Min) / _MaxMinusMin;

            }

            public CVar(string Name, double Min, double Max, double Value)
            {
                if (CheckInputData(Min, Max, Value))
                {
                    _ObjectNumber++;
                    _Name = Name;
                    _Min = Min;
                    _Max = Max;
                    _MaxMinusMin = _Max - _Min;
                    _Value = Value;
                    _Code = (Value - Min) / _MaxMinusMin;
                    _BestCode = _Code;   
                    _BestValue = _Value;
                }
                else
                {
                    return;
                }

            }

          public CVar(string Name, double Min, double Max, double Mashtab, double Value)
          {
            if (CheckInputData(Min, Max, Value) & Mashtab>0)
            {
                _ObjectNumber++;
                _Name = Name;
                _Min = Min;
                _Max = Max;
                _MaxMinusMin = _Max - _Min;
                _Value = Value;
                _Code = (Value - Min) / _MaxMinusMin;
                _BestCode = _Code;
                _BestValue = _Value;
                _Mashtab = Mashtab;
            }
            else
            {
                return;
            }

          }

        private bool CheckInputData(double Min, double Max, double Value)
            {
                int part = 0;

                if (Max <= Min)
                {
                    throw new System.ArgumentException(_Name + ": Max cannot be less then Min", "CParameter");
                }
                else
                {
                    part++;
                }

                if (Value < Min || Value > Max)
                {
                    throw new System.ArgumentException(_Name + ": Value out of range", "CParameter");
                }
                else
                {
                    part++;
                }

                if (part > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }
    }

