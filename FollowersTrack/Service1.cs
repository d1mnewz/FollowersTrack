using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FollowersTrack
{
    public partial class Service1 : ServiceBase
    {
        public VK_Tracker Vk_track;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Logger.WriteLog(DateTime.Now.ToString() + ": service started.");

                Vk_track.CheckCurrentState();
            }
            catch { }
        }

        protected override void OnStop()
        {
            try
            {
                Vk_track.timer.Stop();
                Logger.WriteLog(DateTime.Now.ToString() + ": service stopped.");
            }
            catch { }
            }
    }
}
