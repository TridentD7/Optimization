using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Endeavour
{
    public partial class EVF : Form
    {
        bool _bCanUpdate = true;
        public Action stop;
        bool _FinishFlag=false;
        CEndeavour _Alg;
        CAnnouncement _msgBuffer;
        DateTime _StartTime;
        double _speed;
        double _seconds;
        int _localstop;
     
        public EVF(CEndeavour alg)
        {
            _Alg = alg;
            InitializeComponent();
           
            alg._MesssageEvent += handlerMessage;
            this.AddStopEventHandler(alg.Stop);
            this.progressBar_BS.Maximum = alg.BadStepStopCriterion;
            _localstop= alg.BadStepStopCriterion;
            _msgBuffer = new CAnnouncement();
            alg._FPNnotFound = handlerFinish;
        }

     
        public void CloseFormAfterFinish()
        {
            _Alg._Finish+=handlerFinish;
        }
            
        public void Start()
        {
            _StartTime = DateTime.Now;
            _FinishFlag = false;
            label_argsqty.Text = _Alg.VarQuantity.ToString();
            label_timestart.Text = _StartTime.ToString();
            label_historyLength.Text = _Alg.HistoryLength.ToString();
            label_stopcriterion.Text = _Alg.BadStepStopCriterion.ToString();
            this.Show();
            _Alg.Start();
        }

        public void Stop()
        {  
            _Alg.Stop();
            this.Close();
        }


        private void checkBox_upd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_upd.Checked)
            {
                _bCanUpdate = false;
            }
            else
            {
                _bCanUpdate = true;
            }
            this.groupBox1.Enabled = _bCanUpdate;
            InfUpd(_msgBuffer);

        }

        private void EVF_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(!_FinishFlag&&progressBar_BS.Value!= progressBar_BS.Maximum)
            if (MessageBox.Show("Do you want to exit?", "Search process still running",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
            {
                stop();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
          
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if(_Alg.FirstPointStatus)
            this.Close();
        }

    
        public void AddStopEventHandler(Action dlg)
        {
            if (dlg != null)
            {
                stop += dlg;
            }
        }

        public void InfUpd(CAnnouncement msg)
        {

            if (progressBar_BS.InvokeRequired)

                progressBar_BS.Invoke((MethodInvoker)delegate { progressBar_BS.Value = msg.iBadStepQty; });

            else
                progressBar_BS.Value = msg.iBadStepQty;

            //-----------------------------------------------------------------------------------------------
            if (label_TF.InvokeRequired)

                label_TF.Invoke((MethodInvoker)delegate { label_TF.Text = msg.dTFValue.ToString(); });
        
            else
                label_TF.Text = msg.dTFValue.ToString();

            //-----------------------------------------------------------------------------------------------
            if (label_LuckSteps.InvokeRequired)

                label_LuckSteps.Invoke((MethodInvoker)delegate { label_LuckSteps.Text = msg.lGoodStepNumber.ToString(); });

            else
                label_LuckSteps.Text = msg.lGoodStepNumber.ToString();

            //-----------------------------------------------------------------------------------------------
            if (label_calculated.InvokeRequired)

                label_calculated.Invoke((MethodInvoker)delegate { label_calculated.Text = msg.uTFCalcQty.ToString(); });

            else
                label_calculated.Text = msg.uTFCalcQty.ToString();

            //-----------------------------------------------------------------------------------------------
            if (label_Args.InvokeRequired)

                label_Args.Invoke((MethodInvoker)delegate { label_Args.Text = msg.iArgsOnBorder.ToString(); });

            else
                label_Args.Text = msg.iArgsOnBorder.ToString();

            //-----------------------------------------------------------------------------------------------
            if (_msgBuffer.iBadStepQty< _localstop)
            {
                _seconds = (DateTime.Now - _StartTime).TotalSeconds;
                _speed = _seconds / msg.uTFCalcQty;
            }
            //-----------------------------------------------------------------------------------------------
            if (label_avgtime.InvokeRequired)
                      label_avgtime.Invoke((MethodInvoker)delegate { label_avgtime.Text = String.Format("{0:E}", _speed); });

            else
                label_avgtime.Text = String.Format("{0:E} ", _speed); ;
            
        }

        public void handlerMessage(CAnnouncement msg)
        {
            if (_bCanUpdate && !this.IsDisposed)
            {
                InfUpd(msg);
            }

            _msgBuffer = msg;
         }

        public void handlerFinish()
        {
            _FinishFlag = true;
            this.Close();
        }

    }
  }
