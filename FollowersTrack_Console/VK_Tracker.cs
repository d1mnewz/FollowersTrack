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
using System.Threading;

namespace FollowersTrack
{
    public class State
    {
        public DateTime Checked;
        public VkCollection<User> followers = null;
        public List<long> followersIDs = new List<long>();
        public List<long> NewFollowersIDs = new List<long>();
        public List<long> UnFollowedIDs = new List<long>();
        public State()
        {

        }
    }
    public class VK_Tracker
    {
        public State last = null;
        private static VkApi _api;

        public static String CommunityScreenName = ConfigurationManager.AppSettings["CommunityID"];

        public System.Timers.Timer timer;
        public void ShowUnfollows()
        {
            if (this.last.UnFollowedIDs.Count > 0)
            {
                Console.Write(DateTime.Now.ToString() + " Current: {0} -Unfollow: ", this.last.followersIDs.Count);
                //Console.WriteLine("Unfollows");

                foreach (var el in last.UnFollowedIDs)
                {
                    User userModel = _api.Users.Get(el, null, null, true);


                    Console.WriteLine(userModel.FirstName + " " + userModel.LastName + " ID " + userModel.Id);
                }


            }
        }

        public void ShowNewFollows()
        {
            if (this.last.NewFollowersIDs.Count > 0)
            {
                Console.Write(DateTime.Now.ToString() + " Current: {0}  +Follow: ", this.last.followersIDs.Count);
                foreach (var el in last.NewFollowersIDs)
                {
                    User userModel = _api.Users.Get(el, null, null, true);


                    Console.WriteLine(userModel.FirstName + " " + userModel.LastName + " ID " +  userModel.Id);
                }


            }
        }

        public void GetNewState()
        {
            State newState = new State();

            newState.followers = _api.Groups.GetMembers(
                new GroupsGetMembersParams
                { GroupId = CommunityScreenName },
                true);
            newState.followersIDs = newState.followers.Select(x => x.Id).ToList();
            


            newState.Checked = DateTime.Now;
            //newState.NewFollowers = newState.followers.Concat(last.followers).Distinct(o => o.);
           // Console.WriteLine("Check state at " + DateTime.Now );
            //foreach (var el in newState.followers)
            //{
            //    ...Console.WriteLine(el);
            //}

            if (last.followersIDs.Count > 0)
            {
                newState.UnFollowedIDs = last.followersIDs.Except(newState.followersIDs).ToList();
                newState.NewFollowersIDs = newState.followersIDs.Except(last.followersIDs).ToList();
                ShowNewFollows();
                ShowUnfollows();
            }
            last = newState;




            Thread.Sleep(10000);
            GetNewState();
            






        }




        public VK_Tracker()
        {
            //CheckCurrentState();
            last = new State();
            timer = new System.Timers.Timer(10000); // every 5 hours
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            _api = new VkApi();
            Console.WriteLine("Started {0}; Target: {1}", DateTime.Now.ToString(), CommunityScreenName);


        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // ignore the time, just compare the date
            //if (last.Checked.Date < DateTime.Now.Date)
            //{
                // stop the timer while we are running the cleanup task
                timer.Stop();
                GetNewState();
                //
                // do cleanup stuff
                //

                timer.Start();
            //}
        }











    }
}
