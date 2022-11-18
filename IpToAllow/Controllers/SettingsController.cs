﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using IpToAllow.Models;

namespace IpToAllow.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            PropertyConfigurationModel pcm = new PropertyConfigurationModel
            {
                Delimeter = ',',
                IsList = true,
                IsRequired = true,
                Value = "192.168.1.133",
                ValueType = ValueCollectionType.Normal,
                XmlLabel = "dummy"
            };

            PropertyConfigurationModel pcm1 = new PropertyConfigurationModel
            {
                Delimeter = ',',
                IsList = true,
                IsRequired = true,
                Value = "192.168.1.183",
                ValueType = ValueCollectionType.Normal,
                XmlLabel = "dummy2"
            };

            PropertyConfigurationModel pcm2 = new PropertyConfigurationModel
            {
                Delimeter = ',',
                IsList = true,
                IsRequired = true,
                Value = "192.168.1.183",
                ValueType = ValueCollectionType.Normal,
                XmlLabel = "dummy3"
            };

            PropertyConfigurationModel[] pm_arr = new PropertyConfigurationModel[3];
            pm_arr[0] = pcm;
            pm_arr[1] = pcm1;
            pm_arr[2] = pcm2;

            return View(pm_arr);
        }
    }
}
