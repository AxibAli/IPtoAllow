using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using IpToAllow.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace IpToAllow.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IConfiguration _configuration;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<PropertyConfiguration> propertyConfigurations = new List<PropertyConfiguration>();
            string[] ips = _configuration["section:appSetting:key:IpAddresses"]?.ToString().Split(",") ?? new string[] { };

            if (ips.Length > 0)
                foreach (var item in ips)
                {
                    propertyConfigurations.Add(new PropertyConfiguration
                    {
                        Value = item,
                    });
                }

            return View(propertyConfigurations.ToArray());

            #region Hard coded key values
            //PropertyConfigurationModel propertyConfigurationModel = new PropertyConfigurationModel();
            //propertyConfigurationModel.PropertyConfigurationsList = new List<PropertyConfiguration>();

            //string xmlfilepath = Path.Combine(Directory.GetCurrentDirectory(), "Settings", "Settings.config");
            //string xmlstring = System.IO.File.ReadAllText(xmlfilepath);
            //var stringReader = new StringReader(xmlstring);

            //var dsSet = new System.Data.DataSet();

            //dsSet.ReadXml(stringReader);

            //for (int i = 0; i < dsSet.Tables[0].Rows.Count; i++)
            //{
            //    propertyConfigurationModel.PropertyConfigurationsList.Add(new PropertyConfiguration
            //    {
            //        XmlLabel = dsSet.Tables[0].Rows[i][0].ToString(),
            //        Value = dsSet.Tables[0].Rows[i][1].ToString(),
            //        ValueType = (ValueCollectionType)dsSet.Tables[0].Rows[i][2],
            //        Delimeter = (char)dsSet.Tables[0].Rows[i][3],

            //    });
            //}

            //PropertyConfigurationModel pcm = new PropertyConfigurationModel
            //{
            //    Delimeter = ',',
            //    IsList = true,
            //    IsRequired = true,
            //    Value = "192.168.1.133",
            //    ValueType = ValueCollectionType.Normal,
            //    XmlLabel = "dummy"
            //};

            //PropertyConfigurationModel pcm1 = new PropertyConfigurationModel
            //{
            //    Delimeter = ',',
            //    IsList = true,
            //    IsRequired = true,
            //    Value = "192.168.1.183",
            //    ValueType = ValueCollectionType.Normal,
            //    XmlLabel = "dummy2"
            //};

            //PropertyConfigurationModel pcm2 = new PropertyConfigurationModel
            //{
            //    Delimeter = ',',
            //    IsList = true,
            //    IsRequired = true,
            //    Value = "192.168.1.183",
            //    ValueType = ValueCollectionType.Normal,
            //    XmlLabel = "dummy3"
            //};

            //PropertyConfigurationModel[] pm_arr = new PropertyConfigurationModel[3];
            //pm_arr[0] = pcm;
            //pm_arr[1] = pcm1;
            //pm_arr[2] = pcm2;

            #endregion
        }
    }
}
