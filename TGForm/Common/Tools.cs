using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Common
{
    public class Tools
    {
        public static Tools Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tools();
                }
                return instance;
            }
        }
        private static Tools instance = null;

        public int GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        public DateTime TimestampToDateTime(long timestamp)
        {
            // 1970-01-01 00:00:00
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // 加上秒数
            return start.AddSeconds(timestamp).ToLocalTime();
        }

        public string GetDefaultPath()
        {
            string path = Path.Combine(Application.StartupPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string GetWindowsDeviceId()
        {
            const string regPath = @"SOFTWARE\Microsoft\Cryptography";
            const string regValue = "MachineGuid";
            try
            {
                // 使用 Registry64 视图，避免 64 位系统下读取失败
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (var key = hklm.OpenSubKey(regPath))
                {
                    if (key != null)
                    {
                        var value = key.GetValue(regValue);
                        if (value != null)
                            return value.ToString().ToUpper();
                    }
                }
            }
            catch
            {
                // 忽略异常，返回 Unknown
            }
            return "Unknown";
        }

        public async Task<(bool Status, string Message)> activaionCode(string code)
        {
            var systemDeviceId = Common.Tools.Instance.GetWindowsDeviceId();

            if (systemDeviceId == "Unknown")
            {
                return (false, "获取设备标识异常");
            }

            var result = await LicenseApi.GetStatusAsync(code, systemDeviceId);


            //await DbHelper.Db.Updateable<Models.settings>().SetColumns(it => it.activation_code == code).Where(it => 1 == 1).ExecuteCommandAsync();
            return (true, "激活成功");
        }
    }
}
