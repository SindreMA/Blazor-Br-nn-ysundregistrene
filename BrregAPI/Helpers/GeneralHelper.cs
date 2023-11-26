using BrregAPI.Modals;
using BrregAPI.Modals.Database;
using BrregAPI.Modals.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrregAPI.Helpers
{
    public class GeneralHelper
    {
        internal UserObject FirstLoadData(string id)
        {
            var context = new BrregContext();
            var user = context.Users.FirstOrDefault(x=> x.Id == id);

            var data = new UserObject(user);

            return data;
        }

        public static string HttpGet(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }         
        }
    }
}
