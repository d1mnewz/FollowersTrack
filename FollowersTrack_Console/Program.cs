using FollowersTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FollowersTrack_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            VK_Tracker vk_t = new VK_Tracker();
            vk_t.GetNewState();

        }
    }
}
