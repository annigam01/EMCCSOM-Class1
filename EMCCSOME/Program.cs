using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

//sharepoint spefics usings
using Microsoft.SharePoint.Client;

namespace Superhero
{
    class superman
    {
        static void Main()
        {
            Console.WriteLine("hello EMC world");
            //1. Webapplication
            //2. Sitecollection '/' - root Web '/' - SITE
            //3. Webs - Web
            Console.WriteLine(GetTitle("https://contoso005.sharepoint.com/"));
            Console.WriteLine("getting all the lists");

            ListCollection Mylist = GetLibrary("https://contoso005.sharepoint.com/");
            foreach (List Lst in Mylist)
            {
                if(Lst.Hidden != true)
                { Console.WriteLine(Lst.Title); }
                
            }


            Console.ReadLine();
        }
        public static string GetTitle(string SiteUrl)
        {
            string SiteTitle = "";
            //sharepoint specific code

            //1. create clientcontext for your site
            ClientContext RakeshSiteClientContext = new ClientContext(SiteUrl);
            
            //set the creds
            RakeshSiteClientContext.Credentials = new SharePointOnlineCredentials("admin@contoso005.onmicrosoft.com", CovertToSecureString("Welcome@1234"));

            //Define your data what you are intrested in 
            Web RakeshWeb = RakeshSiteClientContext.Web;

            //load whatever u want to get from server
            RakeshSiteClientContext.Load(RakeshWeb,web=>web.Title);
            
            //actually go to the server
            RakeshSiteClientContext.ExecuteQuery();

            SiteTitle = RakeshWeb.Title;
            
            return SiteTitle;
        }

        public static ListCollection GetLibrary(string SiteUrl)
        {
            List<string> AllLibrary = new List<string>();
            //sharepoint specific code

            //1. create clientcontext for your site
            ClientContext RakeshSiteClientContext = new ClientContext(SiteUrl);

            //set the creds
            RakeshSiteClientContext.Credentials = new SharePointOnlineCredentials("admin@contoso005.onmicrosoft.com", CovertToSecureString("Welcome@1234"));

            //Define your data what you are intrested in 
            Web RakeshWeb = RakeshSiteClientContext.Web;
            
            // get all the lists
            ListCollection AllList = RakeshWeb.Lists;
            
            //load whatever u want to get from server
            RakeshSiteClientContext.Load(AllList,lst=>lst.Include(l=> l.Hidden, l=>l.Title));

            //actually go to the server
            RakeshSiteClientContext.ExecuteQuery();

            return AllList;
            
        }

        private static SecureString CovertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }
    }
}
