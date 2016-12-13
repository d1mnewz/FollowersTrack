using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FollowersTrack
{
   public static class Logger // via vkNet api
    {
        public static void WriteLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim());
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void WriteLog(String error)
        {
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + error.Trim());
                }
            }
            catch (Exception e)
            {

            }
        }

    }
}
