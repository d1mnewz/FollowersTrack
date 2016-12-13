using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using System.Configuration;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkNet.Model;
using System.Timers;
using VkNet.Enums.Filters;

namespace FollowersTrack
{
    public class State
    {
        public DateTime Checked;
        public VkCollection<User> followers;
    }
    public class VK_Tracker
    {
        public State last;
        private static VkApi _api;
        
        public static String CommunityScreenName = ConfigurationManager.AppSettings["CommunityID"];
        
        public Timer timer;
        public VK_Tracker()
        {
            CheckCurrentState();
            timer = new Timer(5 * 60 * 60 * 1000); // every 5 hours
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            _api = new VkApi();
            
        }
        
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // ignore the time, just compare the date
            if (last.Checked.Date < DateTime.Now.Date)
            {
                // stop the timer while we are running the cleanup task
                timer.Stop();
                CheckCurrentState();
                //
                // do cleanup stuff
                //
                
                timer.Start();
            }
        }
        public void CheckCurrentState()
        {
            
            last.followers = _api.Groups.GetMembers(new GroupsGetMembersParams { GroupId = CommunityScreenName }, true);
            if (last.followers.Count > 0)
            {
                foreach (var el in last.followers)
                {
                    Logger.WriteLog(String.Format("UserID {0}, Name {1} {2}", el.Id, el.FirstName, el.LastName));

                }
            }
            else
                Logger.WriteLog("Some error occurred in CheckCurrentState()");

            last.Checked = DateTime.Now;
            
            
        }







    }
}
