using Lagrange.Core;
using Lagrange.Core.Common;
using Lagrange.Core.Common.Interface;

namespace QQBot
{
    internal class Program
    {
        static async Task Main(string[] args)  // 注意：从 async void 改为 async Task
        {
            // 1. 构建设备信息
            var deviceInfo = new BotDeviceInfo
            {
                Guid = Guid.NewGuid(),
                MacAddress = GenRandomBytes(6),  // 6 位随机字节
                DeviceName = "Lagrange-Console",
                SystemKernel = "Windows 10.0.19042",
                KernelVersion = "10.0.19042.0"
            };

            // 2. 创建 BotConfig（使用默认 Linux 协议）
            var config = new BotConfig();

            // 3. 创建 KeyStore（首次为空）
            var keyStore = new BotKeyStore();

            // 4. 创建 Bot 实例
            var bot = BotFactory.Create(config, deviceInfo, keyStore);

            // 5. 获取二维码并保存
            Console.WriteLine("正在获取二维码...");
            var qrCode = await bot.FetchQrCode();

            if (qrCode == null)
            {
                Console.WriteLine("获取二维码失败，请检查网络或签名服务配置。");
                return;
            }

            // 保存二维码图片
            var qrPath = Path.Combine(AppContext.BaseDirectory, "qrcode.png");
            await File.WriteAllBytesAsync(qrPath, qrCode.Value.QrCode);
            Console.WriteLine($"二维码已保存到: {qrPath}");
            Console.WriteLine("请用手机 QQ 扫描该二维码登录...");

            // 6. 等待扫码登录（此方法会阻塞直到扫码成功或超时）
            await bot.LoginByQrCode();

            // 7. 登录成功，保存 KeyStore 用于下次免扫码
            keyStore = bot.UpdateKeystore();
            Console.WriteLine($"登录成功！BotUin: {bot.BotUin}");

            // 8. 测试：打印已加入的群数量（如果 API 存在）
            // 注意：具体获取群列表的方法需查阅实际 API，此处仅为示意
            Console.WriteLine("登录完成，按任意键退出...");
            Console.ReadKey();
        }

        // 生成指定长度的随机字节数组（用于模拟 MAC 地址）
        private static byte[] GenRandomBytes(int length)
        {
            var bytes = new byte[length];
            Random.Shared.NextBytes(bytes);
            return bytes;
        }
    }
}