using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting.Server;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml;

namespace IpToAllow.Filters
{
    public class IPAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IpWork.CheckIp(filterContext);
        }
    }
    public static class IpWork
    {
        public static bool CheckIp(ActionExecutingContext filterContext)
        {
            // for storing ips
            List<string> ips = new List<string>();

            // for log creation
            LogDetails(Environment.NewLine + DateTime.Now.ToString() + "--" + ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ControllerName + "----" + ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ActionName + "--" + "onActionExecuting");
            
            // get local ip (user ip)
            string IpAddress = GetLocalIPAddress();

            //for reading path from appsettings.json
            IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

            //List<string> ip_addresses = _config.GetSection("IsAllowed:IpAddresses").Get<List<string>>();
            
            //get file path from appsettings.json 
            var path = _config.GetSection("IsAllowed:IpAddresses").Value;
            
            // reading xml external doc (settings.config)
            XmlDocument xd = new XmlDocument();
            xd.Load(path);
            XmlNodeList nodelist = xd.SelectNodes("appSettings");
            foreach (XmlNode node in nodelist) // for each <testcase> node
            {
                ips = node.InnerXml.Split(" ")[2].Substring(7).TrimEnd('"').Split(',').ToList();
            }

            // for comparing user ip with  allowed ips
            bool res = ips.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();

            //bool res = ip_addresses.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();

            //checking result condiotion for route
            if(!res)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Error"},{"action","Index"} });
            }
            return res;
        }

        public static void LogDetails(string LogData)
        {
            File.AppendAllText(@"Logs/Log.txt", LogData);
        }

        public static string GetLocalIPAddress()
        {
            //string Host = Dns.GetHostName();
            //string ip = Dns.GetHostByName(Host).AddressList[4].ToString();
            //return ip;

            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            return localIP;

            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        return ip.ToString();
            //    }
            //}
            //throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}