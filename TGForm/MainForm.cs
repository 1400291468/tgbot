using Dm;
using Dm.util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V150.FileSystem;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SqlSugar;
using Starksoft.Net.Proxy;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using TGForm.Common;
using TGForm.Models;
using TL;
using WTelegram;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;
using File = System.IO.File;
using Image = System.Drawing.Image;

namespace TGForm
{
    public partial class MainForm : Form
    {
        private ContextMenuStrip _contextMenu;  // vpn右键菜单
        private ContextMenuStrip _contextCollectTaskMenu;  // 采集任务右键菜单
        private ContextMenuStrip _contextWebwoserAccountMenu;  // web账号右键菜单
        private ContextMenuStrip _contexSessionAccountMenu;//session账号右键菜单
        private ContextMenuStrip _contexCollectUserNameMenu;//已采集会员列表右键菜单
        private ContextMenuStrip _contexWebCollectTaskMenu;//web版本采集任务右键菜单
        private int _currentPage = 1;
        private int _currentAccountPage = 1;
        private int _currentCollectTaskPage = 1;
        private int _currentWebwoserPage = 1;
        private int _currentCollectUserNamePage = 1;
        private int _currentWebCollectGroupPage = 1;




        private int _pageSize = 10;
        private readonly ImageList _collectUserNameImages = new()
        {
            ImageSize = new Size(40, 40),
            ColorDepth = ColorDepth.Depth32Bit
        };
        private readonly ImageList _collectTaskRowHeightImages = new()
        {
            ImageSize = new Size(1, 40),
            ColorDepth = ColorDepth.Depth32Bit
        };
        private List<string> _allSessionFiles = new List<string>();
        private string _filePath = Application.StartupPath + @$"\session";
        private CancellationTokenSource? _ctsSessionCollect;
        private bool _isRunningSessionCollect;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = new Icon("icons/2j92t-xs66w-001.ico");
            _collectTaskRowHeightImages.Images.Add(new Bitmap(1, 40));
            Logger.Initialize(richTextBoxLog, maxLines: 2000);
            this.StartPosition = FormStartPosition.CenterScreen;
            SetupContextMenu();  // 初始化右键菜单
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {


            await Init();
            await LoadSettings();




        }


        #region 初始化
        // ========== 初始化 ==========
        async Task Init()
        {
            try
            {



                var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
                if (setting == null)
                {
                    setting = new settings
                    {
                        api_id = "",
                        api_hash = "",
                        default_vpn_id = 0,
                        default_account_path = _filePath,
                        session_collect_avatar_enabled = false,
                        session_collect_filter_bot_enabled = false,
                        session_collect_filter_manager_enabled = false,
                        session_collect_info_filter_enabled = false,
                        session_collect_info_filter_context = "",
                        session_collect_nick_filter_context = "",
                        session_collect_nick_filter_enabled = false,
                        session_send_context = "",
                        web_send_context = "",
                        web_collect_info_filter_context = "",
                        web_collect_nick_filter_context = "",
                        web_collect_info_filter_enabled = false,
                        web_collect_nick_filter_enabled = false,

                    };
                    var res = await DbHelper.Db.Insertable(setting).ExecuteCommandAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(setting.default_account_path))
                    {

                        setting.default_account_path = _filePath;
                    }
                    var res = await DbHelper.Db.Updateable(setting).ExecuteCommandAsync();
                }



                _filePath = setting.default_account_path;

                LoadTitle();
                await LoadVpns();
                await LoadAccounts();
                await LoadCollectTask();
                await LoadWebwoserAccount();
                await LoadCollectUserName();
                await LoadWebCollectGroup();

            }
            catch (Exception ex)
            {
                string methodName = MethodBase.GetCurrentMethod().Name;
                Logger.Log($"函数名=>{methodName}()", Common.LogLevel.Error);
                Logger.Log($"报错信息=>{ex.Message}", Common.LogLevel.Error);
            }
        }



        // ========== 加载列标题 ==========
        private void LoadTitle()
        {
            var dom = lvVpnList;
            dom.CheckBoxes = true;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;

            // 总宽度 1074，减去滚动条预留 20px ≈ 1054
            dom.Columns.Add("ID", 50, HorizontalAlignment.Center);           // 50
            dom.Columns.Add("主机记录", 140, HorizontalAlignment.Center);    // 140
            dom.Columns.Add("端口", 70, HorizontalAlignment.Center);         // 70
            dom.Columns.Add("用户名", 120, HorizontalAlignment.Center);      // 120
            dom.Columns.Add("密码", 120, HorizontalAlignment.Center);        // 120
            dom.Columns.Add("备注", 180, HorizontalAlignment.Center);        // 180
            dom.Columns.Add("分组", 100, HorizontalAlignment.Center);        // 100
            dom.Columns.Add("创建时间", 140, HorizontalAlignment.Center);    // 140
            dom.Columns.Add("状态", 80, HorizontalAlignment.Center);         // 80


            dom.Columns[8].Width = -2;  // -2 表示自动填满剩余宽度

            dom = lvAccounts;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;
            dom.CheckBoxes = false; // 根据需求设置

            // ⭐ 设置列宽
            dom.Columns.Add("手机号", 200, HorizontalAlignment.Center);
            dom.Columns.Add("文件最后访问时间", 200, HorizontalAlignment.Center);
            dom.Columns.Add("最后检查登录时间", 200, HorizontalAlignment.Center);
            dom.Columns.Add("状态", 100, HorizontalAlignment.Center);
            dom.Columns[3].Width = -2; // 状态列自动填满剩余宽度

            dom = lvCollect;
            dom.CheckBoxes = true;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;
            dom.SmallImageList = _collectTaskRowHeightImages;
            dom.Columns.Add("Id", 60, HorizontalAlignment.Center);
            dom.Columns.Add("群链接", 100, HorizontalAlignment.Center);
            dom.Columns.Add("Group_Id", 100, HorizontalAlignment.Center);
            dom.Columns.Add("采集手机号", 140, HorizontalAlignment.Center);
            //dom.Columns.Add("群人数", 100, HorizontalAlignment.Center);
            dom.Columns.Add("采集备注", 200, HorizontalAlignment.Center);
            dom.Columns.Add("状态", 100, HorizontalAlignment.Center);
            dom.Columns.Add("创建时间", 150, HorizontalAlignment.Center);
            dom.Columns.Add("采集时间", 150, HorizontalAlignment.Center);
            //dom.Columns.Add("采集备注", 200, HorizontalAlignment.Center);
            //dom.Columns.Add("废弃字段", 100, HorizontalAlignment.Center);

            dom.Columns[7].Width = -2; // 状态列自动填满剩余宽度


            dom = lsWebwoserAccounts;
            dom.CheckBoxes = true;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;

            dom.Columns.Add("Id", 100, HorizontalAlignment.Center);
            dom.Columns.Add("手机号", 200, HorizontalAlignment.Center);
            dom.Columns.Add("登录时间", 200, HorizontalAlignment.Center);
            dom.Columns.Add("登录缓存", 200, HorizontalAlignment.Center);
            dom.Columns[3].Width = -2; // 状态列自动填满剩余宽度






            dom = lsCollectUserName;
            dom.CheckBoxes = true;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;

            dom.SmallImageList = _collectUserNameImages;
            dom.Columns.Add("会员头像", 60, HorizontalAlignment.Center);
            dom.Columns.Add("Id", 100, HorizontalAlignment.Center);
            dom.Columns.Add("用户名", 100, HorizontalAlignment.Center);
            dom.Columns.Add("会员昵称", 100, HorizontalAlignment.Center);
            dom.Columns.Add("采集时间", 100, HorizontalAlignment.Center);
            dom.Columns.Add("状态", 100, HorizontalAlignment.Center);
            dom.Columns.Add("备注", 100, HorizontalAlignment.Center);
            dom.Columns.Add("群发时间", 100, HorizontalAlignment.Center);
            dom.Columns.Add("TgUserId", 100, HorizontalAlignment.Center);
            dom.Resize += (_, _) => FillCollectUserNameLastColumn();
            dom.ColumnWidthChanged += CollectUserName_ColumnWidthChanged;
            FillCollectUserNameLastColumn();


            dom = lsWebCollect;
            dom.CheckBoxes = true;
            dom.View = View.Details;
            dom.FullRowSelect = true;
            dom.GridLines = true;

            dom.Columns.Add("Id", 100, HorizontalAlignment.Center);
            dom.Columns.Add("群链接", 100, HorizontalAlignment.Center);
            dom.Columns.Add("状态", 100, HorizontalAlignment.Center);
            dom.Columns.Add("创建时间", 150, HorizontalAlignment.Center);
            dom.Columns.Add("采集时间", 150, HorizontalAlignment.Center);
            dom.Columns.Add("群人数", 100, HorizontalAlignment.Center);
            dom.Columns.Add("采集成功人数", 100, HorizontalAlignment.Center);
            dom.Columns.Add("任务消息", 200, HorizontalAlignment.Center);
            dom.Columns[7].Width = -2; // 状态列自动填满剩余宽度

        }

        private void CollectUserName_ColumnWidthChanged(object? sender, ColumnWidthChangedEventArgs e)
        {
            if (e.ColumnIndex < lsCollectUserName.Columns.Count - 1)
            {
                FillCollectUserNameLastColumn();
            }
        }

        private void FillCollectUserNameLastColumn()
        {
            const int minimumWidth = 100;
            var columns = lsCollectUserName.Columns;
            if (columns.Count == 0)
            {
                return;
            }

            var lastIndex = columns.Count - 1;
            var occupiedWidth = columns
                .Cast<ColumnHeader>()
                .Take(lastIndex)
                .Sum(column => column.Width);
            var availableWidth = lsCollectUserName.ClientSize.Width
                - occupiedWidth
                - SystemInformation.VerticalScrollBarWidth;
            var targetWidth = Math.Max(minimumWidth, availableWidth);

            if (columns[lastIndex].Width != targetWidth)
            {
                columns[lastIndex].Width = targetWidth;
            }
        }

        private static bool TryCreateAvatarImage(byte[]? bytes, out Image image)
        {
            image = null!;
            if (bytes is not { Length: > 0 })
            {
                return false;
            }

            try
            {
                using var stream = new MemoryStream(bytes);
                using var source = Image.FromStream(stream);
                image = new Bitmap(source);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        async Task LoadCollectUserName()
        {
            if (cmbMemberP.Items.Count == 0)
            {
                cmbMemberP.Items.Clear();
                cmbMemberP.Items.Add("全部");
                cmbMemberP.Items.Add("web采集");
                cmbMemberP.Items.Add("协议采集");
                cmbMemberP.SelectedIndex = 0;
            }
            if (cmbMemberOrderBy.Items.Count == 0)
            {
                cmbMemberOrderBy.Items.Clear();
                cmbMemberOrderBy.Items.Add("倒叙");
                cmbMemberOrderBy.Items.Add("正序");
                cmbMemberOrderBy.SelectedIndex = 0;
            }
            if (cmbMemberSelectSendStatus.Items.Count == 0)
            {
                cmbMemberSelectSendStatus.Items.Clear();
                cmbMemberSelectSendStatus.Items.Add("全部状态");
                cmbMemberSelectSendStatus.Items.Add("发送成功");
                cmbMemberSelectSendStatus.Items.Add("发送失败");
                cmbMemberSelectSendStatus.Items.Add("待发送");
                cmbMemberSelectSendStatus.Items.Add("采集到机器人");
                cmbMemberSelectSendStatus.Items.Add("采集到管理员");
                cmbMemberSelectSendStatus.Items.Add("采集到非法简介");
                cmbMemberSelectSendStatus.Items.Add("采集到非法昵称");
                cmbMemberSelectSendStatus.SelectedIndex = 0;
            }


            var dom = lsCollectUserName;
            var page = _currentCollectUserNamePage;
            var pageSize = _pageSize * 100;

            // 查询分页数据
            var data = DbHelper.Db.Queryable<collect_group_member_username>();
            if (txtCollectUserNameSeach.Text.Trim().Length > 0)
            {
                data.Where(it => it.username.Contains(txtCollectUserNameSeach.Text.Trim()));
            }

            if (cmbMemberP.SelectedIndex != 0)
            {
                data.Where(it => it.SourceType == cmbMemberP.SelectedIndex);
            }
            if (cmbMemberSelectSendStatus.SelectedIndex != 0)
            {
                var status = 0;
                var index = cmbMemberSelectSendStatus.SelectedIndex;
                switch (index)
                {
                    case 1:
                        {
                            status = 1; break;
                        }
                    case 2:
                        {
                            status = -1; break;
                        }
                    case 3:
                        {
                            status = 0; break;
                        }
                    case 4:
                        {
                            status = -100; break;
                        }
                    case 5:
                        {
                            status = -99; break;
                        }
                    case 6:
                        {
                            status = -3; break;
                        }
                    case 7:
                        {
                            status = -2; break;
                        }
                    default:
                        break;
                }
                data.Where(it => it.status == status);
            }
            var idSort = "id desc";
            if (cmbMemberOrderBy.SelectedIndex != 0)
            {
                idSort = "id asc";
            }


            var list = await data.OrderBy(idSort).ToPageListAsync(page, pageSize);



            // 获取总数
            var total = await DbHelper.Db.Queryable<collect_group_member_username>().CountAsync();

            dom.BeginUpdate();
            dom.Items.Clear();
            _collectUserNameImages.Images.Clear();

            foreach (var item in list)
            {
                var imageKey = item.telegram_user_id.ToString();
                if (TryCreateAvatarImage(item.head_image, out var avatar))
                {
                    _collectUserNameImages.Images.Add(imageKey, avatar);
                }

                var row = new ListViewItem
                {
                    ImageKey = _collectUserNameImages.Images.ContainsKey(imageKey) ? imageKey : string.Empty
                };

                row.SubItems.Add(item.id.ToString());
                row.SubItems.Add(item.username);
                row.SubItems.Add($"{item.first_name}{item.last_name}");
                row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(item.create_time).ToString());

                var statusText = "";
                switch (item.status)
                {
                    case 0:
                        {
                            statusText = "待发送"; break;
                        }
                    case 1:
                        {
                            statusText = "发送成功"; break;
                        }
                    case -1:
                        {
                            statusText = "发送失败"; break;
                        }
                    case -2:
                        {
                            statusText = "非法昵称"; break;
                        }
                    case -3:
                        {
                            statusText = "非法简介"; break;
                        }
                    case -100:
                        {
                            statusText = "机器人账户"; break;
                        }
                    case -99:
                        {
                            statusText = "管理员账号"; break;
                        }
                    default:
                        statusText = "未知状态"; break;
                        break;
                }

                row.SubItems.Add(statusText);
                row.SubItems.Add(item.msg);
                row.SubItems.Add(item.send_time == 0 ? "" : Common.Tools.Instance.TimestampToDateTime(item.send_time).ToString());
                row.SubItems.Add(item.telegram_user_id.ToString());
                row.Tag = item;

                dom.Items.Add(row);
            }

            dom.EndUpdate();

            // 更新分页信息
            int totalPages = (int)Math.Ceiling((double)total / pageSize);
            lbCollectUserNameShowCount.Text = $"当前页码 {page} 本页数据总数 {list.Count()} 条 数据总数 {total} 条";
            lbCollectUsername.Text = $"{page}/{totalPages}";
            btnCollectUserNameUp.Enabled = page > 1;
            btnCollectUserNameDown.Enabled = page < totalPages;
        }

        private void lsCollectUserName_ItemActivate(object sender, EventArgs e)
        {
            if (lsCollectUserName.SelectedItems.Count == 0) return;
            ListViewItem item = lsCollectUserName.SelectedItems[0];
            MessageBox.Show($"双击第 {item.Index} 项：{item.Text}");
        }

        // ========== 加载 VPN 数据（分页） ==========
        async Task LoadVpns()
        {
            var dom = lvVpnList;
            var page = _currentPage;

            // 查询分页数据
            var data = DbHelper.Db.Queryable<vpns>();
            if (txtHostSeach.Text.Trim().Length > 0)
            {
                data.Where(it => it.host.Contains(txtHostSeach.Text.Trim()));
            }
            if (txtRemarkSeach.Text.Trim().Length > 0)
            {
                data.Where(it => it.remark.Contains(txtRemarkSeach.Text.Trim()));
            }
            var list = await data.OrderBy(it => it.id, OrderByType.Desc)
                                       .ToPageListAsync(page, _pageSize);

            // 获取总数
            var total = await DbHelper.Db.Queryable<vpns>().CountAsync();

            dom.BeginUpdate();
            dom.Items.Clear();

            foreach (var item in list)
            {
                var row = new ListViewItem(item.id.ToString());
                row.SubItems.Add(item.host);
                row.SubItems.Add(item.port);
                row.SubItems.Add(item.username);
                row.SubItems.Add(item.password);
                row.SubItems.Add(item.remark);
                row.SubItems.Add(item.group);
                row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(item.create_time).ToString());
                row.SubItems.Add(item.Status ? "启用中" : "禁用中");

                // 保存数据对象到 Tag
                row.Tag = item;

                dom.Items.Add(row);
            }

            dom.EndUpdate();

            // 更新分页信息
            int totalPages = (int)Math.Ceiling((double)total / _pageSize);
            lblVpnListPage.Text = $"{_currentPage}/{totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        async Task LoadAccounts()
        {
            var dom = lvAccounts;
            dom.Items.Clear();

            // 获取当前页数据
            var currentPageFiles = GetCurrentPageFiles();

            if (currentPageFiles.Count == 0)
            {
                Logger.Log("没有找到任何账号文件！", Common.LogLevel.Warning);
                return;
            }

            var list = await DbHelper.Db.Queryable<account_check_log>().ToListAsync();
            // 添加到列表
            foreach (string file in currentPageFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                DateTime lastAccess = System.IO.File.GetLastAccessTime(file);

                var account = list.Where(it => it.account == fileName).OrderByDescending(it => it.id).FirstOrDefault();


                var row = new ListViewItem(fileName);
                row.SubItems.Add(lastAccess.ToString());

                if (account == null)
                {
                    row.SubItems.Add("暂无记录");
                    row.SubItems.Add("未知");

                }
                else
                {
                    var status = account.status;
                    row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(account.time).ToString());
                    row.SubItems.Add(account.msg);
                }
                dom.Items.Add(row);
            }

            // 更新页码信息
            int totalPages = GetTotalPages();
            lbAccount.Text = $"{_currentAccountPage}/{totalPages}";

            // 更新按钮状态
            btnAccountUp.Enabled = _currentAccountPage > 1;
            btcAccountNext.Enabled = _currentAccountPage < totalPages;
        }



        async Task LoadCollectTask()
        {
            var dom = lvCollect;
            var page = _currentCollectTaskPage;
            var pageSize = _pageSize * 100;

            // 查询分页数据
            var list = await DbHelper.Db.Queryable<collect_group_username>().Where(it => it.username.Contains(txtCollectTaskSeach.Text.Trim())).OrderByDescending(it => it.id).ToPageListAsync(page, pageSize); ;


            var total = await DbHelper.Db.Queryable<collect_group_username>().CountAsync();

            dom.BeginUpdate();
            dom.Items.Clear();

            foreach (var item in list)
            {


                var row = new ListViewItem(item.id.ToString())
                {
                    ImageIndex = 0
                };
                row.SubItems.Add(item.username.ToString());
                row.SubItems.Add(item.group_id.ToString());
                row.SubItems.Add(item.collect_phone == null ? "" : item.collect_phone.ToString());
                row.SubItems.Add(item.msg);
                row.SubItems.Add(item.status == 0 ? "待采集" : item.status == 1 ? "采集中" : item.status == 2 ? "已采集" : "采集失败");
                row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(item.create_time).ToString());
                row.SubItems.Add(item.collect_time == 0 ? "暂无记录" : Common.Tools.Instance.TimestampToDateTime(item.collect_time).ToString());


                // 保存数据对象到 Tag
                row.Tag = item;

                dom.Items.Add(row);
            }


            dom.EndUpdate();

            // 更新分页信息
            int totalPages = (int)Math.Ceiling((double)total / pageSize);
            lbCollectTaskTxt.Text = $"{page}/{totalPages}";
            btnCollectTaskPrev.Enabled = _currentPage > 1;

            btnCollectTaskNext.Enabled = _currentPage < totalPages;
        }

        private List<string> GetCurrentPageFiles(bool isAll = false, string fileAddress = "")
        {
            var path = string.IsNullOrEmpty(fileAddress) ? _filePath : fileAddress;
            if (!System.IO.Directory.Exists(path))
            {
                return new List<string>();
            }

            // 获取所有文件
            _allSessionFiles = System.IO.Directory.GetFiles(path).Where(it => it.Contains(txtAccountSeach.Text))
                                        .OrderBy(f => f) // 排序，保证顺序一致
                                        .ToList();
            if (isAll)
            {
                return _allSessionFiles;
            }
            // 计算跳过的数量
            int skipCount = (_currentAccountPage - 1) * _pageSize;

            // 返回当前页的数据
            return _allSessionFiles
                .Skip(skipCount)
                .Take(_pageSize)
                .ToList();
        }

        // 获取总页数
        private int GetTotalPages()
        {
            if (_allSessionFiles.Count == 0)
                return 0;

            return (int)Math.Ceiling((double)_allSessionFiles.Count / _pageSize);
        }
        async Task LoadSettings()
        {
            var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
            if (setting == null)
            {
                return;
            }

            this.txtApiId.Text = setting.api_id;
            this.txtApiHash.Text = setting.api_hash;


            //vpn设置
            var vList = await DbHelper.Db.Queryable<Models.vpns>().Where(it => it.Status).ToListAsync();

            var dom = cboSettingVpn;
            if (dom.Items.Count == 0)
            {
                dom.DropDownStyle = ComboBoxStyle.DropDownList;
                dom.Items.Add("本机网络");
                foreach (var item in vList)
                {
                    dom.Items.Add(item.host + ":" + item.port);
                }
                if (setting.default_vpn_id == 0)
                {
                    dom.SelectedIndex = 0;
                }
                else
                {
                    int index = vList.FindIndex(f => f.id == setting.default_vpn_id);
                    if (index >= 0)
                    {
                        dom.SelectedIndex = index + 1;
                    }
                }
            }


            this.txtWebGroupManagerSendMax.Text = setting.web_send_max.ToString();
            this.txtWebGroupManagerSendMin.Text = setting.web_send_min.ToString();
            this.txtWebGroupManagerSendMaxCount.Text = setting.web_send_max_count.ToString();
            this.txtWebManagerSendContext.Text = setting.web_send_context;


            this.txtSessionSendMsgContexts.Text = setting.session_send_context;
            this.txtSessionSendMsgCountMax.Text = setting.session_send_count.ToString();
            this.txtSessionSendMsgDelayMaxs.Text = setting.session_send_max.ToString();
            this.txtSessionSendMsgDelayMins.Text = setting.session_send_min.ToString();



            //协议采集设置
            this.cboxSessionCollectInfoFilter.Checked = setting.session_collect_info_filter_enabled;
            this.cboxSessionCollectNickFilter.Checked = setting.session_collect_nick_filter_enabled;

            this.txtSessionCollectInfoFilter.Text = setting.session_collect_info_filter_context;
            this.txtSessionCollectNickFilter.Text = setting.session_collect_nick_filter_context;

            this.cboxSessionSetingDownloadAvatar.Checked = setting.session_collect_avatar_enabled;

            this.cboxSessionCollectFilterBot.Checked = setting.session_collect_filter_bot_enabled;
            this.cboxSessionCollectFilterManager.Checked = setting.session_collect_filter_manager_enabled;


            //web采集设置
            this.cboxWebCollectInfoFilter.Checked = setting.web_collect_info_filter_enabled;
            this.cboxWebCollectNickFilter.Checked = setting.web_collect_nick_filter_enabled;

            this.txtWebCollectInfoFilter.Text = setting.web_collect_info_filter_context;
            this.txtWebCollectNickFilter.Text = setting.web_collect_nick_filter_context;



        }

        // ========== 初始化右键菜单 ==========
        public class SessionData
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        private void SetupContextMenu()
        {
            _contextMenu = new ContextMenuStrip();

            var menuEdit = new ToolStripMenuItem("编辑", null, MenuEdit_Click);
            var menuDelete = new ToolStripMenuItem("删除", null, MenuDelete_Click);
            var menuToggle = new ToolStripMenuItem("启用/禁用", null, MenuToggle_Click);
            var menuSeparator = new ToolStripSeparator();
            var menuRefresh = new ToolStripMenuItem("刷新", null, MenuRefresh_Click);

            _contextMenu.Items.AddRange(new ToolStripItem[] {
                menuEdit,
                menuDelete,
                menuToggle,
                menuSeparator,
                menuRefresh
            });

            lvVpnList.ContextMenuStrip = _contextMenu;

            //采集任务右键菜单
            _contextCollectTaskMenu = new ContextMenuStrip();

            var CollectTaskmenuDelete = new ToolStripMenuItem("删除", null, async (object sender, EventArgs e) =>
            {
                DialogResult result = MessageBox.Show(
    "确定要删除选中的采集任务吗？",    // 消息内容
    "确认操作",                // 标题
    MessageBoxButtons.YesNo,   // 按钮类型
    MessageBoxIcon.Question   // 图标类型
);

                // 判断用户点击了哪个按钮
                if (result != DialogResult.Yes)
                {
                    return;
                }
                foreach (ListViewItem item in lvCollect.SelectedItems)
                {
                    var id = Convert.ToInt32(item.Text);

                    var res = await DbHelper.Db.Deleteable<Models.collect_group_username>().Where(it => it.id == id).ExecuteCommandAsync();

                    Logger.Log($"采集任务删除{(res > 0 ? "成功" : "失败")}：ID={id}");
                }
                await LoadCollectTask();
            });
            var StartCollectTaskmenuDelete = new ToolStripMenuItem("加入采集队列", null, async (object sender, EventArgs e) =>
            {
                var sessionList = GetCurrentPageFiles(true, Path.Combine(Application.StartupPath, "session", "collect"));
                if (sessionList.Count == 0)
                {
                    Logger.Log($"暂无可用账号", Common.LogLevel.Error);
                    return;
                }
                foreach (ListViewItem item in lvCollect.SelectedItems)
                {
                    var id = Convert.ToInt32(item.Text);
                    var data = DbHelper.Db.Queryable<Models.collect_group_username>().First(it => it.id == id);
                    if (data == null)
                    {
                        Logger.Log($"数据库中找不到该数据源", Common.LogLevel.Error);
                        return;
                    }
                    data.status = 1;
                    var res = await DbHelper.Db.Updateable<Models.collect_group_username>(data).ExecuteCommandAsync();
                    if (res < 1)
                    {
                        Logger.Log($"{data}任务开始失败", Common.LogLevel.Error);
                        return;
                    }
                    Logger.Log($"{data.username} 任务进入采集队列中", Common.LogLevel.Success);
                }
            });
            var CollectTaskmenuRefresh = new ToolStripMenuItem("刷新", null, async (object sender, EventArgs e) =>
            {
                await LoadCollectTask();
                Logger.Log("刷新成功");
            });

            _contextCollectTaskMenu.Items.AddRange(new ToolStripItem[] {
                CollectTaskmenuDelete,
                StartCollectTaskmenuDelete,
                CollectTaskmenuRefresh
            });

            lvCollect.ContextMenuStrip = _contextCollectTaskMenu;




            _contextWebwoserAccountMenu = new ContextMenuStrip();
            var webwoserAccountDel = new ToolStripMenuItem("删除", null, async (object sender, EventArgs e) =>
            {
                DialogResult result = MessageBox.Show(
    "确定要删除选中的账号吗？",    // 消息内容
    "确认操作",                // 标题
    MessageBoxButtons.YesNo,   // 按钮类型
    MessageBoxIcon.Question   // 图标类型
);

                // 判断用户点击了哪个按钮
                if (result != DialogResult.Yes)
                {
                    return;
                }
                foreach (ListViewItem item in lsWebwoserAccounts.SelectedItems)
                {
                    var id = Convert.ToInt32(item.Text);

                    var res = await DbHelper.Db.Deleteable<Models.webbrowser_accounts>().Where(it => it.id == id).ExecuteCommandAsync();

                    Logger.Log($"web账号删除{(res > 0 ? "成功" : "失败")}：ID={id}");
                }
                await LoadWebwoserAccount();
            });
            var webwoserAccountGetAccount = new ToolStripMenuItem("获取手机号", null, async (object sender, EventArgs e) =>
            {
                var id = Convert.ToInt32(lsWebwoserAccounts.SelectedItems[0].Text);
                var data = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().FirstAsync(it => it.id == id);
                if (data == null)
                {
                    Logger.Log($"数据库中找不到该数据源", Common.LogLevel.Error);
                    return;
                }
                _driver = await OpenWebbrowser(data.localstorage);
                var mobile = await GetAccountMobileAsync();

                CloseBrowser();
                if (string.IsNullOrEmpty(mobile))
                {
                    Logger.Log($"手机号获取失败", Common.LogLevel.Error);
                    return;
                }
                else
                {
                    data.phonenumber = mobile;

                    var updateMobile = await DbHelper.Db.Updateable<Models.webbrowser_accounts>(data).ExecuteCommandAsync();
                    if (updateMobile < 1)
                    {
                        Logger.Log($"修改手机号失败", Common.LogLevel.Error);
                        return;
                    }
                    else
                    {
                        Logger.Log($"修改手机号成功", Common.LogLevel.Success);
                        return;
                    }
                }

            });
            var webwoserAccountShowAccount = new ToolStripMenuItem("查看账号", null, async (object sender, EventArgs e) =>
            {
                var id = Convert.ToInt32(lsWebwoserAccounts.SelectedItems[0].Text);
                var data = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().FirstAsync(it => it.id == id);
                if (data == null)
                {
                    Logger.Log($"数据库中找不到该数据源", Common.LogLevel.Error);
                    return;
                }
                _driver = await OpenWebbrowser(data.localstorage);
            });

            var webbrowserReload = new ToolStripMenuItem("刷新", null, async (object sender, EventArgs e) =>
            {
                await LoadWebwoserAccount();
                Logger.Log("数据已刷新", Common.LogLevel.Success);
            });

            var webbrowserGetGroup = new ToolStripMenuItem("获取账号已加入群聊", null, async (object sender, EventArgs e) =>
            {
                var id = Convert.ToInt32(lsWebwoserAccounts.SelectedItems[0].Text);

                _ = Task.Run(async () => { await WebBrowserGetGroup(id); });

            });

            var webbrowserDeleteCollectGroupLog = new ToolStripMenuItem("清空所选账号采集群链接记录", null, async (object sender, EventArgs e) =>
            {
                var ids = new List<int>();
                foreach (ListViewItem item in lsWebwoserAccounts.SelectedItems)
                {
                    ids.add(Convert.ToInt32(item.Text));
                }
                var res = await DbHelper.Db.Deleteable<Models.webbrowser_collect_group_log>().Where(it => ids.Contains(it.id)).ExecuteCommandAsync();

                Logger.Log($"成功删除{ids.Count}个账号数据,共计{res}条", Common.LogLevel.Success);

            });

            _contextWebwoserAccountMenu.Items.AddRange(new ToolStripItem[] {
                webbrowserReload,
                webwoserAccountDel,
                webwoserAccountShowAccount,
                webwoserAccountGetAccount,
                webbrowserGetGroup,
                webbrowserDeleteCollectGroupLog
            });
            lsWebwoserAccounts.ContextMenuStrip = _contextWebwoserAccountMenu;


            _contexSessionAccountMenu = new ContextMenuStrip();
            var SessionAccountDel = new ToolStripMenuItem("删除", null, async (object sender, EventArgs e) =>
            {
                DialogResult result = MessageBox.Show(
   "确定要删除吗？",    // 消息内容
   "确认操作",                // 标题
   MessageBoxButtons.YesNo,   // 按钮类型
   MessageBoxIcon.Question   // 图标类型
);

                // 判断用户点击了哪个按钮
                if (result != DialogResult.Yes)
                {
                    return;
                }
                foreach (ListViewItem item in lvAccounts.SelectedItems)
                {
                    var phone = item.SubItems[0].Text;
                    try
                    {
                        var filePath = _filePath + @"\" + phone + ".session";
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            Logger.Log($"移除成功,账号{phone}", Common.LogLevel.Success);
                        }
                        else
                        {
                            Logger.Log($"文件不存在,账号{phone}", Common.LogLevel.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"移除失败,,账号{phone},错误日志{ex.Message}", Common.LogLevel.Error);
                    }
                }
                await LoadAccounts();
            });

            var SessionAccountReload = new ToolStripMenuItem("刷新列表", null, async (object sender, EventArgs e) =>
            {
                await LoadAccounts();
            });

            _contexSessionAccountMenu.Items.AddRange(new ToolStripItem[] {
                SessionAccountDel,
                SessionAccountReload
            });
            lvAccounts.ContextMenuStrip = _contexSessionAccountMenu;

            //已采集会员列表 右键菜单事件

            _contexCollectUserNameMenu = new ContextMenuStrip();

            _contexCollectUserNameMenu.Items.AddRange(new ToolStripItem[] {
                new ToolStripMenuItem("刷新",null,async (object sender, EventArgs e) =>
                {
                    await LoadCollectUserName();
                    Logger.Log("数据已刷新", Common.LogLevel.Success);
                }),
                new ToolStripMenuItem("删除",null,async (object sender, EventArgs e) =>
                {
                            DialogResult result = MessageBox.Show(
   "确定要删除选中吗？",    // 消息内容
   "确认操作",                // 标题
   MessageBoxButtons.YesNo,   // 按钮类型
   MessageBoxIcon.Question   // 图标类型
);

                     if (result != DialogResult.Yes)
                {
                    return;
                }

                       foreach (ListViewItem item in lsCollectUserName.SelectedItems)
                {
                    try
                    {
                            var id =Convert.ToInt32(item.SubItems[1].Text) ;
                            var res = await DbHelper.Db.Deleteable<Models.collect_group_member_username>().Where(it => it.id == id).ExecuteCommandAsync();
                            if(res>0){
                                Logger.Log($"删除成功 Id标识{id}", Common.LogLevel.Success);

                            }else{
                                Logger.Log("删除失败", Common.LogLevel.Error);
                            }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"移除失败,账号{item.SubItems[1].Text},错误日志{ex.ToString()}", Common.LogLevel.Error);
                    }
                }

                                await LoadCollectUserName();
                }),
            });

            lsCollectUserName.ContextMenuStrip = _contexCollectUserNameMenu;


            _contexWebCollectTaskMenu = new ContextMenuStrip();
            _contexWebCollectTaskMenu.Items.AddRange(
                 new ToolStripItem[] {
                    new ToolStripMenuItem("刷新",null,async (object sender, EventArgs e) =>
                     {
                         await LoadWebCollectGroup();
                         Logger.Log("数据已刷新", Common.LogLevel.Success);
                     }),
                     new ToolStripMenuItem("加入采集队列",null,async (object sender, EventArgs e) =>
                     {
                         var selectList = lsWebCollect.SelectedItems;
                         var SuccessCount = 0;
                         var FailCount = 0;
                         foreach (ListViewItem item in selectList)
                        {
                             var id =Convert.ToInt32(item.SubItems[0].Text) ;
                             var res = await DbHelper.Db.Updateable<Models.webbrowser_collect_username>().SetColumns(it => it.status == 1).Where(it => it.id == id).ExecuteCommandAsync();

                             if(res>0){
                                 SuccessCount ++;
                             }else{
                                 FailCount ++;
                             }
                         }

                         Logger.Log($"加入队列成功：成功 {SuccessCount} 条，失败 {FailCount} 条",Common.LogLevel.Info);
                         await LoadWebCollectGroup();
                     }),
                     new ToolStripMenuItem("移除",null,async (object sender, EventArgs e) =>
                     {
                         var selectList = lsWebCollect.SelectedItems;
                         var SuccessCount = 0;
                         var FailCount = 0;
                         foreach (ListViewItem item in selectList)
                        {
                             var id =Convert.ToInt32(item.SubItems[0].Text) ;
                             var res = await DbHelper.Db.Deleteable<Models.webbrowser_collect_username>().Where(it => it.id == id).ExecuteCommandAsync();

                             if(res>0){
                                 SuccessCount ++;
                             }else{
                                 FailCount ++;
                             }
                         }

                         Logger.Log($"移除web采集任务成功：成功 {SuccessCount} 条，失败 {FailCount} 条",Common.LogLevel.Info);
                         await LoadWebCollectGroup();
                     }),
            });
            lsWebCollect.ContextMenuStrip = _contexWebCollectTaskMenu;

        }

        private async Task WebBrowserGetGroup(int accountId)
        {
            var processName = "web获取已加入群聊线程==>";
            var id = accountId;
            var data = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().FirstAsync(it => it.id == id);
            if (data == null)
            {
                Logger.Log($"{processName}数据库中找不到该数据源,accountId：{accountId}已退出采集队列", Common.LogLevel.Error);
                return;
            }
            try
            {

                _driver = await OpenWebbrowser(data.localstorage);
                if (_driver == null)
                {
                    Logger.Log($"{processName}浏览器失去控制权，{data.phonenumber}已退出采集队列", Common.LogLevel.Error);
                    return;
                }
                var seachInputs = _driver.FindElements(By.CssSelector("input.input-field-input.is-empty.input-search-input.with-focus-effect"));
                for (int i = 0; i < 10; i++)
                {
                    if (seachInputs.Count == 0)
                    {
                        Logger.Log($"{processName}未找到搜索框，等待5秒继续查找", Common.LogLevel.Warning);
                        await Task.Delay(5000);

                        var notLogin = _driver.FindElements(By.CssSelector("canvas._qrCanvas_1b0yp_186"));
                        if (notLogin.Count > 0)
                        {
                            Logger.Log($"{processName}{data.phonenumber}当前账号未登录，请点击重试", Common.LogLevel.Warning);
                            return;
                        }
                    }
                    else
                    {
                        Logger.Log($"web群发线程==>已确认找到搜索框", Common.LogLevel.Warning);
                        break;
                    }
                }

                var ErrorCount = 0;
                var delayTime = 2;
                //先拉到底部
                var mainDivs = _driver.FindElements(By.CssSelector("div.scrollable.scrollable-y.tabs-tab.chatlist-parts.folders-scrollable.scrollable-y-bordered.active"));
                var toBotton = false;
                while (ErrorCount < 10)
                {
                    if (mainDivs.Count == 0)
                    {
                        Logger.Log($"{processName}未能找到主容器", Common.LogLevel.Error);
                        ErrorCount++;
                        continue;
                    }
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", mainDivs[0]);

                    await Task.Delay(delayTime * 1000);

                    var mainOverDiv = _driver.FindElements(By.CssSelector("div.scrollable.scrollable-y.tabs-tab.chatlist-parts.folders-scrollable.scrollable-y-bordered.active.scrolled-end"));

                    if (mainOverDiv.Count > 0)
                    {
                        toBotton = true;
                        //已到底
                        break;
                    }
                }
                if (!toBotton)
                {
                    Logger.Log($"{processName}{data.phonenumber}无法拉取列表到底部,跳过账号", Common.LogLevel.Error);
                    return;
                }

                Logger.Log($"{processName}已成功跳转到底部,开始遍历群组", Common.LogLevel.Info);

                var addList = new List<Models.webbrowser_collect_username>();
                var logs = new List<Models.webbrowser_collect_group_log>();

                while (true)
                {

                    var aDivs = mainDivs[0].FindElements(By.CssSelector("a.rp.row.no-wrap.row-with-padding.row-clickable.hover-effect.chatlist-chat.chatlist-chat-bigger.row-big._Item_5idej_1"));
                    if (aDivs.Count == 0)
                    {
                        await Task.Delay(delayTime * 1000);

                        Logger.Log($"{processName}{data.phonenumber}找不到对话列表,跳过", Common.LogLevel.Error);
                        return;
                    }

                    foreach (var item in aDivs)
                    {
                        try
                        {
                            var peerId = Convert.ToInt64(item.GetAttribute("data-peer-id"));
                            if (peerId > 0)
                            {
                                continue;
                            }
                            var groupAny = await DbHelper.Db.Queryable<Models.webbrowser_collect_group_log>().Where(it => it.peer_id == peerId.ToString()).AnyAsync();

                            if (groupAny)
                            {
                                continue;
                            }

                            groupAny = logs.Any(it => it.peer_id == peerId.ToString());
                            if (groupAny)
                            {
                                continue;
                            }


                            addList.add(new Models.webbrowser_collect_username
                            {
                                account_id = accountId,
                                collect_success_count = 0,
                                collect_time = 0,
                                create_time = Common.Tools.Instance.GetTimeStamp(),
                                group_total_count = 0,
                                msg = "",
                                status = 0,
                                username = @$"https://web.telegram.org/k/#{peerId}"
                            });
                            logs.add(new Models.webbrowser_collect_group_log
                            {
                                peer_id = peerId.toString()
                            });
                        }
                        catch (Exception ex)
                        {
                            Logger.Log($"{processName}采集群聊报错{ex.ToString()}", Common.LogLevel.Error);
                        }

                    }

                    var startDivs = _driver.FindElements(By.CssSelector("div.scrollable.scrollable-y.tabs-tab.chatlist-parts.folders-scrollable.scrollable-y-bordered.active.scrolled-start"));
                    if (startDivs.Count > 0)
                    {
                        //遍历完全部数据
                        break;
                    }
                    var movePx = 400;
                    ((IJavaScriptExecutor)_driver).ExecuteScript($"arguments[0].scrollTop = arguments[0].scrollTop-{movePx};", mainDivs[0]);
                    Logger.Log($"{processName}上拉{movePx}像素", Common.LogLevel.Info);
                    await Task.Delay(delayTime * 1000);

                }
                Logger.Log($"{processName}【{data.phonenumber}】共采集到群组{addList.Count()}个,正在写入数据库", Common.LogLevel.Success);

                var res = await DbHelper.Db.Insertable(addList).ExecuteCommandAsync();
                var res2 = await DbHelper.Db.Insertable(logs).ExecuteCommandAsync();

                Logger.Log($"{processName}【{data.phonenumber}】采集到{addList.Count()}个群，写入成功{res}条", Common.LogLevel.Success);
            }
            catch (Exception ex)
            {
                Logger.Log($"{processName}线程异常，已停止{ex.toString()}", Common.LogLevel.Error);
            }
            finally
            {
                Logger.Log($"{processName}{data}已退出采集队列");
                CloseBrowser();
            }


        }

        #endregion

        #region vpn
        // ========== 获取选中的 VPN ==========
        private vpns GetSelectedVpn()
        {
            if (lvVpnList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一条记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return lvVpnList.SelectedItems[0].Tag as vpns;
        }

        // ========== 获取所有勾选的 VPN ==========
        private List<vpns> GetCheckedVpns()
        {
            var result = new List<vpns>();
            foreach (ListViewItem item in lvVpnList.Items)
            {
                if (item.Checked && item.Tag is vpns vpn)
                {
                    result.Add(vpn);
                }
            }
            return result;
        }

        // ========== 编辑 ==========
        private void MenuEdit_Click(object sender, EventArgs e)
        {
            EditSelected();
        }

        private void EditSelected()
        {
            var vpn = GetSelectedVpn();
            if (vpn == null) return;

            // 打开编辑窗口（传入数据）
            var editForm = new AddVpnForm(vpn);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // 更新数据库
                DbHelper.Db.Updateable(vpn).ExecuteCommand();
                _ = LoadVpns();
                Logger.Log($"VPN 编辑成功：ID={vpn.id}", Common.LogLevel.Success);
                MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========== 删除（单个） ==========
        private void MenuDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            var vpn = GetSelectedVpn();
            if (vpn == null) return;

            if (MessageBox.Show($"确定删除 ID={vpn.id} 的 VPN 吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            DbHelper.Db.Deleteable<vpns>().Where(x => x.id == vpn.id).ExecuteCommand();
            _ = LoadVpns();
            Logger.Log($"VPN 删除成功：ID={vpn.id}", Common.LogLevel.Success);
        }

        // ========== 批量删除（勾选的） ==========
        private void DeleteChecked()
        {
            var checkedList = GetCheckedVpns();
            if (checkedList.Count == 0)
            {
                MessageBox.Show("请先勾选要删除的记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"确定删除选中的 {checkedList.Count} 条 VPN 吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var ids = checkedList.Select(x => x.id).ToList();
            DbHelper.Db.Deleteable<vpns>().In(ids).ExecuteCommand();
            _ = LoadVpns();
            Logger.Log($"批量删除 VPN：{checkedList.Count} 条", Common.LogLevel.Success);
        }

        // ========== 启用/禁用（单个） ==========
        private void MenuToggle_Click(object sender, EventArgs e)
        {
            ToggleSelected();
        }

        private void ToggleSelected()
        {
            var vpn = GetSelectedVpn();
            if (vpn == null) return;

            vpn.Status = !vpn.Status;
            DbHelper.Db.Updateable(vpn).ExecuteCommand();
            _ = LoadVpns();
            Logger.Log($"VPN 状态切换：ID={vpn.id} -> {(vpn.Status ? "启用" : "禁用")}", Common.LogLevel.Success);
        }

        // ========== 批量启用/禁用（勾选的） ==========
        private void ToggleChecked(bool enable)
        {
            var checkedList = GetCheckedVpns();
            if (checkedList.Count == 0)
            {
                MessageBox.Show("请先勾选要操作的记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string action = enable ? "启用" : "禁用";
            if (MessageBox.Show($"确定{action}选中的 {checkedList.Count} 条 VPN 吗？", $"确认{action}",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            foreach (var vpn in checkedList)
            {
                vpn.Status = enable;
                DbHelper.Db.Updateable(vpn).ExecuteCommand();
            }

            _ = LoadVpns();
            Logger.Log($"批量{action} VPN：{checkedList.Count} 条", Common.LogLevel.Success);
        }

        // ========== 刷新 ==========
        private void MenuRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadVpns();
            Logger.Log("VPN 列表已刷新", Common.LogLevel.Info);
        }

        // ========== 新增 VPN ==========
        private void btnVpnAdd_Click(object sender, EventArgs e)
        {
            AddVpnForm form = new AddVpnForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadVpns();
                Logger.Log("新增 VPN 成功", Common.LogLevel.Success);
            }
        }

        // ========== 刷新按钮 ==========
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadVpns();
        }

        // ========== 上一页 ==========
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                _ = LoadVpns();
            }
        }

        // ========== 下一页 ==========
        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            _ = LoadVpns();
        }

        // ========== 批量删除按钮 ==========
        private void btnBatchDelete_Click(object sender, EventArgs e)
        {
            DeleteChecked();
        }

        // ========== 批量启用 ==========
        private void btnBatchEnable_Click(object sender, EventArgs e)
        {
            ToggleChecked(true);
        }

        // ========== 批量禁用 ==========
        private void btnBatchDisable_Click(object sender, EventArgs e)
        {
            ToggleChecked(false);
        }

        // ========== 全选 ==========
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvVpnList.Items)
            {
                item.Checked = true;
            }
        }

        // ========== 全不选 ==========
        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvVpnList.Items)
            {
                item.Checked = false;
            }
        }

        // ========== 双击编辑 ==========
        private void lvVpnList_DoubleClick(object sender, EventArgs e)
        {
            EditSelected();
        }

        private async void btnNext_Click_1(object sender, EventArgs e)
        {
            _currentPage += 1;
            await LoadVpns();

        }

        private async void btnPrev_Click_1(object sender, EventArgs e)
        {
            _currentPage -= 1;
            await LoadVpns();
        }

        private async void btnPageGoto_Click(object sender, EventArgs e)
        {
            var page = this.txtPageGoto.Text.Trim();
            if (int.TryParse(page, out int pageNum) && pageNum > 0)
            {
                _currentPage = pageNum;
                await LoadVpns();
            }
            else
            {
                Logger.Log("请输入有效的页码！", Common.LogLevel.Warning);
            }
        }

        private async void btnSeach_Click(object sender, EventArgs e)
        {
            await LoadVpns();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AddAccountForm form = new AddAccountForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadAccounts();
                Logger.Log("新增 账号 成功", Common.LogLevel.Success);
            }
        }

        private async void btnSettingSave_Click(object sender, EventArgs e)
        {
            try
            {

                //基础设置
                var apiId = this.txtApiId.Text.Trim();
                var apiHash = this.txtApiHash.Text.Trim();

                //web群发设置
                var webSendMin = this.txtWebGroupManagerSendMin.Text.Trim() == "" ? 0 : int.Parse(this.txtWebGroupManagerSendMin.Text.Trim());
                var webSendMax = this.txtWebGroupManagerSendMax.Text.Trim() == "" ? 0 : int.Parse(this.txtWebGroupManagerSendMax.Text.Trim());
                var webSendMaxCount = this.txtWebGroupManagerSendMaxCount.Text.Trim() == "" ? 0 : int.Parse(this.txtWebGroupManagerSendMaxCount.Text.Trim());
                var webSendContext = this.txtWebManagerSendContext.Text.Trim();

                //协议群发设置
                var sessionSendMin = this.txtSessionSendMsgDelayMins.Text.Trim() == "" ? 0 : int.Parse(this.txtSessionSendMsgDelayMins.Text.Trim());
                var sessionSendMax = this.txtSessionSendMsgDelayMaxs.Text.Trim() == "" ? 0 : int.Parse(this.txtSessionSendMsgDelayMaxs.Text.Trim());
                var sessionSendCountMax = this.txtSessionSendMsgCountMax.Text.Trim() == "" ? 0 : int.Parse(this.txtSessionSendMsgCountMax.Text.Trim());
                var sessionSendContexts = this.txtSessionSendMsgContexts.Text.Trim();

                //协议采集设置
                var sessionCollectInfoEnabled = this.cboxSessionCollectInfoFilter.Checked;
                var sessionCollectNickEnabled = this.cboxSessionCollectNickFilter.Checked;

                var sessionCollectInfoText = this.txtSessionCollectInfoFilter.Text;
                var sessionCollectNickText = this.txtSessionCollectNickFilter.Text;

                //web采集设置
                var webtInfoEnabled = this.cboxWebCollectInfoFilter.Checked;
                var webNickEnabled = this.cboxWebCollectNickFilter.Checked;

                var webCollectInfoText = this.txtWebCollectInfoFilter.Text;
                var webCollectNickText = this.txtWebCollectNickFilter.Text;


                var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
                var res = 0;
                if (setting == null)
                {
                    setting = new Models.settings
                    {
                        //基础设置
                        api_id = apiId,
                        api_hash = apiHash,
                        default_vpn_id = cboSettingVpn.SelectedIndex == 0 ? 0 : ((List<vpns>)DbHelper.Db.Queryable<vpns>().Where(it => it.Status).ToList()).ElementAt(cboSettingVpn.SelectedIndex - 1).id,

                        //web群发设置
                        web_send_min = webSendMin,
                        web_send_context = webSendContext,
                        web_send_max = webSendMax,
                        web_send_max_count = webSendMaxCount,
                        web_last_send_count = 0,

                        //协议群发设置
                        session_send_context = sessionSendContexts,
                        session_send_count = sessionSendCountMax,
                        session_send_max = sessionSendMax,
                        session_send_min = sessionSendMin,

                        //web采集设置
                        web_collect_info_filter_enabled = webtInfoEnabled,
                        web_collect_info_filter_context = webCollectInfoText,
                        web_collect_nick_filter_enabled = webNickEnabled,
                        web_collect_nick_filter_context = webCollectNickText,

                        //协议采集设置
                        session_collect_info_filter_enabled = sessionCollectInfoEnabled,
                        session_collect_info_filter_context = sessionCollectInfoText,
                        session_collect_nick_filter_enabled = sessionCollectNickEnabled,
                        session_collect_nick_filter_context = sessionCollectNickText,
                        session_collect_avatar_enabled = cboxSessionSetingDownloadAvatar.Checked,
                        session_collect_filter_bot_enabled = cboxSessionCollectFilterBot.Checked,
                        session_collect_filter_manager_enabled = cboxSessionCollectFilterManager.Checked
                    };
                    res = await DbHelper.Db.Insertable(setting).ExecuteCommandAsync();
                }
                else
                {
                    //基础设置
                    setting.api_id = apiId;
                    setting.api_hash = apiHash;
                    setting.default_vpn_id = cboSettingVpn.SelectedIndex == 0 ? 0 : ((List<vpns>)DbHelper.Db.Queryable<vpns>().Where(it => it.Status).ToList()).ElementAt(cboSettingVpn.SelectedIndex - 1).id;


                    //web群发设置
                    setting.web_send_max_count = webSendMaxCount;
                    setting.web_send_min = webSendMin;
                    setting.web_send_max = webSendMax;
                    setting.web_send_context = webSendContext;

                    //协议群发设置
                    setting.session_send_context = sessionSendContexts;
                    setting.session_send_count = sessionSendCountMax;
                    setting.session_send_max = sessionSendMax;
                    setting.session_send_min = sessionSendMin;

                    //web采集设置
                    setting.web_collect_info_filter_enabled = webtInfoEnabled;
                    setting.web_collect_info_filter_context = webCollectInfoText;
                    setting.web_collect_nick_filter_enabled = webNickEnabled;
                    setting.web_collect_nick_filter_context = webCollectNickText;

                    //协议采集设置
                    setting.session_collect_info_filter_enabled = sessionCollectInfoEnabled;
                    setting.session_collect_info_filter_context = sessionCollectInfoText;
                    setting.session_collect_nick_filter_enabled = sessionCollectNickEnabled;
                    setting.session_collect_nick_filter_context = sessionCollectNickText;
                    setting.session_collect_avatar_enabled = cboxSessionSetingDownloadAvatar.Checked;

                    setting.session_collect_filter_bot_enabled = cboxSessionCollectFilterBot.Checked;
                    setting.session_collect_filter_manager_enabled = cboxSessionCollectFilterManager.Checked;

                    res = await DbHelper.Db.Updateable(setting).ExecuteCommandAsync();
                }

                if (res > 0)
                {
                    Logger.Log($"设置保存成功！影响行数{res}行", Common.LogLevel.Success);
                    await LoadSettings();
                }
                else
                {
                    Logger.Log("设置保存失败！", Common.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Common.LogLevel.Error);
            }
        }

        private async void btnAccountUp_Click(object sender, EventArgs e)
        {
            this._currentAccountPage--;
            await LoadAccounts();
        }

        private async void btcAccountNext_Click(object sender, EventArgs e)
        {
            this._currentAccountPage++;
            await LoadAccounts();
        }

        private async void btnAccountGoto_Click(object sender, EventArgs e)
        {
            this._currentAccountPage = Convert.ToInt32(this.txtAccountGoto.Text);
            if (this._currentAccountPage < 1)
            {
                this._currentAccountPage = 1;
            }
            await LoadAccounts();
        }

        private async void btnAccountRefresh_Click(object sender, EventArgs e)
        {

            var groupCount = 0;
            var loginSuccessCount = 0;
            var loginFailCount = 0;
            try
            {
                btnAccountRefresh.Enabled = false;
                _allSessionFiles = System.IO.Directory.GetFiles(_filePath)
                                     .OrderBy(f => f) // 排序，保证顺序一致
                                     .ToList();
                var settings = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();


                foreach (var item in _allSessionFiles)
                {

                    WTelegram.Client _client = default;
                    string account = Path.GetFileNameWithoutExtension(item);
                    try
                    {
                        _client = new WTelegram.Client(Config);
                        var vpn = await DbHelper.Db.Queryable<Models.vpns>().Where(it => it.id == settings.default_vpn_id).FirstAsync();
                        if (vpn != null)
                        {
                            _client.TcpHandler = async (address, port) =>
                            {
                                var proxy = new Socks5ProxyClient(vpn.host, Convert.ToInt32(vpn.port));
                                return proxy.CreateConnection(address, port);
                            };
                        }

                        var user = await _client.LoginUserIfNeeded();

                        if (user == null)
                        {
                            loginFailCount++;
                            Logger.Log($"账号 {account} 登录失败！", Common.LogLevel.Error);
                        }
                        else
                        {
                            loginSuccessCount++;
                            var res = await DbHelper.Db.Insertable<Models.account_check_log>(new Models.account_check_log
                            {
                                account = account,
                                msg = "登录成功",
                                status = 1,
                                time = Common.Tools.Instance.GetTimeStamp()
                            }).ExecuteCommandAsync();
                            Logger.Log($"登录成功！\n用户ID: {user.id}\n用户名: {user.username ?? "无"}");

                            var chats = await _client.Messages_GetAllChats();
                            var groups = chats.chats.Where(it => it.Value != null && it.Value.IsGroup).ToList();

                            foreach (var group in groups)
                            {
                                if (group.Value != null)
                                {
                                    var groupAny = await DbHelper.Db.Queryable<Models.collect_group_username>().Where(it => it.group_id == group.Key).AnyAsync();
                                    if (groupAny)
                                    {
                                        continue;
                                    }
                                    var result = await DbHelper.Db.Insertable<Models.collect_group_username>(new Models.collect_group_username
                                    {
                                        collect_time = 0,
                                        create_time = Common.Tools.Instance.GetTimeStamp(),
                                        group_total_count = 0,
                                        msg = group.Value.Title,
                                        status = 0,
                                        username = group.Value.MainUsername == null ? "" : group.Value.MainUsername,
                                        group_id = group.Key,
                                        collect_phone = account
                                    }).ExecuteCommandAsync();
                                    groupCount++;
                                }
                            }

                        }


                        string Config(string what)
                        {
                            switch (what)
                            {
                                case "api_id": return settings.api_id;
                                case "api_hash": return settings.api_hash;
                                case "phone_number":
                                    {
                                        Logger.Log("📱 准备发送验证码...", Common.LogLevel.Info);
                                        return account;
                                    }
                                case "verification_code": return "";
                                case "password": return "";     // if user has enabled 2FA
                                case "session_pathname":
                                    {
                                        return item;
                                    }  // to save/load session
                                default: return null;                  // let WTelegramClient decide the default config
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        loginFailCount++;
                        Logger.Log($"账号 {account} 登录异常：{ex.Message}", Common.LogLevel.Error);
                        var res = await DbHelper.Db.Insertable<Models.account_check_log>(new Models.account_check_log
                        {
                            account = account,
                            msg = ex.Message,
                            status = 0,
                            time = Common.Tools.Instance.GetTimeStamp()
                        }).ExecuteCommandAsync();
                    }
                    finally
                    {
                        _client?.Dispose();
                    }


                }

                await LoadAccounts();
            }
            catch (Exception ex)
            {
                Logger.Log($"检测账号异常{ex.ToString()}", Common.LogLevel.Error);
            }
            finally
            {
                Logger.Log($"检测账号完成,总计登录成功{loginSuccessCount}个账号，失败{loginFailCount}个账号,群组{groupCount}个", Common.LogLevel.Success);
                btnAccountRefresh.Enabled = true;
            }

        }

        private async void btnAccountSeach_Click(object sender, EventArgs e)
        {
            await LoadAccounts();
        }

        private async void btnCollectAdd_Click(object sender, EventArgs e)
        {
            var username = txtCollectAdd.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                Logger.Log("请填写群链接");
                return;
            }

            username = NormalizeGroupUsername(username);

            var res = await DbHelper.Db.Insertable<Models.collect_group_username>(new Models.collect_group_username
            {
                create_time = Common.Tools.Instance.GetTimeStamp(),
                status = 0,
                username = username,
                collect_time = 0,
                msg = "",
                group_total_count = 0
            }).ExecuteCommandAsync();

            Logger.Log($"新增采集任务成功！{txtCollectAdd.Text.Trim()}", Common.LogLevel.Success);

            await LoadCollectTask();
        }

        private async void btnCollectTaskSeach_Click(object sender, EventArgs e)
        {
            await LoadCollectTask();
        }

        private async void btnCollectTaskNext_Click(object sender, EventArgs e)
        {
            await LoadCollectTask();

        }

        private async void btnCollectTaskPrev_Click(object sender, EventArgs e)
        {

            await LoadCollectTask();
        }

        private async void btnAccountSelecetPath_Click(object sender, EventArgs e)
        {
            var path = Common.Tools.Instance.GetDefaultPath();
            using (var folderDialog = new FolderBrowserDialog())
            {
                // 设置对话框标题
                folderDialog.Description = "请选择文件夹";

                // 设置起始目录（默认桌面）
                folderDialog.SelectedPath = path;

                // 显示新文件夹按钮（允许用户创建新文件夹）
                folderDialog.ShowNewFolderButton = true;

                // 显示对话框
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取选中的文件夹路径
                    string selectedPath = folderDialog.SelectedPath;

                    // 显示路径 

                    var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
                    if (setting == null)
                    {
                        setting = new settings
                        {
                            api_id = "",
                            api_hash = "",
                            default_vpn_id = 0,
                            default_account_path = selectedPath
                        };
                        var res = await DbHelper.Db.Insertable(setting).ExecuteCommandAsync();
                    }
                    else
                    {
                        setting.default_account_path = selectedPath;
                        var res = await DbHelper.Db.Updateable(setting).ExecuteCommandAsync();
                    }

                    _filePath = selectedPath;
                    await LoadAccounts();
                }
            }
        }

        #endregion
        #region 控制浏览器

        private async Task LoadWebwoserAccount()
        {
            try
            {
                var sql = $"update webbrowser_accounts set last_send_date={DateTime.Now.ToString("yyyyMMdd")},today_send_count=0 where last_send_date<>{DateTime.Now.ToString("yyyyMMdd")}";
                var res = await DbHelper.Db.Ado.ExecuteCommandAsync(sql);

                var dom = lsWebwoserAccounts;
                var page = _currentWebwoserPage;

                // 查询分页数据
                var list = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().OrderByDescending(it => it.id).ToPageListAsync(page, _pageSize); ;


                var total = await DbHelper.Db.Queryable<webbrowser_accounts>().CountAsync();

                dom.BeginUpdate();
                dom.Items.Clear();

                foreach (var item in list)
                {
                    var row = new ListViewItem(item.id.ToString());

                    row.SubItems.Add(item.phonenumber);
                    row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(item.login_time).ToString());

                    row.SubItems.Add(item.localstorage);
                    // 保存数据对象到 Tag
                    row.Tag = item;

                    dom.Items.Add(row);
                }

                dom.EndUpdate();

                // 更新分页信息
                int totalPages = (int)Math.Ceiling((double)total / _pageSize);
                lbWebwoserPageShow.Text = $"{page}/{totalPages}";
                btnWebwoserup.Enabled = _currentPage > 1;

                btnWebwoserNext.Enabled = _currentPage < totalPages;
            }
            catch (Exception ex)
            {
                Logger.Log($"加载Webwoser账号时发生错误: {ex.Message}", Common.LogLevel.Error);

            }
        }

        private void btnWebwoserup_Click(object sender, EventArgs e)
        {

        }

        private void btnWebwoserNext_Click(object sender, EventArgs e)
        {

        }

        private void btnWebwoserGoto_Click(object sender, EventArgs e)
        {

        }

        private IWebDriver? _driver;
        private Process? _driverProcess;
        private string? _driverLogPath;
        private bool _isBrowserStarting;



        private async void btnWebwoserAddAccount_Click(object sender, EventArgs e)
        {
            if (_isBrowserStarting) return;
            _isBrowserStarting = true;
            btnWebwoserAddAccount.Enabled = false;

            try
            {
                // 将整个浏览器启动逻辑放到后台线程
                await Task.Run(async () =>
                {
                    _driver = await OpenWebbrowser();

                    if (_driver == null)
                    {
                        Logger.Log("浏览器初始化失败", Common.LogLevel.Error);
                    }
                    else
                    {
                        Logger.Log("浏览器初始化成功", Common.LogLevel.Success);
                    }


                    using var loginCancellation = new CancellationTokenSource();
                    var loginSucceeded = await WaitForTelegramLoginAsync(
                        TimeSpan.FromMinutes(5),
                        loginCancellation.Token,
                        _driver
                    );

                    if (!loginSucceeded)
                    {
                        Logger.Log("未确认登录成功：可能超时或浏览器已关闭。", Common.LogLevel.Info);
                        return;
                    }

                    Logger.Log("Telegram 登录成功。正在获取手机号等数据", Common.LogLevel.Success);

                    // 获取手机号
                    var mobile = await GetAccountMobileAsync();

                    // 获取 LocalStorage
                    IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                    var script = @"
                var result = {};
                for (var key in localStorage) {
                    if (localStorage.hasOwnProperty(key)) {
                        result[key] = localStorage.getItem(key);
                    }
                }
                return result;
            ";
                    var localStorage = js.ExecuteScript(script);
                    var list = new List<object>();
                    if (localStorage is Dictionary<string, object> dict)
                    {
                        foreach (var item in dict)
                        {
                            string key = item.Key;
                            string value = item.Value?.ToString();
                            list.Add(new { key = key, value = value });
                        }
                    }

                    // 数据库操作
                    var res = await DbHelper.Db.Insertable<Models.webbrowser_accounts>(new Models.webbrowser_accounts
                    {
                        localstorage = Newtonsoft.Json.JsonConvert.SerializeObject(list),
                        login_time = Common.Tools.Instance.GetTimeStamp(),
                        phonenumber = mobile,
                        last_send_date = 0,
                        today_send_count = 0,
                        last_send_time = 0
                    }).ExecuteCommandAsync();

                    Logger.Log($"账号数据写入{(res > 0 ? "成功" : "失败")}",
                        res > 0 ? Common.LogLevel.Success : Common.LogLevel.Error);


                    this.Invoke(new Action(async () =>
                    {
                        await LoadWebwoserAccount();
                    }));
                    // 关闭浏览器（在后台线程执行）
                    CloseBrowser();
                });

                // 注意：Task.Run 内部的 CloseBrowser 可能在后台执行，UI 线程不会等待
            }
            catch (WebDriverException ex)
            {
                Logger.Log($"初始化浏览器失败：{ex.Message}", Common.LogLevel.Error);
                LogDriverFailureDetails();
                CloseBrowser();
            }
            catch (Exception ex)
            {
                Logger.Log($"初始化浏览器失败：{ex.Message}", Common.LogLevel.Error);
                CloseBrowser();
            }
            finally
            {
                _isBrowserStarting = false;
                btnWebwoserAddAccount.Enabled = true;
            }
        }

        private async Task<string> GetAccountMobileAsync()
        {
            try
            {
                while (true)
                {

                    var element1 = _driver.FindElements(By.CssSelector("div.profile-pinned-gifts"));
                    if (element1.Count > 0)
                    {
                        Logger.Log("已进入 Telegram Web 主界面，尝试获取手机号...", Common.LogLevel.Info);

                        await Task.Delay(2000);
                        element1 = _driver.FindElements(By.CssSelector("div.row-title"));


                        var filteredElements = element1
        .Where(e =>
        {
            string text = e.Text.Trim();
            // 条件1：包含 '+' 号
            if (!text.Contains("+")) return false;

            // 条件2（可选）：进一步验证是否为电话号码格式（包含数字和+）
            // 这个正则匹配以+开头，后面跟数字、空格、-或()的电话号码
            return Regex.IsMatch(text, @"^\+\d[\d\s\-\(\)]{5,}$");
        })
        .ToList();
                        //row-title
                        return filteredElements[0].Text;
                    }
                    else
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        var element = _driver.FindElement(By.CssSelector("button.btn-icon.rp.btn-menu-toggle.sidebar-tools-button.is-visible"));

                        // 强制点击，绕过拦截
                        js.ExecuteScript("arguments[0].click();", element);



                        await Task.Delay(2000);

                        // 1. 先找到包含 "Settings" 的 span
                        var spanElement = _driver.FindElement(By.XPath(
                            "//span[contains(@class, 'btn-menu-item-text') and text()='Settings']"
                        ));

                        if (spanElement != null)
                        {
                            // 2. 找到它所在的父级 div.btn-menu-item.rp-overflow
                            var settingsElement = spanElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'btn-menu-item') and contains(@class, 'rp-overflow')]"));

                            settingsElement.Click();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log($"获取账户手机号失败：{ex.Message}", Common.LogLevel.Error);
                return "";
            }
        }

        private async Task<bool> WaitForTelegramLoginAsync(
        TimeSpan timeout,
        CancellationToken cancellationToken, IWebDriver webDriver)
        {
            var deadline = DateTime.UtcNow + timeout;
            while (DateTime.UtcNow < deadline)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var driver = webDriver;
                if (driver == null)
                {
                    return false;
                }
                try
                {
                    // 将 IWebDriver 转换为 IJavaScriptExecutor
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    // 执行 JavaScript 获取 Local Storage
                    var script = @"
    var result = {};
    for (var key in localStorage) {
        if (localStorage.hasOwnProperty(key)) {
            result[key] = localStorage.getItem(key);
        }
    }
    return result;
";
                    var localStorage = js.ExecuteScript(script);
                    // 转换为 Dictionary
                    if (localStorage is Dictionary<string, object> dict)
                    {
                        foreach (var item in dict)
                        {
                            string key = item.Key;
                            string value = item.Value?.ToString();
                            if (key == "user_auth")
                            {
                                return true; // 登录成功
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"读取 Local Storage 失败：{ex.Message}", Common.LogLevel.Error);
                    CloseBrowser();
                    // 浏览器窗口被用户关闭或 WebDriver 连接已失效。
                    return false;
                }
                // 这一句不会阻塞 UI，只是异步等待一秒后再检查。
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
            return false;
        }



        private async Task<IWebDriver> OpenWebbrowser(string JsonList = "")
        {
            try
            {
                _isBrowserStarting = true;
                // 将整个浏览器启动逻辑放到后台线程
                return await Task.Run(async () =>
                  {
                      var options = new ChromeOptions();
                      options.AddArgument("--disable-gpu");
                      options.AddArgument("--disable-notifications");

                      var driverDirectory = Path.Combine(AppContext.BaseDirectory, "chromedriver-win64");
                      if (!System.IO.File.Exists(Path.Combine(driverDirectory, "chromedriver.exe")))
                      {
                          Logger.Log($"未找到 ChromeDriver：{driverDirectory}", Common.LogLevel.Error);
                          return null;
                      }

                      Logger.Log("正在启动浏览器…", Common.LogLevel.Info);
                      var driverUrl = await StartDriverServiceAsync(driverDirectory);

                      HttpClient.DefaultProxy = new System.Net.WebProxy { BypassProxyOnLocal = true };

                      // RemoteWebDriver 创建
                      var webDriver = new OpenQA.Selenium.Remote.RemoteWebDriver(
                            driverUrl,
                            options.ToCapabilities(),
                            TimeSpan.FromSeconds(60)
                        );
                      webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                      // 导航操作
                      webDriver.Navigate().GoToUrl("https://web.telegram.org/k/");

                      Logger.Log("浏览器初始化成功", Common.LogLevel.Success);

                      if (!string.IsNullOrEmpty(JsonList))
                      {
                          IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                          var jsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SessionData>>(JsonList);

                          // 执行 JavaScript 获取 Local Storage
                          string script = "";
                          var count = 0;
                          foreach (var item in jsonList)
                          {
                              script += $"localStorage.setItem('{item.key}','{item.value}');";
                              count++;
                          }
                          Logger.Log($"JS注入总计{count}条");
                          var localStorage = js.ExecuteScript(script);
                      }
                      return webDriver;
                  });
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Common.LogLevel.Error);
                return null;
            }
            finally
            {
                _isBrowserStarting = false;
            }
        }

        private async Task<Uri> StartDriverServiceAsync(string driverDirectory)
        {
            var port = GetAvailablePort();
            var driverPath = Path.Combine(driverDirectory, "chromedriver.exe");
            _driverLogPath = Path.Combine(Path.GetTempPath(), $"TGForm-ChromeDriver-{port}.log");

            _driverProcess = Process.Start(new ProcessStartInfo
            {
                FileName = driverPath,
                Arguments = $"--port={port} --verbose --log-path=\"{_driverLogPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (_driverProcess == null)
            {
                throw new InvalidOperationException("无法启动 ChromeDriver 进程。");
            }

            var serviceUrl = new Uri($"http://127.0.0.1:{port}");
            using var handler = new HttpClientHandler { UseProxy = false };
            using var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(2) };

            for (var attempt = 0; attempt < 20; attempt++)
            {
                if (_driverProcess.HasExited)
                {
                    throw new WebDriverException($"ChromeDriver 意外退出，退出码：{_driverProcess.ExitCode}。");
                }

                try
                {
                    using var response = await client.GetAsync(new Uri(serviceUrl, "/status"));
                    if (response.IsSuccessStatusCode)
                    {
                        return serviceUrl;
                    }
                }
                catch (HttpRequestException)
                {
                    // 服务尚未完成监听，继续在固定间隔后重试。
                }
                catch (TaskCanceledException)
                {
                    // 单次状态请求超时，继续重试直到总时限结束。
                }

                await Task.Delay(250);
            }

            throw new WebDriverException($"ChromeDriver 未能在 5 秒内响应：{serviceUrl}");
        }

        private static int GetAvailablePort()
        {
            using var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
            listener.Start();
            return ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
        }

        private void LogDriverFailureDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(_driverLogPath) || !System.IO.File.Exists(_driverLogPath))
                {
                    return;
                }

                // 使用 FileShare.ReadWrite 允许其他进程同时读写
                using (var fileStream = new System.IO.FileStream(
                    _driverLogPath,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read,
                    System.IO.FileShare.ReadWrite))
                using (var reader = new System.IO.StreamReader(fileStream))
                {
                    string log = reader.ReadToEnd();
                    const int maxLength = 6000;
                    var recentLog = log.Length > maxLength ? log[^maxLength..] : log;
                    Logger.Log($"ChromeDriver 详细日志：\n{recentLog}", Common.LogLevel.Error);
                }
            }
            catch (IOException ex)
            {
                // 专门处理文件被占用的情况
                Logger.Log($"ChromeDriver 日志文件被占用，无法读取：{ex.Message}", Common.LogLevel.Warning);
                // 可以稍后重试，或者记录当前状态
            }
            catch (Exception ex)
            {
                Logger.Log($"读取 ChromeDriver 日志失败：{ex.Message}", Common.LogLevel.Error);
            }
        }

        private void CloseBrowser()
        {
            var activeDriver = _driver;
            var activeDriverProcess = _driverProcess;
            _driver = null;
            _driverProcess = null;

            try
            {
                activeDriver?.Quit();
            }
            catch (WebDriverException)
            {
                // 浏览器进程已经终止时，Quit 会失败；仍需继续释放本地资源。
            }
            finally
            {
                activeDriver?.Dispose();

                if (activeDriverProcess is { HasExited: false })
                {
                    activeDriverProcess.Kill(entireProcessTree: true);
                }

                activeDriverProcess?.Dispose();
            }

        }

        private static void CancelTask(CancellationTokenSource? cancellationSource)
        {
            try
            {
                cancellationSource?.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // 任务已完成并释放资源。
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _isRunning = false;
            _isRunningWeb = false;
            _isRunningSessionSend = false;
            _isRunningSessionCollect = false;

            CancelTask(_cts);
            CancelTask(_ctsWeb);
            CancelTask(_ctsSessionSend);
            CancelTask(_ctsSessionCollect);
            CloseBrowser();

            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CloseBrowser();
            base.OnFormClosed(e);
        }


        private CancellationTokenSource _cts;
        private bool _isRunning;
        private bool _isComplete = false;
        private async void btnWebbrowserSendMsg_Click(object sender, EventArgs e)
        {
            if (this.btnWebbrowserSendMsg.Text == "开 始 web 群 发")
            {
                var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
                if (setting == null || setting.web_send_max == 0 || setting.web_send_min == 0 || setting.web_send_max_count == 0 || string.IsNullOrEmpty(setting.web_send_context))
                {
                    Logger.Log("请先在设置中配置Web群控群发参数！", Common.LogLevel.Warning);
                    return;
                }
                this.btnWebbrowserSendMsg.Text = "停 止 web 群 发";
                _isRunning = true;
                _cts = new CancellationTokenSource();


                _ = Task.Run(async () =>
                {
                    try
                    {
                        var count = 0;
                        while (_isRunning)
                        {
                            _isComplete = false;
                            _cts.Token.ThrowIfCancellationRequested();

                            await SendMessage(_cts.Token);

                            _isComplete = true;

                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _isComplete = true;
                        Logger.Log("群发任务被取消", Common.LogLevel.Info);

                        this.Invoke(new Action(() =>
                        {
                            this.btnWebbrowserSendMsg.Text = "开 始 群 发";
                            this.btnWebbrowserSendMsg.Enabled = true;
                        }));
                    }
                    finally
                    {
                        _cts?.Dispose();
                        _cts = null;
                    }

                }, _cts.Token);
            }
            else
            {

                // ===== 停止群发 =====
                this.btnWebbrowserSendMsg.Text = "停 止 中...";
                this.btnWebbrowserSendMsg.Enabled = false;

                _isRunning = false;
                _cts?.Cancel();

                // 等待一下让任务响应取消
                while (!_isComplete)
                {
                    this.btnWebbrowserSendMsg.Text = "停 止 中.";
                    await Task.Delay(300);

                    this.btnWebbrowserSendMsg.Text = "停 止 中..";
                    await Task.Delay(300);

                    this.btnWebbrowserSendMsg.Text = "停 止 中...";
                    await Task.Delay(300);
                }

                this.btnWebbrowserSendMsg.Text = "开 始 web 群 发";
                this.btnWebbrowserSendMsg.Enabled = true;
            }
        }




        private async Task SendMessage(CancellationToken cancellationToken)
        {
            var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
            var processName = "Web群发线程==>";
            try
            {
                var userData = await DbHelper.Db.Queryable<Models.collect_group_member_username>().Where(it => it.status == 0).FirstAsync();
                if (userData == null)
                {
                    Logger.Log($"web群发线程==>当前没有可用会员用户名,当前群发已停止", Common.LogLevel.Warning);
                    _cts?.Cancel(); // 请求外层循环停止
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }

                if (setting == null || string.IsNullOrEmpty(setting.web_send_context))
                {
                    Logger.Log($"web群发线程==>当前配置暂无可用发送内容,请先配置,当前群发已停止", Common.LogLevel.Warning);
                    _cts?.Cancel(); // 请求外层循环停止
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }
                var sendTexts = setting.web_send_context.Split("|");

                var sendContext = sendTexts[new Random().Next(0, sendTexts.Length)];



                var account = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().Where(it => it.today_send_count < setting.web_send_max_count).OrderByDescending(it => it.last_send_time).FirstAsync();

                if (account == null)
                {
                    Logger.Log($"web群发线程==>当前没有可用账号,当前群发已停止", Common.LogLevel.Warning);
                    _cts?.Cancel(); // 请求外层循环停止
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }
                _driver = await OpenWebbrowser(account.localstorage);

                if (_driver == null)
                {
                    Logger.Log($"web群发线程==>浏览器初始化失败,重新尝试", Common.LogLevel.Warning);
                    return;
                }

                await Task.Delay(5000);

                var seachInputs = _driver.FindElements(By.CssSelector("input.input-field-input.is-empty.input-search-input.with-focus-effect"));
                for (int i = 0; i < 10; i++)
                {
                    if (seachInputs.Count == 0)
                    {
                        Logger.Log($"web群发线程==>未找到搜索框，等待5秒继续查找", Common.LogLevel.Warning);
                        await Task.Delay(5000);

                        var notLogin = _driver.FindElements(By.CssSelector("canvas._qrCanvas_1b0yp_186"));
                        if (notLogin.Count > 0)
                        {
                            Logger.Log($"web群发线程==>当前账号未登录，跳过当前账号", Common.LogLevel.Warning);
                            account.last_send_time = Common.Tools.Instance.GetTimeStamp();
                            await DbHelper.Db.Updateable<Models.webbrowser_accounts>(account).ExecuteCommandAsync();
                            return;
                        }
                    }
                    else
                    {
                        Logger.Log($"web群发线程==>已确认找到搜索框", Common.LogLevel.Warning);
                        break;
                    }
                }

                var seachInput = seachInputs[0];

                seachInput.Clear();
                seachInput.SendKeys(userData.username);


                await Task.Delay(5000);

                var searchResultUls = _driver.FindElements(By.CssSelector("ul.chatlist"));
                if (searchResultUls.Count == 0)
                {
                    Logger.Log($"web群发线程==>{userData.username} 搜索结果为空", Common.LogLevel.Warning);
                    return;
                }
                var aList = _driver.FindElements(By.CssSelector("a.rp.row.no-wrap.row-with-padding.row-clickable.hover-effect.chatlist-chat.chatlist-chat-abitbigger"));

                var found = false;

                foreach (var item in aList)
                {
                    if (found)
                    {
                        break;
                    }
                    var userNames = item.FindElements(By.CssSelector("div.row-subtitle.no-wrap"));
                    if (userNames.Count > 0)
                    {
                        foreach (var u in userNames)
                        {
                            if (u.Text.ToLower() == "@" + userData.username.ToLower())
                            {
                                item.Click();
                                await Task.Delay(5000);
                                found = true;
                                break;
                            }
                        }
                    }
                }
                if (!found)
                {
                    Logger.Log("web群发线程 ==> 未找到对应的用户，可能是用户名错误或用户不存在", Common.LogLevel.Error);
                    userData.status = -1;
                    userData.msg = "未找到对应的用户，可能是用户名错误或用户不存在";
                    await DbHelper.Db.Updateable<Models.collect_group_member_username>(userData).ExecuteCommandAsync();
                    return;
                }


                var sendInputs = _driver.FindElements(By.CssSelector("div.input-message-input.scrollable.scrollable-y.no-scrollbar.forwards"));
                if (sendInputs.Count == 0)
                {
                    Logger.Log("web群发线程 ==> 未找到发送框", Common.LogLevel.Error);
                    userData.status = -1;
                    userData.msg = "未找到发送框";
                    await DbHelper.Db.Updateable<Models.collect_group_member_username>(userData).ExecuteCommandAsync();
                    return;
                }

                sendInputs[0].SendKeys(sendContext);


                await Task.Delay(5000);

                var sendButtons = _driver.FindElements(By.CssSelector("button.btn-icon.rp.btn-circle.btn-send.animated-button-icon.send"));

                if (sendButtons.Count == 0)
                {
                    Logger.Log("web群发线程 ==> 未找到发送按钮", Common.LogLevel.Error);
                    userData.status = -1;
                    userData.msg = "未找到发送按钮";
                    await DbHelper.Db.Updateable<Models.collect_group_member_username>(userData).ExecuteCommandAsync();
                    return;
                }

                sendButtons[0].Click();

                await Task.Delay(5000);

                Logger.Log($"发送成功 username:{userData.username}", Common.LogLevel.Success);

                userData.status = 1;
                userData.send_time = Common.Tools.Instance.GetTimeStamp();
                userData.msg = $"发送成功,发送者账号{account.phonenumber}";

                var res = await DbHelper.Db.Updateable<Models.collect_group_member_username>(userData).ExecuteCommandAsync();


                await Task.Delay(5000);

            }
            catch (Exception ex)
            {
                Logger.Log($"web群发线程==>{ex.Message}", Common.LogLevel.Error);
            }
            finally
            {
                CloseBrowser();
                var min = 10;
                var max = 20;
                if (setting != null && setting.web_send_min > 0 && setting.web_send_max > 0)
                {
                    min = setting.web_send_min;
                    max = setting.web_send_max;
                }
                var time = new Random().Next(min, max);

                while (time > 0)
                {
                    Logger.Log($"{processName}休息{time}秒");
                    time--;
                    await Task.Delay(1000);
                }
            }
        }
        #endregion


        #region 协议采集线程
        private const int CollectTaskPollSeconds = 5;
        private const int CollectTaskCooldownSeconds = 20;
        private const int CollectPageSize = 200;

        private async void btnSessionCollectStart_Click(object sender, EventArgs e)
        {
            var button = btnSessionCollectStart;
            if (_isRunningSessionCollect)
            {
                var activeCts = _ctsSessionCollect;
                if (activeCts is null || activeCts.IsCancellationRequested)
                {
                    return;
                }

                button.Enabled = false;
                button.Text = "停 止 中...";
                activeCts.Cancel();
                return;
            }

            var cts = new CancellationTokenSource();
            _ctsSessionCollect = cts;
            _isRunningSessionCollect = true;
            button.Enabled = true;
            button.Text = "停 止 协 议 采 集";

            try
            {
                await CollectUserNameTask(cts.Token);
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                Logger.Log("协议采集已停止", Common.LogLevel.Info);
            }
            finally
            {
                if (ReferenceEquals(_ctsSessionCollect, cts))
                {
                    _ctsSessionCollect = null;
                    _isRunningSessionCollect = false;
                    button.Text = "开 始 协 议 采 集";
                    button.Enabled = true;
                }

                cts.Dispose();
                Logger.Log("协议采集线程已停止", Common.LogLevel.Info);
            }
        }

        private async Task CollectUserNameTask(CancellationToken cancellationToken)
        {
            Logger.Log("采集线程已启动。", Common.LogLevel.Info);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                WTelegram.Client? client = null;
                var completedTask = false;

                try
                {
                    var task = await DbHelper.Db.Queryable<collect_group_username>()
                        .Where(it => it.status == 1)
                        .OrderBy(it => it.id, OrderByType.Asc)
                        .FirstAsync();
                    if (task == null)
                    {
                        Logger.Log("暂无采集任务，协议采集线程停止中。", Common.LogLevel.Info);
                        return;
                    }
                    cancellationToken.ThrowIfCancellationRequested();

                    var sessionDirectory = Path.Combine(Application.StartupPath, "session", "collect");
                    var sessions = GetCurrentPageFiles(true, sessionDirectory);
                    if (sessions.Count == 0)
                    {
                        task.msg = "采集账号目录中没有可用 session 文件,线程停止中";
                        await DbHelper.Db.Updateable(task).ExecuteCommandAsync();
                        Logger.Log(task.msg, Common.LogLevel.Info);
                        return;
                    }

                    var settings = await DbHelper.Db.Queryable<settings>().FirstAsync();
                    if (settings == null || string.IsNullOrWhiteSpace(settings.api_id) || string.IsNullOrWhiteSpace(settings.api_hash))
                    {
                        task.msg = "未配置 Telegram API ID 或 API Hash,线程停止中";
                        await DbHelper.Db.Updateable(task).ExecuteCommandAsync();
                        Logger.Log(task.msg, Common.LogLevel.Info);
                        return;
                    }

                    var sessionPath = "";

                    if (string.IsNullOrEmpty(task.collect_phone))
                    {
                        sessionPath = sessions[Random.Shared.Next(sessions.Count)];
                    }
                    else
                    {
                        sessionPath = sessions.Where(it => it.Contains(task.collect_phone)).FirstOrDefault();
                    }

                    if (string.IsNullOrEmpty(sessionPath))
                    {
                        task.msg = "加载账号失败";
                        await DbHelper.Db.Updateable(task).ExecuteCommandAsync();
                        Logger.Log(task.msg, Common.LogLevel.Info);

                        return;
                    }

                    var phone = Path.GetFileNameWithoutExtension(sessionPath);
                    client = new WTelegram.Client(Config);

                    var vpn = await DbHelper.Db.Queryable<vpns>()
                        .Where(it => it.id == settings.default_vpn_id)
                        .FirstAsync();
                    if (vpn != null)
                    {
                        client.TcpHandler = async (address, port) =>
                        {
                            var proxy = new Socks5ProxyClient(vpn.host, Convert.ToInt32(vpn.port));
                            return proxy.CreateConnection(address, port);
                        };
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                    var currentUser = await client.LoginUserIfNeeded();
                    if (currentUser == null)
                    {
                        throw new InvalidOperationException($"采集账号 {phone} 登录失败。");
                    }

                    Logger.Log($"当前采集群聊{task.username} 群组ID:{task.group_id}", Common.LogLevel.Info);
                    InputChannel inputChannel = default;
                    if (string.IsNullOrEmpty(task.collect_phone))
                    {
                        var groupUsername = NormalizeGroupUsername(task.username);
                        var resolved = await client.Contacts_ResolveUsername(groupUsername);


                        Channel channel = resolved.Channel ?? throw new InvalidOperationException($"未找到可采集的超级群：{task.username}");
                        inputChannel = new InputChannel(channel.id, channel.access_hash);




                        var joinedChats = await client.Messages_GetAllChats();
                        var isJoined = joinedChats.chats.Values
                            .OfType<Channel>()
                            .Any(chat => chat.id == channel.id && !chat.flags.HasFlag(Channel.Flags.left));
                        if (!isJoined)
                        {
                            Logger.Log($"账号 {phone} 尚未加入 {task.username}，正在加入。", Common.LogLevel.Info);
                            var joinGroupResult = await client.Channels_JoinChannel(inputChannel);
                            if (joinGroupResult == null)
                            {
                                throw new InvalidOperationException($"加入群聊失败");
                            }
                        }
                    }
                    else
                    {
                        var allChats = await client.Messages_GetAllChats();
                        if (!allChats.chats.TryGetValue(task.group_id, out var chat) || chat is not Channel channel)
                        {
                            throw new InvalidOperationException(
                                $"当前账号未加入群组 {task.group_id}，或该群不是可采集成员的超级群。");
                        }

                        inputChannel = new InputChannel(channel.id, channel.access_hash);
                    }



                    cancellationToken.ThrowIfCancellationRequested();
                    var result = await CollectGroupUsernames(client, task.id, inputChannel, cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                    task.status = 2;
                    task.collect_time = Tools.Instance.GetTimeStamp();
                    var count = await DbHelper.Db.Queryable<Models.collect_group_member_username>().Where(it => it.collect_task_id == task.id).CountAsync();
                    var rate = (Decimal.Divide(count, result.MemberTotal) * 100).ToString("F2");
                    task.msg = $"本次新采集{result.IncrementalCount}个群成员，本群总成员{result.MemberTotal}个\n采集成功率{rate}%，其中已去重{result.ExistingMemberCount}个群成员";
                    await DbHelper.Db.Updateable(task).ExecuteCommandAsync();
                    completedTask = true;
                    Logger.Log(task.msg, Common.LogLevel.Success);

                    string Config(string what) => what switch
                    {
                        "api_id" => settings.api_id,
                        "api_hash" => settings.api_hash,
                        "phone_number" => phone,
                        "verification_code" => string.Empty,
                        "password" => string.Empty,
                        "session_pathname" => sessionPath,
                        _ => null
                    };
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Logger.Log($"采集线程发生错误：{ex}", Common.LogLevel.Error);

                    var activeTask = await DbHelper.Db.Queryable<collect_group_username>()
                        .Where(it => it.status == 1)
                        .OrderBy(it => it.id, OrderByType.Asc)
                        .FirstAsync();
                    if (activeTask != null)
                    {
                        activeTask.status = -1;
                        activeTask.msg = ex.ToString();
                        await DbHelper.Db.Updateable(activeTask).ExecuteCommandAsync();
                    }
                }
                finally
                {
                    client?.Dispose();
                    await Task.Delay(
                        TimeSpan.FromSeconds(completedTask ? CollectTaskCooldownSeconds : CollectTaskPollSeconds),
                        cancellationToken);
                }
            }
        }

        private static string NormalizeGroupUsername(string value)
        {
            var username = value.Trim();
            if (username.StartsWith("https://t.me/", StringComparison.OrdinalIgnoreCase))
            {
                username = username["https://t.me/".Length..];
            }
            else if (username.StartsWith("http://t.me/", StringComparison.OrdinalIgnoreCase))
            {
                username = username["http://t.me/".Length..];
            }

            username = username.TrimStart('@').Trim('/');
            if (string.IsNullOrWhiteSpace(username) || username.Contains('/'))
            {
                throw new ArgumentException("群用户名格式不正确，请填写 @group 或 https://t.me/group。");
            }

            return username;
        }

        private async Task<CollectGroupMemberResponse> CollectGroupUsernames(
            WTelegram.Client client,
            int taskId,
            InputChannel inputChannel,
            CancellationToken cancellationToken)
        {
            var processName = "协议采集线程==>";
            cancellationToken.ThrowIfCancellationRequested();
            var offset = 0;

            int MemberTotal = 0;//总成员数 已获取

            int IncrementalCount = 0;//净新增 已获取
            decimal SuccessRate = 0;//成功率
            int ExistingMemberCount = 0;//已存在成员数

            var fullInfo = await client.Channels_GetFullChannel(inputChannel);
            cancellationToken.ThrowIfCancellationRequested();
            if (fullInfo.full_chat is ChannelFull channelFull &&
                channelFull.flags.HasFlag(ChannelFull.Flags.has_participants_count))
            {
                MemberTotal = channelFull.participants_count;
            }

            var meId = client.UserId;

            var adminParticipants = await client.Channels_GetParticipants(inputChannel, new ChannelParticipantsAdmins(), 0, int.MaxValue, 0);
            var adminIds = adminParticipants.users.Select(u => u.Key).ToHashSet();
            if (!adminIds.Contains(meId))
            {
                adminIds.Add(meId);
            }
            while (true)
            {
                var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
                if (setting == null)
                {
                    Logger.Log($"配置文件不存在，无法正常运行采集县城", Common.LogLevel.Warning);
                    return new CollectGroupMemberResponse
                    {
                        ExistingMemberCount = ExistingMemberCount,
                        IncrementalCount = IncrementalCount,
                        MemberTotal = MemberTotal,
                        SuccessRate = SuccessRate
                    };
                }
                cancellationToken.ThrowIfCancellationRequested();
                var task = await DbHelper.Db.Queryable<collect_group_username>()
                    .Where(it => it.id == taskId)
                    .FirstAsync();
                if (task == null || task.status != 1)
                {
                    Logger.Log($"采集任务意外停止,已返回现有数据", Common.LogLevel.Warning);
                    return new CollectGroupMemberResponse
                    {
                        ExistingMemberCount = ExistingMemberCount,
                        IncrementalCount = IncrementalCount,
                        MemberTotal = MemberTotal,
                        SuccessRate = SuccessRate
                    };
                }
                var page = await client.Channels_GetParticipants(
                    inputChannel,
                    new ChannelParticipantsRecent(),
                    offset,
                    CollectPageSize,
                    0);
                if (MemberTotal == 0 || page.count > MemberTotal)
                {
                    MemberTotal = page.count;
                }
                cancellationToken.ThrowIfCancellationRequested();
                if (page == null || page.users.Count == 0)
                {
                    return new CollectGroupMemberResponse
                    {
                        ExistingMemberCount = ExistingMemberCount,
                        IncrementalCount = IncrementalCount,
                        MemberTotal = MemberTotal,
                        SuccessRate = SuccessRate
                    };
                }

                var users = page.users.Values
                    .Where(user => !string.IsNullOrWhiteSpace(user.username) && !adminIds.Contains(user.id))
                    .GroupBy(user => user.id)
                    .Select(group => group.First())
                    .ToList();
                var userIds = users.Select(user => user.id).ToList();
                var existingIds = userIds.Count == 0
                    ? new HashSet<long>()
                    : (await DbHelper.Db.Queryable<collect_group_member_username>()
                        .Where(item => userIds.Contains(item.telegram_user_id))
                        .Select(item => item.telegram_user_id)
                        .ToListAsync()).ToHashSet();
                ExistingMemberCount += existingIds.Count;


                var newUsernames = new List<collect_group_member_username>();
                foreach (var user in users.Where(user => !existingIds.Contains(user.id)))
                {
                    var status = 0;
                    if (setting != null && (setting.session_collect_filter_bot_enabled && user.IsBot))
                    {
                        status = -100;
                    }

                    if (setting != null && (setting.session_collect_filter_manager_enabled && user.IsBot))
                    {
                        status = -99;
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                    byte[]? avatarBytes = null;
                    if (setting.session_collect_avatar_enabled)
                    {
                        avatarBytes = await GetProfilePhotoBytes(client, user, cancellationToken);
                    }
                    cancellationToken.ThrowIfCancellationRequested();




                    if (setting != null && setting.session_collect_nick_filter_enabled)
                    {
                        var nickFilter = setting.session_collect_nick_filter_context.Split("|");
                        var nick = (user.first_name + user.last_name);
                        if (nickFilter.Any(it => nick.Contains(it)))
                        {
                            status = -2;
                            Logger.Log($"{processName}跳过用户【{user.username}】原因：昵称内容非法【{nick}】");
                        }
                    }



                    newUsernames.Add(new collect_group_member_username
                    {
                        collect_task_id = taskId,
                        telegram_user_id = user.id,
                        username = user.username,
                        create_time = Tools.Instance.GetTimeStamp(),
                        status = status,
                        msg = "",
                        send_time = 0,
                        first_name = user.first_name == null ? "" : user.first_name,
                        last_name = user.last_name == null ? "" : user.last_name,
                        head_image = avatarBytes,
                        SourceType = 2
                    });
                }


                if (newUsernames.Count > 0)
                {
                    IncrementalCount += await DbHelper.Db.Insertable(newUsernames).ExecuteCommandAsync();

                }

                var participantCount = page.participants.Length;
                if (participantCount == 0)
                {
                    return new CollectGroupMemberResponse
                    {
                        ExistingMemberCount = ExistingMemberCount,
                        IncrementalCount = IncrementalCount,
                        MemberTotal = MemberTotal,
                        SuccessRate = SuccessRate
                    };
                }

                offset += participantCount;
                if (offset >= page.count)
                {
                    return new CollectGroupMemberResponse
                    {
                        ExistingMemberCount = ExistingMemberCount,
                        IncrementalCount = IncrementalCount,
                        MemberTotal = MemberTotal,
                        SuccessRate = SuccessRate
                    };
                }

                Logger.Log($"{task.username}采集报告 采集进度{offset}/{page.count} 有效用户共{page.count}人 群总人数{MemberTotal}人 本页共{page.users.Count}人  有username{userIds.Count}人  重复{existingIds.Count}人 成功采集{IncrementalCount}人");
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
        private static async Task<byte[]?> GetProfilePhotoBytes(
        WTelegram.Client client,
        User user,
        CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user.photo == null)
            {
                return null;
            }
            try
            {
                await using var stream = new MemoryStream();
                await client.DownloadProfilePhotoAsync(
                    user,
                    stream,
                    big: false);
                cancellationToken.ThrowIfCancellationRequested();
                return stream.Length == 0 ? null : stream.ToArray();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log($"头像下载失败：{user.id}，{ex.Message}", Common.LogLevel.Warning);
                return null;
            }
        }
        private async Task MarkCollectTaskFailed(collect_group_username task, string message)
        {
            //task.status = -1;
            //task.msg = message;
            //await DbHelper.Db.Updateable(task).ExecuteCommandAsync();
            Logger.Log($"群 {task.username} 采集失败：{message}", Common.LogLevel.Error);
        }
        #endregion

        #region 协议群发相关

        private CancellationTokenSource _ctsSessionSend;
        private bool _isRunningSessionSend;
        private async void btnSessionSendMsgStart_Click(object sender, EventArgs e)
        {
            var dom = btnSessionSendMsgStart;

            if (!_isRunningSessionSend)
            {
                _isRunningSessionSend = true;
                _ctsSessionSend = new CancellationTokenSource();
                dom.Text = "停 止 协 议 群 发";

                var cancellationToken = _ctsSessionSend.Token;

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        if (!await SendMessageBySession(cancellationToken))
                        {
                            break;
                        }

                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Log("协议群发已停止", Common.LogLevel.Info);
                }
                finally
                {
                    _isRunningSessionSend = false;
                    _ctsSessionSend?.Dispose();
                    _ctsSessionSend = null;
                    dom.Text = "开 始 协 议 群 发";
                    dom.Enabled = true;
                    Logger.Log($"协议群发线程已停止", Common.LogLevel.Warning);
                }

                return;
            }

            dom.Enabled = false;
            dom.Text = "停 止 中...";
            _ctsSessionSend?.Cancel();
        }



        // 修改函数


        private async Task<bool> SendMessageBySession(CancellationToken cancellationToken)
        {
            var processName = "群发线程==>";
            WTelegram.Client client = default;

            var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                cancellationToken.ThrowIfCancellationRequested();
                if (setting == null)
                {
                    Logger.Log($"{processName}基础设置数据不存在,无法启动线程,线程正在停止", Common.LogLevel.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(setting.api_id) || string.IsNullOrEmpty(setting.api_hash))
                {
                    Logger.Log($"{processName}请先配置api_id,api_hash,无法启动线程,线程正在停止", Common.LogLevel.Error);
                    return false;
                }

                var userNames = await DbHelper.Db.Queryable<Models.collect_group_member_username>().Where(it => it.status == 0).FirstAsync();
                cancellationToken.ThrowIfCancellationRequested();

                var accountPath = setting.default_account_path;
                if (string.IsNullOrEmpty(accountPath))
                {
                    Logger.Log($"{processName}群发账号路劲不存在,无法启动线程,线程正在停止", Common.LogLevel.Error);
                    return false;
                }




                var date = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));

                var accountFileList = new DirectoryInfo(accountPath)
          .GetFiles()
          .Select(f => new
          {
              FileName = f.Name,
              LastAccessTime = f.LastAccessTime,
              FilePath = f.FullName
          })
          .ToList();


                if (accountFileList == null || accountFileList.Count == 0)
                {
                    Logger.Log($"{processName}暂无可用账号,线程正在停止", Common.LogLevel.Warning);
                    return false;
                }

                var sendLogList = await DbHelper.Db.Queryable<Models.session_send_log>()
                    .Where(it => it.send_date == date)
                    .GroupBy(it => it.send_phone)
                    .Select(it => new
                    {
                        phone = it.send_phone,
                        count = SqlFunc.AggregateCount(it.id)
                    })
                    .ToListAsync();


                var accounts = accountFileList.Select(f => new
                {
                    Account = f.FileName,
                    LastAccessTime = f.LastAccessTime,
                    TodayCount = sendLogList.FirstOrDefault(s => s.phone == f.FileName)?.count ?? 0,
                    FilePath = f.FilePath
                }).ToList();

                // 选择发送次数最少、文件最旧的账号
                var selected = accounts
                    .OrderBy(a => a.TodayCount)
                    .ThenBy(a => a.LastAccessTime)
                    .First();

                if (selected == null || selected.TodayCount >= setting.session_send_count)
                {
                    Logger.Log($"{processName}所有账号已达发送上限,线程正在停止", Common.LogLevel.Warning);
                    return false;
                }

                client = new WTelegram.Client(Config);


                if (setting.default_vpn_id > 0)
                {
                    var vpn = await DbHelper.Db.Queryable<Models.vpns>().Where(it => it.id == setting.default_vpn_id).FirstAsync();
                    client.TcpHandler = async (address, port) =>
                    {
                        var proxy = new Socks5ProxyClient(vpn.host, Convert.ToInt32(vpn.port));
                        return proxy.CreateConnection(address, port);
                    };
                }

                cancellationToken.ThrowIfCancellationRequested();
                var user = client.LoginUserIfNeeded();

                var sendTexts = setting.session_send_context.Split("|");

                var sendContext = sendTexts[new Random().Next(0, sendTexts.Length)];


                var userName = await DbHelper.Db.Queryable<Models.collect_group_member_username>().Where(it => it.status == 0).FirstAsync();
                if (userName == null)
                {
                    Logger.Log($"{processName}暂无可用 UserName,线程正在停止", Common.LogLevel.Warning);
                    return false;
                }



                var logAddData = new Models.session_send_log
                {
                    msg = "发送成功",
                    status = 1,

                    send_date = date,
                    send_message = sendContext,
                    send_phone = selected.Account,
                    send_time = Common.Tools.Instance.GetTimeStamp(),
                    send_type = 2,
                    username = userName.username
                };

                userName.send_time = Common.Tools.Instance.GetTimeStamp();
                userName.SourceType = 2;
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var resolved = await client.Contacts_ResolveUsername(userName.username);
                    cancellationToken.ThrowIfCancellationRequested();
                    var sendResult = await client.SendMessageAsync(resolved, sendContext);
                    cancellationToken.ThrowIfCancellationRequested();
                    Logger.Log($"{processName}{selected.Account}发送给{userName.username}成功", Common.LogLevel.Success);

                    userName.status = 1;
                    userName.msg = $"发送成功 发送者【{selected.Account}】 接受者【{userName.username}】";
                }
                catch (Exception ex)
                {
                    logAddData.msg = ex.ToString();
                    logAddData.status = -1;


                    userName.status = -1;
                    userName.msg = $"发送者【{selected.Account}】 接受者【{userName.username}】" + logAddData.msg;
                    Logger.Log($"{processName}{selected.Account}发送给{userName.username}失败,内容{sendContext}", Common.LogLevel.Error);
                    Logger.Log($"失败原因{logAddData.msg}", Common.LogLevel.Error);
                }

                var result = await DbHelper.Db.Insertable(logAddData).ExecuteCommandAsync();

                await DbHelper.Db.Updateable<Models.collect_group_member_username>(userName).ExecuteCommandAsync();

                string Config(string what)
                {
                    switch (what)
                    {
                        case "api_id": return setting.api_id;
                        case "api_hash": return setting.api_hash;
                        case "phone_number":
                            {
                                return selected.Account;
                            }
                        case "verification_code": return "";
                        case "password": return "";
                        case "session_pathname":
                            {
                                return selected.FilePath;
                            }
                        default: return null;
                    }
                }

                return true;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log($"{processName}{ex}", Common.LogLevel.Error);
                return false;
            }
            finally
            {
                client?.Dispose();

                var delay = 0;
                if (setting == null || setting.session_send_min == 0 || setting.session_send_max == 0)
                {
                    setting = new settings
                    {
                        session_send_min = 30,
                        session_send_max = 60
                    };
                }
                delay = new Random().Next(setting.session_send_min, setting.session_send_max);
                while (delay > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Logger.Log($"{processName}上一轮发送完成,{delay}秒后继续发送");
                    await Task.Delay(1000);
                    delay--;
                }
            }














        }


        #endregion


        private async void btnCollectUserNameUp_Click(object sender, EventArgs e)
        {
            _currentCollectUserNamePage--;
            await LoadCollectUserName();
        }

        private async void btnCollectUserNameDown_Click(object sender, EventArgs e)
        {
            _currentCollectUserNamePage++;
            await LoadCollectUserName();
        }

        private async void btnCollectUsernameSeach_Click(object sender, EventArgs e)
        {
            await LoadCollectUserName();
        }

        #region 浏览器采集相关 
        public async Task LoadWebCollectGroup()
        {
            try
            {
                var dom = lsWebCollect;
                var page = _currentWebCollectGroupPage;
                var search = txtWebCollectSearch.Text.Trim();
                var pageSize = _pageSize;

                // 查询分页数据
                var data = DbHelper.Db.Queryable<webbrowser_collect_username>();
                if (search.Length > 0)
                {
                    data.Where(it => it.username.Contains(search));
                }

                var list = await data.OrderBy(it => it.id, OrderByType.Desc)
                                           .ToPageListAsync(page, pageSize);

                // 获取总数
                var total = await DbHelper.Db.Queryable<webbrowser_collect_username>().Where(it => it.username.Contains(search)).CountAsync();

                dom.BeginUpdate();
                dom.Items.Clear();


                var result = await DbHelper.Db.Queryable<Models.collect_group_member_username>()
     .GroupBy(it => it.collect_task_id)
     .Select(it => new
     {
         task_id = it.collect_task_id,
         count = SqlFunc.AggregateCount(it.username)
     })
     .ToListAsync();
                foreach (var item in list)
                {
                    if (item.id == 75)
                    {

                    }
                    var countGroup = result.Where(it => it.task_id == item.id).FirstOrDefault();

                    var row = new ListViewItem(item.id.ToString())
                    {
                        ImageIndex = 0
                    };
                    row.SubItems.Add(item.username.ToString());
                    row.SubItems.Add(item.status == 0 ? "待采集" : item.status == 1 ? "采集中" : item.status == 2 ? "已采集" : "采集失败");
                    row.SubItems.Add(Common.Tools.Instance.TimestampToDateTime(item.create_time).ToString());
                    row.SubItems.Add(item.collect_time == 0 ? "暂无记录" : Common.Tools.Instance.TimestampToDateTime(item.collect_time).ToString());

                    row.SubItems.Add(item.group_total_count.ToString());


                    row.SubItems.Add(countGroup != null ? countGroup.count.ToString() : "0");
                    row.SubItems.Add(item.msg);

                    row.Tag = item;

                    dom.Items.Add(row);
                }


                dom.EndUpdate();

                // 更新分页信息
                int totalPages = (int)Math.Ceiling((double)total / pageSize);
                lbWebCollectText.Text = $"{page}/{totalPages}";
                btnWebCollectPrev.Enabled = page > 1;
                btnWebCollectNext.Enabled = page < totalPages;
            }
            catch (Exception ex)
            {
                Logger.Log($"浏览器采集页==>报错{ex.ToString()}", Common.LogLevel.Error);
            }
        }
        private void btnWebCollectAdd_Click(object sender, EventArgs e)
        {
            AddWebBrowserCollectForm form = new AddWebBrowserCollectForm(this);
            form.ShowDialog();
        }
        private async void btnWebCollectSearch_Click(object sender, EventArgs e)
        {
            await LoadWebCollectGroup();
        }

        private async void btnWebCollectPrev_Click(object sender, EventArgs e)
        {
            _currentWebCollectGroupPage--;
            await LoadWebCollectGroup();
        }

        private async void btnWebCollectNext_Click(object sender, EventArgs e)
        {
            _currentWebCollectGroupPage++;
            await LoadWebCollectGroup();
        }

        private async void btnWebCollectGoto_Click(object sender, EventArgs e)
        {
            _currentWebCollectGroupPage = Convert.ToInt32(txtWebCollectGoto.Text);

            await LoadWebCollectGroup();
        }


        private CancellationTokenSource? _ctsWeb;
        private Task? _webCollectTask;
        private bool _isRunningWeb;

        private async void btnWebbrowserCollectStart_Click(object sender, EventArgs e)
        {
            var button = btnWebbrowserCollectStart;
            if (_isRunningWeb)
            {
                button.Enabled = false;
                button.Text = "停 止 中...";
                _ctsWeb?.Cancel();

                try
                {
                    await (_webCollectTask ?? Task.CompletedTask);
                }
                catch (OperationCanceledException)
                {
                    // 取消是正常停止路径。
                }
                return;
            }

            var cancellationSource = new CancellationTokenSource();
            _ctsWeb = cancellationSource;
            _isRunningWeb = true;
            button.Text = "停 止 采 集";
            _webCollectTask = RunWebCollectAsync(cancellationSource);
        }

        private async Task RunWebCollectAsync(CancellationTokenSource cancellationSource)
        {
            var cancellationToken = cancellationSource.Token;
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await WebBrowserCollect(cancellationToken);
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Logger.Log("Web 采集已停止", Common.LogLevel.Info);
            }
            catch (Exception ex)
            {
                Logger.Log($"Web 采集已异常停止：{ex}", Common.LogLevel.Error);
            }
            finally
            {
                CloseBrowser();
                if (_ctsWeb == cancellationSource)
                {
                    _isRunningWeb = false;
                    _ctsWeb = null;
                    _webCollectTask = null;
                    cancellationSource.Dispose();

                    if (!IsDisposed && IsHandleCreated)
                    {
                        try
                        {
                            BeginInvoke(new Action(() =>
                            {
                                btnWebbrowserCollectStart.Text = "开 始 采 集";
                                btnWebbrowserCollectStart.Enabled = true;
                            }));
                        }
                        catch (InvalidOperationException)
                        {
                            // 窗体关闭过程中不再更新 UI。
                        }
                    }
                }
            }
        }


        /// <summary>
        /// web端 头像url 转 byte
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="blobUrl"></param>
        /// <returns></returns>
        private static byte[]? ReadBlobImageBytes(IWebDriver driver, string blobUrl)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(blobUrl) ||
                    !blobUrl.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                var executor = (IJavaScriptExecutor)driver;
                var base64 = executor.ExecuteAsyncScript(@"
        const [url, done] = arguments;
        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`读取头像失败：${response.status}`);
                }
                return response.arrayBuffer();
            })
            .then(buffer => {
                const bytes = new Uint8Array(buffer);
                let binary = '';
                for (let i = 0; i < bytes.length; i++) {
                    binary += String.fromCharCode(bytes[i]);
                }
                done(btoa(binary));
            })
            .catch(error => {
                console.error(error);
                done(null);
            });
    ", blobUrl) as string;
                return string.IsNullOrWhiteSpace(base64)
                    ? null
                    : Convert.FromBase64String(base64);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static async Task WaitForWebConditionAsync(
            Func<bool> condition,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            var deadline = DateTime.UtcNow + timeout;
            while (DateTime.UtcNow < deadline)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (condition())
                {
                    return;
                }

                await Task.Delay(TimeSpan.FromMilliseconds(200), cancellationToken);
            }

            throw new TimeoutException("等待 Telegram Web 页面状态超时。");
        }

        private async Task WebBrowserCollect(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var task = await DbHelper.Db.Queryable<Models.webbrowser_collect_username>().Where(it => it.status == 1).FirstAsync();
                if (task == null)
                {
                    Logger.Log("当前暂无可用采集群组,已停止采集线程", Common.LogLevel.Error);

                    _ctsWeb?.Cancel(); // 请求外层循环停止
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }
                var user = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().Where(it => it.id == task.account_id).FirstAsync();
                if (user == null)
                {
                    Logger.Log("采集任务相关绑定的账户不可用", Common.LogLevel.Warning);
                    task.msg = $"采集任务相关绑定的账户不可用";
                    task.status = -1;
                    await DbHelper.Db.Updateable<Models.webbrowser_collect_username>(task).ExecuteCommandAsync();
                    return;
                }


                _driver = await OpenWebbrowser(user.localstorage);

                if (_driver == null)
                {
                    throw new InvalidOperationException("Web 采集浏览器初始化失败。");
                }





                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                if (!task.username.contains("https://web.telegram.org/k/#"))
                {

                    var seachInputs = _driver.FindElements(By.CssSelector("input.input-field-input.is-empty.input-search-input.with-focus-effect"));
                    for (int i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        seachInputs = _driver.FindElements(By.CssSelector("input.input-field-input.is-empty.input-search-input.with-focus-effect"));
                        if (seachInputs.Count > 0)
                        {
                            Logger.Log("web采集线程==>已确认找到搜索框", Common.LogLevel.Info);
                            break;
                        }

                        Logger.Log("web采集线程==>未找到搜索框，等待5秒继续查找", Common.LogLevel.Warning);
                        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                        var notLogin = _driver.FindElements(By.CssSelector("canvas._qrCanvas_1b0yp_186"));
                        if (notLogin.Count > 0)
                        {
                            throw new InvalidOperationException("当前 Web 账号登录已失效。");
                        }
                    }

                    if (seachInputs.Count == 0)
                    {
                        throw new InvalidOperationException("等待搜索框超时。");
                    }

                    var seachInput = seachInputs[0];

                    seachInput.Clear();
                    seachInput.SendKeys(task.username);


                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                    var searchResultUls = _driver.FindElements(By.CssSelector("ul.chatlist"));
                    if (searchResultUls.Count == 0)
                    {
                        Logger.Log($"web群发线程==>{task.username} 搜索结果为空", Common.LogLevel.Warning);
                        return;
                    }
                    var aList = _driver.FindElements(By.CssSelector("a.rp.row.no-wrap.row-with-padding.row-clickable.hover-effect.chatlist-chat.chatlist-chat-abitbigger"));


                    var found = false;

                    foreach (var item in aList)
                    {
                        if (found)
                        {
                            break;
                        }
                        var userNames = item.FindElements(By.CssSelector("div.row-subtitle.no-wrap"));
                        if (userNames.Count > 0)
                        {
                            foreach (var u in userNames)
                            {
                                Logger.Log(u.Text.ToLower());
                                if (u.Text.ToLower().Contains("@" + task.username.ToLower()))
                                {
                                    item.Click();
                                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        Logger.Log("web群发线程 ==> 未找到对应的用户，可能是用户名错误或用户不存在", Common.LogLevel.Error);
                        task.status = -1;
                        task.msg = "未找到对应的用户，可能是用户名错误或用户不存在";
                        await DbHelper.Db.Updateable<Models.webbrowser_collect_username>(task).ExecuteCommandAsync();
                        return;
                    }


                }
                else
                {
                    //// 打开新标签页
                    //_driver.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Control + "t");

                    //// 切换到新标签
                    //_driver.SwitchTo().Window(_driver.WindowHandles.Last());

                    // 导航到目标地址

                    _driver.Navigate().GoToUrl(task.username);
                    await Task.Delay(1 * 1000);
                    _driver.Navigate().GoToUrl(task.username);

                    await Task.Delay(5 * 1000);
                }



                var groupTitle = _driver.FindElements(By.CssSelector("div.chat-info"));

                var totalCount = 0;//总人数

                if (groupTitle.Count > 0)
                {
                    groupTitle[0].Click();

                    var groupInfo = groupTitle[0].FindElements(By.CssSelector("span.i18n"));
                    if (groupInfo.Count > 0)
                    {
                        foreach (var infoItem in groupInfo)
                        {
                            if (infoItem.Text.contains("members"))
                            {
                                totalCount = Convert.ToInt32(infoItem.Text.replace("members", "").replace(" ", ""));

                                task.group_total_count = totalCount;
                                await DbHelper.Db.Updateable<Models.webbrowser_collect_username>(task).ExecuteCommandAsync();

                                break;
                            }
                        }
                    }
                }
                else
                {
                    Logger.Log("web群发线程 ==> 未找到对应的用户，可能是用户名错误或用户不存在", Common.LogLevel.Error);
                    task.status = -1;
                    task.msg = "未找到对应的用户，可能是用户名错误或用户不存在";
                    await DbHelper.Db.Updateable<Models.webbrowser_collect_username>(task).ExecuteCommandAsync();
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                // 定位列表容器
                var listContainer = _driver.FindElements(By.CssSelector("div.scrollable.scrollable-y"));

                var alistCount = 0;
                var errCount = 0;
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var Container = listContainer[4];

                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", Container);

                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);


                    var alist = Container.FindElements(By.CssSelector("a.row.no-wrap.row-with-padding.row-clickable.hover-effect.chatlist-chat.chatlist-chat-abitbigger"));

                    if (alistCount == alist.Count)
                    {
                        errCount += 1;
                        if (errCount > 5)
                        {
                            task.status = 2;
                            task.msg = "采集完成";
                            task.collect_success_count = await DbHelper.Db.Queryable<Models.collect_group_member_username>().Where(it => it.collect_task_id == task.id).CountAsync();
                            task.group_total_count = totalCount;
                            await DbHelper.Db.Updateable<Models.webbrowser_collect_username>(task).ExecuteCommandAsync();
                            break;
                        }
                    }
                    else
                    {
                        errCount = 0;
                        alistCount = alist.Count;
                    }

                    foreach (var item in alist)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                        var peerid = item.GetAttribute("data-peer-id");



                        var any = await DbHelper.Db.Queryable<Models.webbrowser_collect_log>().Where(it => it.peer_id == peerid).AnyAsync();

                        if (any)
                        {
                            continue;
                        }
                        else
                        {

                            string originalWindow = _driver.CurrentWindowHandle;

                            ((IJavaScriptExecutor)_driver).ExecuteScript(@"var peerid = arguments[0].getAttribute('data-peer-id');arguments[0].innerHTML+=`<a href=""https://web.telegram.org/k/#${peerid}"" target=""_blank"" class='new-open-chat'>打开聊天</a>`", item);

                            var openChat = item.FindElement(By.CssSelector("a.new-open-chat"));


                            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", openChat);


                            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                            await WaitForWebConditionAsync(
                                () => _driver.WindowHandles.Count > 1,
                                TimeSpan.FromSeconds(10),
                                cancellationToken);

                            // 4. 切换到新标签页
                            string newWindow = _driver.WindowHandles[^1];
                            _driver.SwitchTo().Window(newWindow);

                            // 5. 等待页面加载完成
                            await WaitForWebConditionAsync(
                                () => ((IJavaScriptExecutor)_driver)
                                    .ExecuteScript("return document.readyState")
                                    .ToString() == "complete",
                                TimeSpan.FromSeconds(10),
                                cancellationToken);


                            var memberTitle = _driver.FindElements(By.CssSelector("div.chat-info"));


                            var nick = "";
                            byte[] imageUrl = null;
                            var userName = "";
                            var info = "";
                            if (memberTitle.Count > 0)
                            {

                                memberTitle[0].Click();


                                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                                //获取username


                                var memberTitleUserName = _driver.FindElements(By.CssSelector("div.sidebar-left-section-content"));
                                if (memberTitleUserName.Count > 0)
                                {
                                    var memberTitleUserName_1 = memberTitleUserName[0].FindElements(By.CssSelector("div.rp.row.row-with-icon.row-with-padding.row-clickable.hover-effect"));
                                    foreach (var userNameItem in memberTitleUserName_1)
                                    {
                                        var memberTitleUserName_2 = userNameItem.FindElements(By.CssSelector("div.row-subtitle"));
                                        if (memberTitleUserName_2.Count > 0)
                                        {
                                            var titleText = memberTitleUserName_2[0].Text;
                                            if (titleText == "Username" || titleText == "用户名")
                                            {
                                                //确定存在用户名
                                                var realUserNameHtml = userNameItem.FindElements(By.CssSelector("div.row-title"));
                                                userName = realUserNameHtml.Count > 0 ? realUserNameHtml[0].Text : "";

                                            }
                                            else if (titleText == "个人简介" || titleText == "Bio")
                                            {
                                                var realUserNameHtml = userNameItem.FindElements(By.CssSelector("div.row-title"));
                                                info = realUserNameHtml.Count > 0 ? realUserNameHtml[0].Text : "";
                                            }
                                        }
                                    }


                                    if (userName != "Notifications" && userName != "My Notes" && userName.Length > 0)
                                    {
                                        //获取昵称头像
                                        var memberTitle_nick = memberTitle[0].FindElements(By.CssSelector("div.user-title"));

                                        if (memberTitle_nick.Count > 0)
                                        {
                                            nick = memberTitle_nick[0].Text;
                                        }


                                        var memberTitle_image = memberTitle[0].FindElements(By.CssSelector("img.avatar-photo"));
                                        if (memberTitle_image.Count > 0)
                                        {
                                            imageUrl = ReadBlobImageBytes(_driver, memberTitle_image[0].GetAttribute("src"));
                                        }


                                    }
                                    else
                                    {
                                        userName = "";
                                    }

                                }

                            }


                            if (userName.Length > 0)
                            {
                                if (!await DbHelper.Db.Queryable<collect_group_member_username>().Where(ex => ex.username.ToLower() == userName.ToLower() || ex.telegram_user_id == Convert.ToInt64(peerid)).AnyAsync())
                                {
                                    var status = 0;
                                    var setting = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();

                                    if (setting != null && setting.web_collect_nick_filter_enabled)
                                    {
                                        var nickRule = setting.web_collect_nick_filter_context.Split("|").ToList();
                                        if (nickRule.Any(it => nick.Contains(it)))
                                        {
                                            status = -2;
                                        }
                                    }

                                    if (setting != null && setting.web_collect_info_filter_enabled)
                                    {
                                        var nickRule = setting.web_collect_info_filter_context.Split("|").ToList();
                                        if (nickRule.Any(it => info.Contains(it)))
                                        {
                                            status = -3;
                                        }
                                    }
                                    var res = await DbHelper.Db.Insertable<Models.collect_group_member_username>(new Models.collect_group_member_username
                                    {
                                        collect_task_id = task.id,
                                        create_time = Common.Tools.Instance.GetTimeStamp(),
                                        first_name = nick,
                                        last_name = "",
                                        head_image = imageUrl,
                                        msg = "",
                                        send_time = 0,
                                        SourceType = 1,
                                        status = status,
                                        telegram_user_id = Convert.ToInt64(peerid),
                                        username = userName
                                    }).ExecuteCommandAsync();
                                    Logger.Log($"{userName}采集成功", Common.LogLevel.Success);
                                }
                                else
                                {
                                    Logger.Log($"{userName}已存在跳过采集", Common.LogLevel.Success);
                                }


                            }

                            await DbHelper.Db.Insertable<Models.webbrowser_collect_log>(new Models.webbrowser_collect_log
                            {
                                peer_id = peerid,
                                task_id = task.id,
                            }).ExecuteCommandAsync();

                            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                            _driver.Close();

                            _driver.SwitchTo().Window(originalWindow);

                        }
                    }
                }


            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log($"web采集线程==>{ex}", Common.LogLevel.Error);
                throw;
            }
            finally
            {
                CloseBrowser();
            }
        #endregion




        }

        private void llabGetApi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://my.telegram.org/auth?to=apps",
                UseShellExecute = true  // 关键：使用系统 shell 打开
            };
            System.Diagnostics.Process.Start(psi);
        }


        #region 协议 已加入群聊 采集


        #endregion


        private async void btnCollectUserNameGoto_Click(object sender, EventArgs e)
        {
            _currentCollectUserNamePage = Convert.ToInt32(txtCollectUserNameGoto.Text);
            await LoadCollectUserName();
        }


    }
}