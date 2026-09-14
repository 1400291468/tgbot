using TGForm.Common;
using TGForm.Models;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace TGForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DbHelper.Db.CodeFirst.InitTables(typeof(vpns));
                DbHelper.Db.CodeFirst.InitTables(typeof(settings));
                DbHelper.Db.CodeFirst.InitTables(typeof(account_check_log));
                DbHelper.Db.CodeFirst.InitTables(typeof(collect_group_username));
                DbHelper.Db.CodeFirst.InitTables(typeof(collect_group_member_username));
                DbHelper.Db.CodeFirst.InitTables(typeof(webbrowser_accounts));
                DbHelper.Db.CodeFirst.InitTables(typeof(webbrowser_collect_log));
                DbHelper.Db.CodeFirst.InitTables(typeof(webbrowser_collect_username));
                DbHelper.Db.CodeFirst.InitTables(typeof(session_send_log));
                DbHelper.Db.CodeFirst.InitTables(typeof(webbrowser_collect_group_log));

                EnsureCollectedUsernameUniqueness();

                //Logger.Log("数据库表初始化完成！");

                var setting = DbHelper.Db.Queryable<Models.settings>().First();
                if (setting == null || string.IsNullOrEmpty(setting.activation_code))
                {
                    // 1. 先显示登录窗体（作为启动窗体）
                    using (var loginForm = new LoginForm())
                    {
                        // 如果登录窗体返回 DialogResult.OK，说明登录成功
                        if (loginForm.ShowDialog() == DialogResult.OK)
                        {
                            // 2. 登录成功后，启动主窗体作为应用程序的主入口
                            Application.Run(new MainForm());
                        }
                        else
                        {
                            // 登录失败或取消，退出程序
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    var result = Common.Tools.Instance.activaionCode(setting.activation_code).Result;
                    if (result.Status)
                    {
                        using (var loginForm = new LoginForm())
                        {
                            // 如果登录窗体返回 DialogResult.OK，说明登录成功
                            if (loginForm.ShowDialog() == DialogResult.OK)
                            {
                                // 2. 登录成功后，启动主窗体作为应用程序的主入口
                                Application.Run(new MainForm());
                            }
                            else
                            {
                                // 登录失败或取消，退出程序
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        Application.Run(new MainForm());
                    }
                }


            }
            catch (Exception ex)
            {
                using (var loginForm = new LoginForm())
                {
                    // 如果登录窗体返回 DialogResult.OK，说明登录成功
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // 2. 登录成功后，启动主窗体作为应用程序的主入口
                        Application.Run(new MainForm());
                    }
                    else
                    {
                        // 登录失败或取消，退出程序
                        Application.Exit();
                    }
                }
            }
        }

        private static void EnsureCollectedUsernameUniqueness()
        {
            DbHelper.Db.Ado.ExecuteCommand(@"
                DELETE FROM collect_group_member_username
                WHERE id NOT IN (
                    SELECT MIN(id)
                    FROM collect_group_member_username
                    GROUP BY telegram_user_id
                );");
            DbHelper.Db.Ado.ExecuteCommand(@"
                CREATE UNIQUE INDEX IF NOT EXISTS ux_collect_member_telegram_user_id
                ON collect_group_member_username (telegram_user_id);");
        }
    }
}