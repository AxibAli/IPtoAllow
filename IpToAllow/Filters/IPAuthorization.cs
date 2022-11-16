using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            string IpAddress = GetLocalIPAddress();

            IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

            List<string> ip_addresses = _config.GetSection("IsAllowed:IpAddresses").Get<List<string>>();
            bool res = ip_addresses.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();

            if(!res)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Error"},{"action","Index"} });
            }

            return res;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

  


    //public class IpWork
    //{ 
    //    public static string GetLocalIPAddress()
    //    {
    //        var host = Dns.GetHostEntry(Dns.GetHostName());
    //        foreach (var ip in host.AddressList)
    //        {
    //            if (ip.AddressFamily == AddressFamily.InterNetwork)
    //            {
    //                return ip.ToString();
    //            }
    //        }
    //        throw new Exception("No network adapters with an IPv4 address in the system!");
    //    }

    //    public static bool checkIpAddressIsValid(ActionExecutedContext filterContext)
    //    {
    //        IConfigurationRoot _config = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    //        .AddJsonFile("appsettings.json", false)
    //        .Build();

    //        List<string> ip_addresses = _config.GetSection("IsAllowed:IpAddresses").Get<List<string>>();
    //        bool res = ip_addresses.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();

    //        return res;
    //    }
    //}
}
