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
    public partial class FollowersTrackService : ServiceBase
    {
        public VK_Tracker Vk_track;
        public FollowersTrackService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                

                VK_Tracker vk_t = new VK_Tracker();
                //vk_t.GetNewState();
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
