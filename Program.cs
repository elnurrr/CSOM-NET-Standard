using Microsoft.SharePoint.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleApp_CSOM_NETStandard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Uri site = new Uri("https://contoso.sharepoint.com/Sites/dev01");
            string clientId = "ae777e7c-a1e0-45a4-a15a-cf53d6c3152f";
            string clientSecret = "AJh0C8cJd4-K83JCDK***";

            // Note: The PnP Sites Core AuthenticationManager class also supports this
            using (var authenticationManager = new AuthenticationManager())
            {
                using (var context = authenticationManager.GetContext(site, clientId, clientSecret))
                {
                    context.Load(context.Web, p => p.Title);
                    await context.ExecuteQueryAsync();
                    Console.WriteLine($"Title: {context.Web.Title}");

                    var list = context.Web.Lists.GetByTitle("Links");
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem newItem = list.AddItem(itemCreateInfo);
                    newItem["Title"] = "My New Item!";
                    newItem.Update();
                    await context.ExecuteQueryAsync();
                    Console.WriteLine(newItem.Id);
                }
            }
        }
    }
}
