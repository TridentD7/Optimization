using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Endeavour
{
    public class CAnnouncement : EventArgs
    {

        public double dTFValue;
        public long lGoodStepNumber;
        public ulong uTFCalcQty;
        public int iArgsOnBorder;
        public int iBadStepQty;
    }
    public delegate void AlgAnnouncement(CAnnouncement msg);

    public class CEndeavour
    {
        CModel _Model;
        CSTD _Hn;
        Random _ERnd;

        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken _Token;
       

        public event AlgAnnouncement _MesssageEvent = null;
        CAnnouncement _Message;
        public Action _Finish;
        public Action _FPNnotFound;

        private static System.Timers.Timer _UpdTimer;
        private static System.Timers.Timer _SaveTimer;

       
        Stopwatch _Sw = new Stopwatch();

        //-------------------------------

        const double TOOMUCH = 1e308;
        const double _MINdTf = -1e-6;
        const int _N0 = 350;
        
        
        private int _iFirstPointSec = 10;
        private int _iRandomScanSec = 10;
        private bool _bFirstPoint = false;
        private bool _bAllowASPP = false;
        private bool _bAllowReport = false;
        private bool _bRandomScanAllowed=false;
        private double _dStepProb = 0.5;
        double _dRndMashtab = 1; // перключатель глобально/локально- очень чувствительно

        int _M = 5;
        int _iVPQty = 0;
        private int _iStop = 55;
        int _iGoodStepNumber = 0;
        int _iBadStepsMax = 0;
        int _iBadSteps = 0;
        private ulong _uTFCalcQty = 0;
        private double _dTFunctionBestValue = TOOMUCH;
        private double _dTFCurrentValue = TOOMUCH;
        private double _dLastSavedTFValue = TOOMUCH;
        int _iArgsOnBorder = 0;

        CDMW _Matrix01;
        CDMW _MatrixDTF;
        CDMW[] _MatrixKxM;
       

        //--------------------------------------------------

        public int FirstPointSec
        {
            get
            {
                return _iFirstPointSec;
            }

            set
            {
                if (value>0)
                _iFirstPointSec = value;
            }
        }

        public bool FirstPointStatus
        {
            get
            {
                return _bFirstPoint;
            }

        }

        public bool AllowReport
        {
            get
            {
                return _bAllowReport;
            }

            set
            {
                _bAllowReport = value;
            }
        }

        public double RndMashtab
        {
            get
            {
                return _dRndMashtab;
            }

            set
            {
                if (value>0)
                _dRndMashtab = value;
            }
        }

        public int HistoryLength
        {
            get
            {
                return _M;
            }

            set
            {
                if (value > 2)
                {
                    _M = value;
                    _Matrix01 = new CDMW(_M);
                    _MatrixDTF = new CDMW(_M);
                    _MatrixKxM = new CDMW[_iVPQty];
                    for (int i = 0; i < _iVPQty; i++)
                    {
                        _MatrixKxM[i] = new CDMW(_M);
                    }
                }

            }
        }

        public int VarQuantity
        {
            get
            {
                return _iVPQty;
            }

        }

        public int BadStepStopCriterion
        {
            get
            {
                return _iStop;
            }

            set
            {
                if (value>_N0+2*Math.Sqrt(_iVPQty))
                _iStop = value;
            }
        }

        public bool RandomScanAllowed
        {
            get
            {
                return _bRandomScanAllowed;
            }

            set
            {
                _bRandomScanAllowed = value;
            }
        }

        public bool AllowASPPMode
        {
            get
            {
                return _bAllowASPP;
            }

            set
            {
                _bAllowASPP = value;
            }
        }

        public double StepChance
        {
            get
            {
                return _dStepProb;
            }

            set
            {
                 if(value>0&value<1) 
                _dStepProb = value;
            }
        }


        //------------------------------

        public CEndeavour(CModel Model)
        {
            _Model = Model;
            _iVPQty = _Model._VarParameters.Count();
            _iStop = (int)((_N0 + Math.Round(5 * Math.Sqrt(_iVPQty))));
            if (_iVPQty > 20)
            {
                _bAllowASPP = true;
            }

            _Matrix01 = new CDMW(_M);
            _MatrixDTF = new CDMW(_M);
            _MatrixKxM = new CDMW[_iVPQty];
            for (int i = 0; i < _iVPQty; i++)
            {
                _MatrixKxM[i] = new CDMW(_M);
            }
            _Hn = new CSTD();
            _ERnd = new Random(DateTime.Now.Millisecond);
            _Message = new CAnnouncement();
            _Token = cancelTokenSource.Token;
            SetInfUpdateInterval(500);
            SetSaveInterval(5000);
        }


//-----------------------------------------------------------------------
        private void SendMessage()
        {
            if (_MesssageEvent != null)
            {
                _Message.dTFValue = _dTFunctionBestValue;
                _Message.lGoodStepNumber = _iGoodStepNumber;
                _Message.iBadStepQty = _iBadSteps;
                _Message.uTFCalcQty = _uTFCalcQty;
                _Message.iArgsOnBorder = _iArgsOnBorder;
                _MesssageEvent(_Message);
            }

        }

        public void SetInfUpdateInterval(int val)
        {
            _UpdTimer = new System.Timers.Timer(val);
            _UpdTimer.Elapsed += OnTimedEvent;
            _UpdTimer.AutoReset = true;
        }

        public void SetSaveInterval(int val)
        {
            _SaveTimer = new System.Timers.Timer(val);
            _SaveTimer.Elapsed += SaveTimedEvent;
            _SaveTimer.AutoReset = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SendMessage();
        }

        private void SaveTimedEvent(Object source, ElapsedEventArgs e)
        {
            Save();
        }

        public void Save()
        {
            if (_dTFunctionBestValue < _dLastSavedTFValue)
            {
                _Model.Save();
                _dLastSavedTFValue = _dTFunctionBestValue;
            }
        }

        public async void Start()
        {
            _Sw.Start();
            await Task.Factory.StartNew(Search);
            _Sw.Stop();
            _Finish?.Invoke();
            if (!_bFirstPoint)
                _FPNnotFound?.Invoke();
            if (_bAllowReport)
                MakeReport();
        }

        private void MakeReport()
        {

            using (System.IO.StreamWriter file =
              new System.IO.StreamWriter("EndeavourReport.txt", false))
            {
                file.WriteLine("Report of 'Endevaur' optimisation module");
                file.WriteLine("");
                file.WriteLine("'History length' parameter of algorithm: " + _M.ToString());
                file.WriteLine("Variable parameters quantity: " + _iVPQty.ToString());
                file.WriteLine("Stop criterion: " + _iStop.ToString());
                file.WriteLine("");
                file.WriteLine("Elapsed time: " + _Sw.Elapsed.ToString());
                file.WriteLine("Target function was calculated: " + _uTFCalcQty.ToString());
                file.WriteLine("Target function best value: " + _dTFunctionBestValue.ToString());
                file.WriteLine("Arguments on border: " + _iArgsOnBorder.ToString());
                file.WriteLine("");
                foreach (CVar vp in _Model._VarParameters)
                {
                    file.WriteLine(vp.Name() + " min=" + vp.Min().ToString() + " max=" + vp.Max().ToString() + " border=" + vp.GetBorderStatus().ToString() + " result=" + vp.BestValue().ToString());
                }
            }


        }

        public void Stop()
        {
            cancelTokenSource.Cancel();
        }

        
        public double TF()
        {
            _uTFCalcQty++;
            return _Model.TargetFunction();
        }

        private double Mn()
        {

            double dd = Math.Exp(-2.3 * (Math.Pow((double)_iBadSteps / _iStop, 2) + Math.Pow((double)_iBadStepsMax / _iStop, 2)));
            dd = _dRndMashtab * 0.1 * dd / Math.Sqrt(_iVPQty);

            return dd;
        }

        private void Step()
        {
            double z = 0;
            double dz = 0;
            double sf = _MatrixDTF.Sum();
            double su = _Matrix01.Sum();
            double mp = Mn();

            if (sf < _MINdTf)
            {
                for (int i = 0; i < _iVPQty; i++)
                {
                    dz = 0;
                    z = _Model._VarParameters[i].BestCode();
                    for (int j = 0; j < _M; j++)
                    {
                        dz += _MatrixDTF.AtIndexLIFO(j) * _MatrixKxM[i].AtIndexLIFO(j) * (1.0 + (double)((1.0 - j) / _M));
                    }
                    dz += su * su / (_M * sf);
                    dz += (1 - su / _M) * mp * _Hn.BM();

                    _Model._VarParameters[i].SetCode(z + Asp() * _Model._VarParameters[i].GetMashtab() * dz);
                }
            }
            else
            {
                for (int i = 0; i < _iVPQty; i++)
                {
                    dz = 0;
                    z = _Model._VarParameters[i].BestCode();
                    dz = _Hn.BM() * mp;
                    _Model._VarParameters[i].SetCode(z + Asp() * _Model._VarParameters[i].GetMashtab() * dz);
                }

            }
        }

        private void CheckStepResult()
        {
            if (_Model.CheckFirstTypeLimits())
            {
                _dTFCurrentValue = TF();
                if (_Model.CheckSecondTypeLimits(_dTFCurrentValue))
                {
                    if (_dTFCurrentValue < _dTFunctionBestValue)
                    {
                        GoodStepEffect();
                        return;
                    }
                }

            }

            BadStepEffect();
        }

        private void GoodStepEffect()
        {
            _iBadSteps = 0;
            _iArgsOnBorder = 0;
            _iGoodStepNumber++;

            if (_iGoodStepNumber > 0)
            {
                _Matrix01.Add(1);
                _MatrixDTF.Add(_dTFCurrentValue - _dTFunctionBestValue);

                for (int i = 0; i < _iVPQty; i++)
                {
                    _MatrixKxM[i].Add(_Model._VarParameters[i].Code() - _Model._VarParameters[i].BestCode());
                    _Model._VarParameters[i].RememberBestValue();
                    if (_Model._VarParameters[i].GetBorderStatus())
                    {
                        _iArgsOnBorder++;
                    }
                }
            }
            _dTFunctionBestValue = _dTFCurrentValue;
        }

        private void BadStepEffect()
        {
            _iBadSteps++;
            if (_iBadSteps > _iBadStepsMax)
            {
                _iBadStepsMax = _iBadSteps;
            }
            _Matrix01.Add(0);
            _MatrixDTF.Add(0);
            for (int i = 0; i < _iVPQty; i++)
            {
                _MatrixKxM[i].Add(0);
            }

        }

        private void Search()
        {
            if (!CheckLimits())
            {
                FirstPointSearch();
            }
            if (CheckLimits())
            {
                if (_bRandomScanAllowed)
                {
                    RandomScan();
                }

                _bFirstPoint = true;
                _SaveTimer.Enabled = true;
                if (_MesssageEvent != null)
                    _UpdTimer.Enabled = true;

                do
                {
                    //----------------------
                    Step();
                    CheckStepResult();
                    //----------------------

                } while (_iBadSteps < _iStop && !_Token.IsCancellationRequested);

               
                Save();
                _SaveTimer.Enabled = false;
                _UpdTimer.Enabled = false;
                if (_iBadSteps >= _iStop)
                    SendMessage();

            }
        }

       
        private void FirstPointSearch()
        {
            DateTime started;
            bool b = false;
            do
            {
                started = DateTime.Now;
                do
                {
                    foreach (CVar v in _Model._VarParameters)
                    {
                        v.SetCode(_ERnd.NextDouble());
                    }
                    b = CheckLimits();
                } while ((b == false) & (DateTime.Now.Subtract(started).TotalSeconds < FirstPointSec));

            } while ((b == false) && (MessageBox.Show("The first base point that satisfies all the constraints could not be found. Try again?", "Houston..we have a problem",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes));

            if (b)
                foreach (CVar v in _Model._VarParameters)
                {
                    v.RememberBestValue();
                }

        }

        private bool CheckLimits()
        {
            if (_Model.CheckFirstTypeLimits())
            {
                _dTFCurrentValue = TF();
                if (_Model.CheckSecondTypeLimits(_dTFCurrentValue))
                {
                    return true;
                }

            }
            return false;
        }

        private double Asp()
        {
            if (_bAllowASPP)
            {
                if (_ERnd.NextDouble() > (1 - _dStepProb))
                {
                    return 1;
                }
            }
            return 0;
        }

        private void RandomScan()
        {
            DateTime started;
          
            started = DateTime.Now;
            do
            {
                foreach (CVar v in _Model._VarParameters)
                {
                    v.SetCode(_ERnd.NextDouble());
                }
                if (CheckLimits())
                {
                    if (_dTFCurrentValue < _dTFunctionBestValue)
                    {
                        _dTFunctionBestValue = _dTFCurrentValue;
                        foreach (CVar v in _Model._VarParameters)
                        {
                            v.RememberBestValue();
                        }
                    }
                }
            } while ((DateTime.Now.Subtract(started).TotalSeconds < _iRandomScanSec));

        }

    }
}
