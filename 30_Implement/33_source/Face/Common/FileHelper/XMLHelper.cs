using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileHelper
{
    public class XMLHelper
    {
       public static string FileName { get; set; }

        public XMLHelper(string fileName)
        {
            FileName = fileName;
        }
     
        // cái này ok
        public void Read(out string result, string key)
        {
            ExeConfigurationFileMap customConfigFileMap = new ExeConfigurationFileMap();
            customConfigFileMap.ExeConfigFilename = FileName;
            Configuration customConfig = ConfigurationManager.OpenMappedExeConfiguration(customConfigFileMap, ConfigurationUserLevel.None);
            AppSettingsSection appSettings = (customConfig.GetSection("appSettings") as AppSettingsSection);
            result = appSettings.Settings[key].Value;
        }
        public static string Read(string key, string fileName)
        {
            ExeConfigurationFileMap customConfigFileMap = new ExeConfigurationFileMap();
            customConfigFileMap.ExeConfigFilename = FileName;
            Configuration customConfig = ConfigurationManager.OpenMappedExeConfiguration(customConfigFileMap, ConfigurationUserLevel.None);
            AppSettingsSection appSettings = (customConfig.GetSection("appSettings") as AppSettingsSection);
            return appSettings.Settings[key].Value;
        }
    }
   
}
