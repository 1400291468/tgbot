using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using ListView = System.Windows.Forms.ListView;
using TextBox = System.Windows.Forms.TextBox;

namespace TGForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabPageSetting = new TabPage();
            label31 = new Label();
            label30 = new Label();
            label29 = new Label();
            label25 = new Label();
            label19 = new Label();
            label28 = new Label();
            label27 = new Label();
            label24 = new Label();
            label22 = new Label();
            label21 = new Label();
            label26 = new Label();
            llabGetApi = new LinkLabel();
            label23 = new Label();
            groupBox5 = new GroupBox();
            cboxWebCollectInfoFilter = new CheckBox();
            cboxWebCollectNickFilter = new CheckBox();
            txtWebCollectInfoFilter = new TextBox();
            txtWebCollectNickFilter = new TextBox();
            groupBox4 = new GroupBox();
            cboxSessionCollectFilterBot = new CheckBox();
            cboxSessionCollectFilterManager = new CheckBox();
            cboxSessionSetingDownloadAvatar = new CheckBox();
            cboxSessionCollectInfoFilter = new CheckBox();
            cboxSessionCollectNickFilter = new CheckBox();
            txtSessionCollectInfoFilter = new TextBox();
            txtSessionCollectNickFilter = new TextBox();
            groupBox3 = new GroupBox();
            txtSessionSendMsgContexts = new TextBox();
            label20 = new Label();
            label16 = new Label();
            txtSessionSendMsgCountMax = new TextBox();
            txtSessionSendMsgDelayMaxs = new TextBox();
            txtSessionSendMsgDelayMins = new TextBox();
            label15 = new Label();
            gbWebGroupManagerSettings = new GroupBox();
            txtWebGroupManagerSendMaxCount = new TextBox();
            label14 = new Label();
            txtWebManagerSendContext = new TextBox();
            label13 = new Label();
            txtWebGroupManagerSendMax = new TextBox();
            txtWebGroupManagerSendMin = new TextBox();
            label4 = new Label();
            groupBox2 = new GroupBox();
            cboSettingVpn = new ComboBox();
            label9 = new Label();
            btnSettingSave = new Button();
            groupBox1 = new GroupBox();
            label8 = new Label();
            txtApiHash = new TextBox();
            label7 = new Label();
            txtApiId = new TextBox();
            tabPageAccount = new TabPage();
            lbAccountSelectPath = new Label();
            btnAccountSelecetPath = new Button();
            txtAccountSeach = new TextBox();
            btnAccountRefresh = new Button();
            btnAddAccount = new Button();
            btnAccountGoto = new Button();
            lbAccount = new Label();
            txtAccountGoto = new TextBox();
            btcAccountNext = new Button();
            btnAccountUp = new Button();
            label6 = new Label();
            btnAccountSeach = new Button();
            lvAccounts = new ListView();
            tabPageCollectTask = new TabPage();
            label5 = new Label();
            txtCollectAdd = new TextBox();
            btnCollectAdd = new Button();
            btnSessionCollectStart = new Button();
            button2 = new Button();
            lbCollectTaskTxt = new Label();
            textBox1 = new TextBox();
            btnCollectTaskNext = new Button();
            btnCollectTaskPrev = new Button();
            label11 = new Label();
            txtCollectTaskSeach = new TextBox();
            btnCollectTaskSeach = new Button();
            lvCollect = new ListView();
            tabPageCollectMember = new TabPage();
            cmbMemberOrderBy = new ComboBox();
            cmbMemberP = new ComboBox();
            btnWebbrowserSendMsg = new Button();
            btnSessionSendMsgStart = new Button();
            lbCollectUserNameShowCount = new Label();
            btnCollectUserNameGoto = new Button();
            lbCollectUsername = new Label();
            txtCollectUserNameGoto = new TextBox();
            btnCollectUserNameDown = new Button();
            btnCollectUserNameUp = new Button();
            label17 = new Label();
            txtCollectUserNameSeach = new TextBox();
            btnCollectUsernameSeach = new Button();
            lsCollectUserName = new ListView();
            tabPageVpn = new TabPage();
            btnVpnAdd = new Button();
            btnPageGoto = new Button();
            lblVpnListPage = new Label();
            txtPageGoto = new TextBox();
            btnNext = new Button();
            btnPrev = new Button();
            label3 = new Label();
            txtRemarkSeach = new TextBox();
            label2 = new Label();
            txtHostSeach = new TextBox();
            btnSeach = new Button();
            lvVpnList = new ListView();
            tabPageWebwoser = new TabPage();
            btnWebwoserAddAccount = new Button();
            btnWebwoserGoto = new Button();
            lbWebwoserPageShow = new Label();
            txtWebwoserGotoPage = new TextBox();
            btnWebwoserNext = new Button();
            btnWebwoserup = new Button();
            label10 = new Label();
            textBox3 = new TextBox();
            label12 = new Label();
            textBox4 = new TextBox();
            button6 = new Button();
            lsWebwoserAccounts = new ListView();
            tabPageWebbrowserCollect = new TabPage();
            btnWebbrowserCollectStart = new Button();
            btnWebCollectAdd = new Button();
            btnWebCollectGoto = new Button();
            lbWebCollectText = new Label();
            txtWebCollectGoto = new TextBox();
            btnWebCollectNext = new Button();
            btnWebCollectPrev = new Button();
            label18 = new Label();
            txtWebCollectSearch = new TextBox();
            btnWebCollectSearch = new Button();
            lsWebCollect = new ListView();
            label1 = new Label();
            richTextBoxLog = new RichTextBox();
            cmbMemberSelectSendStatus = new ComboBox();
            tabControlMain.SuspendLayout();
            tabPageSetting.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            gbWebGroupManagerSettings.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPageAccount.SuspendLayout();
            tabPageCollectTask.SuspendLayout();
            tabPageCollectMember.SuspendLayout();
            tabPageVpn.SuspendLayout();
            tabPageWebwoser.SuspendLayout();
            tabPageWebbrowserCollect.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageSetting);
            tabControlMain.Controls.Add(tabPageAccount);
            tabControlMain.Controls.Add(tabPageCollectTask);
            tabControlMain.Controls.Add(tabPageCollectMember);
            tabControlMain.Controls.Add(tabPageVpn);
            tabControlMain.Controls.Add(tabPageWebwoser);
            tabControlMain.Controls.Add(tabPageWebbrowserCollect);
            tabControlMain.Location = new Point(12, 25);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1106, 448);
            tabControlMain.TabIndex = 0;
            // 
            // tabPageSetting
            // 
            tabPageSetting.Controls.Add(label31);
            tabPageSetting.Controls.Add(label30);
            tabPageSetting.Controls.Add(label29);
            tabPageSetting.Controls.Add(label25);
            tabPageSetting.Controls.Add(label19);
            tabPageSetting.Controls.Add(label28);
            tabPageSetting.Controls.Add(label27);
            tabPageSetting.Controls.Add(label24);
            tabPageSetting.Controls.Add(label22);
            tabPageSetting.Controls.Add(label21);
            tabPageSetting.Controls.Add(label26);
            tabPageSetting.Controls.Add(llabGetApi);
            tabPageSetting.Controls.Add(label23);
            tabPageSetting.Controls.Add(groupBox5);
            tabPageSetting.Controls.Add(groupBox4);
            tabPageSetting.Controls.Add(groupBox3);
            tabPageSetting.Controls.Add(gbWebGroupManagerSettings);
            tabPageSetting.Controls.Add(groupBox2);
            tabPageSetting.Controls.Add(btnSettingSave);
            tabPageSetting.Controls.Add(groupBox1);
            tabPageSetting.Location = new Point(4, 26);
            tabPageSetting.Name = "tabPageSetting";
            tabPageSetting.Padding = new Padding(3);
            tabPageSetting.Size = new Size(1098, 418);
            tabPageSetting.TabIndex = 0;
            tabPageSetting.Text = "基础设置";
            tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(641, 391);
            label31.Name = "label31";
            label31.Size = new Size(178, 17);
            label31.TabIndex = 29;
            label31.Text = "3.群发中无法使用 检测账号功能";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(641, 374);
            label30.Name = "label30";
            label30.Size = new Size(225, 17);
            label30.TabIndex = 28;
            label30.Text = "2.协议群发读取已采集会员正序,web倒叙";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(641, 357);
            label29.Name = "label29";
            label29.Size = new Size(222, 17);
            label29.TabIndex = 27;
            label29.Text = "1.群发账号使用账号管理选择路劲中账号";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(641, 340);
            label25.Name = "label25";
            label25.Size = new Size(56, 17);
            label25.TabIndex = 26;
            label25.Text = "群发说明";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(641, 323);
            label19.Name = "label19";
            label19.Size = new Size(370, 17);
            label19.TabIndex = 25;
            label19.Text = "2.协议采集默认使用当前程序路劲/session/collect下的.session文件";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(641, 382);
            label28.Name = "label28";
            label28.Size = new Size(0, 17);
            label28.TabIndex = 24;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(750, 221);
            label27.Name = "label27";
            label27.Size = new Size(342, 17);
            label27.TabIndex = 23;
            label27.Text = "注:web账号/设置等数据都在本地mydata.db文件中请妥善保管";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(641, 306);
            label24.Name = "label24";
            label24.Size = new Size(421, 17);
            label24.TabIndex = 21;
            label24.Text = "1.设置内容并勾选即为打开,多条用|分割,如填写昵称过滤 a 昵称中存在a即跳过";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(641, 286);
            label22.Name = "label22";
            label22.Size = new Size(83, 17);
            label22.TabIndex = 20;
            label22.Text = "采集设置说明:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(641, 269);
            label21.Name = "label21";
            label21.Size = new Size(252, 17);
            label21.TabIndex = 19;
            label21.Text = "2.用一套id和hash登录 换另一套需要重新登录";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(641, 230);
            label26.Name = "label26";
            label26.Size = new Size(71, 17);
            label26.TabIndex = 18;
            label26.Text = "协议号说明:";
            // 
            // llabGetApi
            // 
            llabGetApi.AutoSize = true;
            llabGetApi.LinkColor = Color.Black;
            llabGetApi.Location = new Point(641, 249);
            llabGetApi.Name = "llabGetApi";
            llabGetApi.Size = new Size(292, 17);
            llabGetApi.TabIndex = 15;
            llabGetApi.TabStop = true;
            llabGetApi.Text = "1.协议相关功能api_id和apihash为必要参数,双击跳转";
            llabGetApi.LinkClicked += llabGetApi_LinkClicked;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(645, 230);
            label23.Name = "label23";
            label23.Size = new Size(0, 17);
            label23.TabIndex = 14;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(cboxWebCollectInfoFilter);
            groupBox5.Controls.Add(cboxWebCollectNickFilter);
            groupBox5.Controls.Add(txtWebCollectInfoFilter);
            groupBox5.Controls.Add(txtWebCollectNickFilter);
            groupBox5.Location = new Point(290, 221);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(345, 191);
            groupBox5.TabIndex = 13;
            groupBox5.TabStop = false;
            groupBox5.Text = "Web采集设置";
            // 
            // cboxWebCollectInfoFilter
            // 
            cboxWebCollectInfoFilter.AutoSize = true;
            cboxWebCollectInfoFilter.Location = new Point(176, 18);
            cboxWebCollectInfoFilter.Name = "cboxWebCollectInfoFilter";
            cboxWebCollectInfoFilter.Size = new Size(75, 21);
            cboxWebCollectInfoFilter.TabIndex = 11;
            cboxWebCollectInfoFilter.Text = "简介过滤";
            cboxWebCollectInfoFilter.UseVisualStyleBackColor = true;
            // 
            // cboxWebCollectNickFilter
            // 
            cboxWebCollectNickFilter.AutoSize = true;
            cboxWebCollectNickFilter.Location = new Point(12, 18);
            cboxWebCollectNickFilter.Name = "cboxWebCollectNickFilter";
            cboxWebCollectNickFilter.Size = new Size(75, 21);
            cboxWebCollectNickFilter.TabIndex = 10;
            cboxWebCollectNickFilter.Text = "昵称过滤";
            cboxWebCollectNickFilter.UseVisualStyleBackColor = true;
            // 
            // txtWebCollectInfoFilter
            // 
            txtWebCollectInfoFilter.Location = new Point(176, 45);
            txtWebCollectInfoFilter.Multiline = true;
            txtWebCollectInfoFilter.Name = "txtWebCollectInfoFilter";
            txtWebCollectInfoFilter.ScrollBars = ScrollBars.Vertical;
            txtWebCollectInfoFilter.Size = new Size(150, 131);
            txtWebCollectInfoFilter.TabIndex = 7;
            // 
            // txtWebCollectNickFilter
            // 
            txtWebCollectNickFilter.Location = new Point(12, 45);
            txtWebCollectNickFilter.Multiline = true;
            txtWebCollectNickFilter.Name = "txtWebCollectNickFilter";
            txtWebCollectNickFilter.ScrollBars = ScrollBars.Vertical;
            txtWebCollectNickFilter.Size = new Size(150, 131);
            txtWebCollectNickFilter.TabIndex = 6;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cboxSessionCollectFilterBot);
            groupBox4.Controls.Add(cboxSessionCollectFilterManager);
            groupBox4.Controls.Add(cboxSessionSetingDownloadAvatar);
            groupBox4.Controls.Add(cboxSessionCollectInfoFilter);
            groupBox4.Controls.Add(cboxSessionCollectNickFilter);
            groupBox4.Controls.Add(txtSessionCollectInfoFilter);
            groupBox4.Controls.Add(txtSessionCollectNickFilter);
            groupBox4.Location = new Point(290, 24);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(343, 191);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "协议采集设置";
            // 
            // cboxSessionCollectFilterBot
            // 
            cboxSessionCollectFilterBot.AutoSize = true;
            cboxSessionCollectFilterBot.Location = new Point(171, 140);
            cboxSessionCollectFilterBot.Name = "cboxSessionCollectFilterBot";
            cboxSessionCollectFilterBot.Size = new Size(95, 21);
            cboxSessionCollectFilterBot.TabIndex = 16;
            cboxSessionCollectFilterBot.Text = "是否过滤bot";
            cboxSessionCollectFilterBot.UseVisualStyleBackColor = true;
            // 
            // cboxSessionCollectFilterManager
            // 
            cboxSessionCollectFilterManager.AutoSize = true;
            cboxSessionCollectFilterManager.Location = new Point(171, 113);
            cboxSessionCollectFilterManager.Name = "cboxSessionCollectFilterManager";
            cboxSessionCollectFilterManager.Size = new Size(111, 21);
            cboxSessionCollectFilterManager.TabIndex = 15;
            cboxSessionCollectFilterManager.Text = "是否过滤管理员";
            cboxSessionCollectFilterManager.UseVisualStyleBackColor = true;
            // 
            // cboxSessionSetingDownloadAvatar
            // 
            cboxSessionSetingDownloadAvatar.AutoSize = true;
            cboxSessionSetingDownloadAvatar.Location = new Point(170, 86);
            cboxSessionSetingDownloadAvatar.Name = "cboxSessionSetingDownloadAvatar";
            cboxSessionSetingDownloadAvatar.Size = new Size(99, 21);
            cboxSessionSetingDownloadAvatar.TabIndex = 14;
            cboxSessionSetingDownloadAvatar.Text = "是否采集头像";
            cboxSessionSetingDownloadAvatar.UseVisualStyleBackColor = true;
            // 
            // cboxSessionCollectInfoFilter
            // 
            cboxSessionCollectInfoFilter.AutoSize = true;
            cboxSessionCollectInfoFilter.Location = new Point(170, 24);
            cboxSessionCollectInfoFilter.Name = "cboxSessionCollectInfoFilter";
            cboxSessionCollectInfoFilter.Size = new Size(119, 21);
            cboxSessionCollectInfoFilter.TabIndex = 13;
            cboxSessionCollectInfoFilter.Text = "简介过滤(不可用)";
            cboxSessionCollectInfoFilter.UseVisualStyleBackColor = true;
            // 
            // cboxSessionCollectNickFilter
            // 
            cboxSessionCollectNickFilter.AutoSize = true;
            cboxSessionCollectNickFilter.Location = new Point(6, 24);
            cboxSessionCollectNickFilter.Name = "cboxSessionCollectNickFilter";
            cboxSessionCollectNickFilter.Size = new Size(75, 21);
            cboxSessionCollectNickFilter.TabIndex = 12;
            cboxSessionCollectNickFilter.Text = "昵称过滤";
            cboxSessionCollectNickFilter.UseVisualStyleBackColor = true;
            // 
            // txtSessionCollectInfoFilter
            // 
            txtSessionCollectInfoFilter.Location = new Point(171, 49);
            txtSessionCollectInfoFilter.Multiline = true;
            txtSessionCollectInfoFilter.Name = "txtSessionCollectInfoFilter";
            txtSessionCollectInfoFilter.ScrollBars = ScrollBars.Vertical;
            txtSessionCollectInfoFilter.Size = new Size(150, 31);
            txtSessionCollectInfoFilter.TabIndex = 6;
            // 
            // txtSessionCollectNickFilter
            // 
            txtSessionCollectNickFilter.Location = new Point(6, 49);
            txtSessionCollectNickFilter.Multiline = true;
            txtSessionCollectNickFilter.Name = "txtSessionCollectNickFilter";
            txtSessionCollectNickFilter.ScrollBars = ScrollBars.Vertical;
            txtSessionCollectNickFilter.Size = new Size(150, 131);
            txtSessionCollectNickFilter.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtSessionSendMsgContexts);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(txtSessionSendMsgCountMax);
            groupBox3.Controls.Add(txtSessionSendMsgDelayMaxs);
            groupBox3.Controls.Add(txtSessionSendMsgDelayMins);
            groupBox3.Controls.Add(label15);
            groupBox3.Location = new Point(639, 24);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(445, 191);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "协议群发设置";
            // 
            // txtSessionSendMsgContexts
            // 
            txtSessionSendMsgContexts.Location = new Point(17, 75);
            txtSessionSendMsgContexts.Multiline = true;
            txtSessionSendMsgContexts.Name = "txtSessionSendMsgContexts";
            txtSessionSendMsgContexts.Size = new Size(414, 105);
            txtSessionSendMsgContexts.TabIndex = 11;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(17, 55);
            label20.Name = "label20";
            label20.Size = new Size(166, 17);
            label20.TabIndex = 7;
            label20.Text = "群发内容|线分割  注:取随机值";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(325, 29);
            label16.Name = "label16";
            label16.Size = new Size(32, 17);
            label16.TabIndex = 6;
            label16.Text = "阈值";
            // 
            // txtSessionSendMsgCountMax
            // 
            txtSessionSendMsgCountMax.Location = new Point(363, 26);
            txtSessionSendMsgCountMax.Name = "txtSessionSendMsgCountMax";
            txtSessionSendMsgCountMax.Size = new Size(68, 23);
            txtSessionSendMsgCountMax.TabIndex = 5;
            // 
            // txtSessionSendMsgDelayMaxs
            // 
            txtSessionSendMsgDelayMaxs.Location = new Point(238, 26);
            txtSessionSendMsgDelayMaxs.Name = "txtSessionSendMsgDelayMaxs";
            txtSessionSendMsgDelayMaxs.Size = new Size(68, 23);
            txtSessionSendMsgDelayMaxs.TabIndex = 2;
            // 
            // txtSessionSendMsgDelayMins
            // 
            txtSessionSendMsgDelayMins.Location = new Point(164, 26);
            txtSessionSendMsgDelayMins.Name = "txtSessionSendMsgDelayMins";
            txtSessionSendMsgDelayMins.Size = new Size(68, 23);
            txtSessionSendMsgDelayMins.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(17, 28);
            label15.Name = "label15";
            label15.Size = new Size(141, 17);
            label15.TabIndex = 0;
            label15.Text = "群发频率(小-大)取随机值";
            // 
            // gbWebGroupManagerSettings
            // 
            gbWebGroupManagerSettings.Controls.Add(txtWebGroupManagerSendMaxCount);
            gbWebGroupManagerSettings.Controls.Add(label14);
            gbWebGroupManagerSettings.Controls.Add(txtWebManagerSendContext);
            gbWebGroupManagerSettings.Controls.Add(label13);
            gbWebGroupManagerSettings.Controls.Add(txtWebGroupManagerSendMax);
            gbWebGroupManagerSettings.Controls.Add(txtWebGroupManagerSendMin);
            gbWebGroupManagerSettings.Controls.Add(label4);
            gbWebGroupManagerSettings.Location = new Point(20, 221);
            gbWebGroupManagerSettings.Name = "gbWebGroupManagerSettings";
            gbWebGroupManagerSettings.Size = new Size(253, 191);
            gbWebGroupManagerSettings.TabIndex = 7;
            gbWebGroupManagerSettings.TabStop = false;
            gbWebGroupManagerSettings.Text = "Web群发设置";
            // 
            // txtWebGroupManagerSendMaxCount
            // 
            txtWebGroupManagerSendMaxCount.Location = new Point(164, 51);
            txtWebGroupManagerSendMaxCount.Name = "txtWebGroupManagerSendMaxCount";
            txtWebGroupManagerSendMaxCount.Size = new Size(68, 23);
            txtWebGroupManagerSendMaxCount.TabIndex = 10;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(102, 54);
            label14.Name = "label14";
            label14.Size = new Size(56, 17);
            label14.TabIndex = 9;
            label14.Text = "发送阈值";
            // 
            // txtWebManagerSendContext
            // 
            txtWebManagerSendContext.Location = new Point(17, 83);
            txtWebManagerSendContext.Multiline = true;
            txtWebManagerSendContext.Name = "txtWebManagerSendContext";
            txtWebManagerSendContext.Size = new Size(215, 92);
            txtWebManagerSendContext.TabIndex = 6;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(17, 54);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 8;
            label13.Text = "群发内容";
            // 
            // txtWebGroupManagerSendMax
            // 
            txtWebGroupManagerSendMax.Location = new Point(176, 22);
            txtWebGroupManagerSendMax.Name = "txtWebGroupManagerSendMax";
            txtWebGroupManagerSendMax.Size = new Size(56, 23);
            txtWebGroupManagerSendMax.TabIndex = 7;
            // 
            // txtWebGroupManagerSendMin
            // 
            txtWebGroupManagerSendMin.Location = new Point(112, 22);
            txtWebGroupManagerSendMin.Name = "txtWebGroupManagerSendMin";
            txtWebGroupManagerSendMin.Size = new Size(58, 23);
            txtWebGroupManagerSendMin.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 28);
            label4.Name = "label4";
            label4.Size = new Size(93, 17);
            label4.TabIndex = 3;
            label4.Text = "群发频率(小-大)";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cboSettingVpn);
            groupBox2.Controls.Add(label9);
            groupBox2.Location = new Point(20, 128);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(253, 84);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Vpn设置";
            // 
            // cboSettingVpn
            // 
            cboSettingVpn.FormattingEnabled = true;
            cboSettingVpn.Location = new Point(17, 48);
            cboSettingVpn.Name = "cboSettingVpn";
            cboSettingVpn.Size = new Size(215, 25);
            cboSettingVpn.TabIndex = 4;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(17, 28);
            label9.Name = "label9";
            label9.Size = new Size(191, 17);
            label9.TabIndex = 3;
            label9.Text = "主要用于协议/群发/采集/登录使用";
            // 
            // btnSettingSave
            // 
            btnSettingSave.Location = new Point(962, 374);
            btnSettingSave.Name = "btnSettingSave";
            btnSettingSave.Size = new Size(130, 38);
            btnSettingSave.TabIndex = 1;
            btnSettingSave.Text = "保存设置/刷新";
            btnSettingSave.UseVisualStyleBackColor = true;
            btnSettingSave.Click += btnSettingSave_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(txtApiHash);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtApiId);
            groupBox1.Location = new Point(20, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(253, 101);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Telegram基础设置";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 63);
            label8.Name = "label8";
            label8.Size = new Size(58, 17);
            label8.TabIndex = 5;
            label8.Text = "api_hash";
            // 
            // txtApiHash
            // 
            txtApiHash.Location = new Point(88, 60);
            txtApiHash.Name = "txtApiHash";
            txtApiHash.Size = new Size(144, 23);
            txtApiHash.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 28);
            label7.Name = "label7";
            label7.Size = new Size(42, 17);
            label7.TabIndex = 3;
            label7.Text = "api_id";
            // 
            // txtApiId
            // 
            txtApiId.Location = new Point(88, 28);
            txtApiId.Name = "txtApiId";
            txtApiId.Size = new Size(144, 23);
            txtApiId.TabIndex = 2;
            // 
            // tabPageAccount
            // 
            tabPageAccount.Controls.Add(lbAccountSelectPath);
            tabPageAccount.Controls.Add(btnAccountSelecetPath);
            tabPageAccount.Controls.Add(txtAccountSeach);
            tabPageAccount.Controls.Add(btnAccountRefresh);
            tabPageAccount.Controls.Add(btnAddAccount);
            tabPageAccount.Controls.Add(btnAccountGoto);
            tabPageAccount.Controls.Add(lbAccount);
            tabPageAccount.Controls.Add(txtAccountGoto);
            tabPageAccount.Controls.Add(btcAccountNext);
            tabPageAccount.Controls.Add(btnAccountUp);
            tabPageAccount.Controls.Add(label6);
            tabPageAccount.Controls.Add(btnAccountSeach);
            tabPageAccount.Controls.Add(lvAccounts);
            tabPageAccount.Location = new Point(4, 26);
            tabPageAccount.Name = "tabPageAccount";
            tabPageAccount.Size = new Size(1098, 418);
            tabPageAccount.TabIndex = 2;
            tabPageAccount.Text = "账号管理";
            tabPageAccount.UseVisualStyleBackColor = true;
            // 
            // lbAccountSelectPath
            // 
            lbAccountSelectPath.AutoSize = true;
            lbAccountSelectPath.Location = new Point(408, 20);
            lbAccountSelectPath.Name = "lbAccountSelectPath";
            lbAccountSelectPath.Size = new Size(0, 17);
            lbAccountSelectPath.TabIndex = 27;
            // 
            // btnAccountSelecetPath
            // 
            btnAccountSelecetPath.Location = new Point(302, 14);
            btnAccountSelecetPath.Name = "btnAccountSelecetPath";
            btnAccountSelecetPath.Size = new Size(100, 27);
            btnAccountSelecetPath.TabIndex = 26;
            btnAccountSelecetPath.Text = "选择账号路劲";
            btnAccountSelecetPath.UseVisualStyleBackColor = true;
            btnAccountSelecetPath.Click += btnAccountSelecetPath_Click;
            // 
            // txtAccountSeach
            // 
            txtAccountSeach.Location = new Point(52, 14);
            txtAccountSeach.Name = "txtAccountSeach";
            txtAccountSeach.Size = new Size(138, 23);
            txtAccountSeach.TabIndex = 25;
            // 
            // btnAccountRefresh
            // 
            btnAccountRefresh.Location = new Point(824, 14);
            btnAccountRefresh.Name = "btnAccountRefresh";
            btnAccountRefresh.Size = new Size(172, 27);
            btnAccountRefresh.TabIndex = 24;
            btnAccountRefresh.Text = "检测账号并获取所有群聊";
            btnAccountRefresh.UseVisualStyleBackColor = true;
            btnAccountRefresh.Click += btnAccountRefresh_Click;
            // 
            // btnAddAccount
            // 
            btnAddAccount.Location = new Point(1002, 14);
            btnAddAccount.Name = "btnAddAccount";
            btnAddAccount.Size = new Size(84, 27);
            btnAddAccount.TabIndex = 23;
            btnAddAccount.Text = "新 增 账 号";
            btnAddAccount.UseVisualStyleBackColor = true;
            btnAddAccount.Click += btnAddAccount_Click;
            // 
            // btnAccountGoto
            // 
            btnAccountGoto.Location = new Point(1002, 378);
            btnAccountGoto.Name = "btnAccountGoto";
            btnAccountGoto.Size = new Size(84, 27);
            btnAccountGoto.TabIndex = 22;
            btnAccountGoto.Text = "跳 转";
            btnAccountGoto.UseVisualStyleBackColor = true;
            btnAccountGoto.Click += btnAccountGoto_Click;
            // 
            // lbAccount
            // 
            lbAccount.AutoSize = true;
            lbAccount.Location = new Point(800, 383);
            lbAccount.Name = "lbAccount";
            lbAccount.Size = new Size(34, 17);
            lbAccount.TabIndex = 21;
            lbAccount.Text = "1/10";
            // 
            // txtAccountGoto
            // 
            txtAccountGoto.Location = new Point(966, 380);
            txtAccountGoto.Name = "txtAccountGoto";
            txtAccountGoto.Size = new Size(30, 23);
            txtAccountGoto.TabIndex = 20;
            // 
            // btcAccountNext
            // 
            btcAccountNext.Location = new Point(862, 378);
            btcAccountNext.Name = "btcAccountNext";
            btcAccountNext.Size = new Size(84, 27);
            btcAccountNext.TabIndex = 19;
            btcAccountNext.Text = "下 一 页";
            btcAccountNext.UseVisualStyleBackColor = true;
            btcAccountNext.Click += btcAccountNext_Click;
            // 
            // btnAccountUp
            // 
            btnAccountUp.Location = new Point(686, 378);
            btnAccountUp.Name = "btnAccountUp";
            btnAccountUp.Size = new Size(84, 27);
            btnAccountUp.TabIndex = 18;
            btnAccountUp.Text = "上 一 页";
            btnAccountUp.UseVisualStyleBackColor = true;
            btnAccountUp.Click += btnAccountUp_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 20);
            label6.Name = "label6";
            label6.Size = new Size(32, 17);
            label6.TabIndex = 15;
            label6.Text = "账号";
            // 
            // btnAccountSeach
            // 
            btnAccountSeach.Location = new Point(212, 14);
            btnAccountSeach.Name = "btnAccountSeach";
            btnAccountSeach.Size = new Size(84, 27);
            btnAccountSeach.TabIndex = 13;
            btnAccountSeach.Text = "搜 索";
            btnAccountSeach.UseVisualStyleBackColor = true;
            btnAccountSeach.Click += btnAccountSeach_Click;
            // 
            // lvAccounts
            // 
            lvAccounts.Location = new Point(12, 49);
            lvAccounts.Name = "lvAccounts";
            lvAccounts.Size = new Size(1074, 316);
            lvAccounts.TabIndex = 12;
            lvAccounts.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageCollectTask
            // 
            tabPageCollectTask.Controls.Add(label5);
            tabPageCollectTask.Controls.Add(txtCollectAdd);
            tabPageCollectTask.Controls.Add(btnCollectAdd);
            tabPageCollectTask.Controls.Add(btnSessionCollectStart);
            tabPageCollectTask.Controls.Add(button2);
            tabPageCollectTask.Controls.Add(lbCollectTaskTxt);
            tabPageCollectTask.Controls.Add(textBox1);
            tabPageCollectTask.Controls.Add(btnCollectTaskNext);
            tabPageCollectTask.Controls.Add(btnCollectTaskPrev);
            tabPageCollectTask.Controls.Add(label11);
            tabPageCollectTask.Controls.Add(txtCollectTaskSeach);
            tabPageCollectTask.Controls.Add(btnCollectTaskSeach);
            tabPageCollectTask.Controls.Add(lvCollect);
            tabPageCollectTask.Location = new Point(4, 26);
            tabPageCollectTask.Name = "tabPageCollectTask";
            tabPageCollectTask.Padding = new Padding(3);
            tabPageCollectTask.Size = new Size(1098, 418);
            tabPageCollectTask.TabIndex = 1;
            tabPageCollectTask.Text = "采集任务管理";
            tabPageCollectTask.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(698, 19);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 25;
            label5.Text = "群组用户名";
            // 
            // txtCollectAdd
            // 
            txtCollectAdd.Location = new Point(772, 14);
            txtCollectAdd.Name = "txtCollectAdd";
            txtCollectAdd.Size = new Size(138, 23);
            txtCollectAdd.TabIndex = 24;
            // 
            // btnCollectAdd
            // 
            btnCollectAdd.Location = new Point(926, 14);
            btnCollectAdd.Name = "btnCollectAdd";
            btnCollectAdd.Size = new Size(160, 27);
            btnCollectAdd.TabIndex = 23;
            btnCollectAdd.Text = "新 增 采 集 群 组";
            btnCollectAdd.UseVisualStyleBackColor = true;
            btnCollectAdd.Click += btnCollectAdd_Click;
            // 
            // btnSessionCollectStart
            // 
            btnSessionCollectStart.Location = new Point(299, 15);
            btnSessionCollectStart.Name = "btnSessionCollectStart";
            btnSessionCollectStart.Size = new Size(142, 25);
            btnSessionCollectStart.TabIndex = 37;
            btnSessionCollectStart.Text = "开 始 协 议 采 集";
            btnSessionCollectStart.UseVisualStyleBackColor = true;
            btnSessionCollectStart.Click += btnSessionCollectStart_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1002, 378);
            button2.Name = "button2";
            button2.Size = new Size(84, 27);
            button2.TabIndex = 22;
            button2.Text = "跳 转";
            button2.UseVisualStyleBackColor = true;
            // 
            // lbCollectTaskTxt
            // 
            lbCollectTaskTxt.AutoSize = true;
            lbCollectTaskTxt.Location = new Point(800, 383);
            lbCollectTaskTxt.Name = "lbCollectTaskTxt";
            lbCollectTaskTxt.Size = new Size(34, 17);
            lbCollectTaskTxt.TabIndex = 21;
            lbCollectTaskTxt.Text = "1/10";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(966, 380);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(30, 23);
            textBox1.TabIndex = 20;
            // 
            // btnCollectTaskNext
            // 
            btnCollectTaskNext.Location = new Point(862, 378);
            btnCollectTaskNext.Name = "btnCollectTaskNext";
            btnCollectTaskNext.Size = new Size(84, 27);
            btnCollectTaskNext.TabIndex = 19;
            btnCollectTaskNext.Text = "下 一 页";
            btnCollectTaskNext.UseVisualStyleBackColor = true;
            btnCollectTaskNext.Click += btnCollectTaskNext_Click;
            // 
            // btnCollectTaskPrev
            // 
            btnCollectTaskPrev.Location = new Point(686, 378);
            btnCollectTaskPrev.Name = "btnCollectTaskPrev";
            btnCollectTaskPrev.Size = new Size(84, 27);
            btnCollectTaskPrev.TabIndex = 18;
            btnCollectTaskPrev.Text = "上 一 页";
            btnCollectTaskPrev.UseVisualStyleBackColor = true;
            btnCollectTaskPrev.Click += btnCollectTaskPrev_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(14, 20);
            label11.Name = "label11";
            label11.Size = new Size(44, 17);
            label11.TabIndex = 15;
            label11.Text = "用户名";
            // 
            // txtCollectTaskSeach
            // 
            txtCollectTaskSeach.Location = new Point(64, 16);
            txtCollectTaskSeach.Name = "txtCollectTaskSeach";
            txtCollectTaskSeach.Size = new Size(138, 23);
            txtCollectTaskSeach.TabIndex = 14;
            // 
            // btnCollectTaskSeach
            // 
            btnCollectTaskSeach.Location = new Point(209, 14);
            btnCollectTaskSeach.Name = "btnCollectTaskSeach";
            btnCollectTaskSeach.Size = new Size(84, 27);
            btnCollectTaskSeach.TabIndex = 13;
            btnCollectTaskSeach.Text = "搜 索";
            btnCollectTaskSeach.UseVisualStyleBackColor = true;
            btnCollectTaskSeach.Click += btnCollectTaskSeach_Click;
            // 
            // lvCollect
            // 
            lvCollect.Location = new Point(12, 49);
            lvCollect.Name = "lvCollect";
            lvCollect.Size = new Size(1074, 316);
            lvCollect.TabIndex = 12;
            lvCollect.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageCollectMember
            // 
            tabPageCollectMember.Controls.Add(cmbMemberSelectSendStatus);
            tabPageCollectMember.Controls.Add(cmbMemberOrderBy);
            tabPageCollectMember.Controls.Add(cmbMemberP);
            tabPageCollectMember.Controls.Add(btnWebbrowserSendMsg);
            tabPageCollectMember.Controls.Add(btnSessionSendMsgStart);
            tabPageCollectMember.Controls.Add(lbCollectUserNameShowCount);
            tabPageCollectMember.Controls.Add(btnCollectUserNameGoto);
            tabPageCollectMember.Controls.Add(lbCollectUsername);
            tabPageCollectMember.Controls.Add(txtCollectUserNameGoto);
            tabPageCollectMember.Controls.Add(btnCollectUserNameDown);
            tabPageCollectMember.Controls.Add(btnCollectUserNameUp);
            tabPageCollectMember.Controls.Add(label17);
            tabPageCollectMember.Controls.Add(txtCollectUserNameSeach);
            tabPageCollectMember.Controls.Add(btnCollectUsernameSeach);
            tabPageCollectMember.Controls.Add(lsCollectUserName);
            tabPageCollectMember.Location = new Point(4, 26);
            tabPageCollectMember.Name = "tabPageCollectMember";
            tabPageCollectMember.Size = new Size(1098, 418);
            tabPageCollectMember.TabIndex = 5;
            tabPageCollectMember.Text = "已采集会员";
            tabPageCollectMember.UseVisualStyleBackColor = true;
            // 
            // cmbMemberOrderBy
            // 
            cmbMemberOrderBy.FormattingEnabled = true;
            cmbMemberOrderBy.Location = new Point(335, 14);
            cmbMemberOrderBy.Name = "cmbMemberOrderBy";
            cmbMemberOrderBy.Size = new Size(121, 25);
            cmbMemberOrderBy.TabIndex = 38;
            // 
            // cmbMemberP
            // 
            cmbMemberP.FormattingEnabled = true;
            cmbMemberP.Location = new Point(208, 14);
            cmbMemberP.Name = "cmbMemberP";
            cmbMemberP.Size = new Size(121, 25);
            cmbMemberP.TabIndex = 37;
            // 
            // btnWebbrowserSendMsg
            // 
            btnWebbrowserSendMsg.Location = new Point(868, 10);
            btnWebbrowserSendMsg.Name = "btnWebbrowserSendMsg";
            btnWebbrowserSendMsg.Size = new Size(161, 25);
            btnWebbrowserSendMsg.TabIndex = 24;
            btnWebbrowserSendMsg.Text = "开 始 web 群 发";
            btnWebbrowserSendMsg.UseVisualStyleBackColor = true;
            btnWebbrowserSendMsg.Click += btnWebbrowserSendMsg_Click;
            // 
            // btnSessionSendMsgStart
            // 
            btnSessionSendMsgStart.Location = new Point(724, 10);
            btnSessionSendMsgStart.Name = "btnSessionSendMsgStart";
            btnSessionSendMsgStart.Size = new Size(138, 25);
            btnSessionSendMsgStart.TabIndex = 36;
            btnSessionSendMsgStart.Text = "开 始 协 议 群 发";
            btnSessionSendMsgStart.UseVisualStyleBackColor = true;
            btnSessionSendMsgStart.Click += btnSessionSendMsgStart_Click;
            // 
            // lbCollectUserNameShowCount
            // 
            lbCollectUserNameShowCount.AutoSize = true;
            lbCollectUserNameShowCount.Location = new Point(14, 386);
            lbCollectUserNameShowCount.Name = "lbCollectUserNameShowCount";
            lbCollectUserNameShowCount.Size = new Size(0, 17);
            lbCollectUserNameShowCount.TabIndex = 35;
            // 
            // btnCollectUserNameGoto
            // 
            btnCollectUserNameGoto.Location = new Point(1002, 378);
            btnCollectUserNameGoto.Name = "btnCollectUserNameGoto";
            btnCollectUserNameGoto.Size = new Size(84, 27);
            btnCollectUserNameGoto.TabIndex = 34;
            btnCollectUserNameGoto.Text = "跳 转";
            btnCollectUserNameGoto.UseVisualStyleBackColor = true;
            btnCollectUserNameGoto.Click += btnCollectUserNameGoto_Click;
            // 
            // lbCollectUsername
            // 
            lbCollectUsername.AutoSize = true;
            lbCollectUsername.Location = new Point(800, 383);
            lbCollectUsername.Name = "lbCollectUsername";
            lbCollectUsername.Size = new Size(34, 17);
            lbCollectUsername.TabIndex = 33;
            lbCollectUsername.Text = "1/10";
            // 
            // txtCollectUserNameGoto
            // 
            txtCollectUserNameGoto.Location = new Point(966, 380);
            txtCollectUserNameGoto.Name = "txtCollectUserNameGoto";
            txtCollectUserNameGoto.Size = new Size(30, 23);
            txtCollectUserNameGoto.TabIndex = 32;
            // 
            // btnCollectUserNameDown
            // 
            btnCollectUserNameDown.Location = new Point(862, 378);
            btnCollectUserNameDown.Name = "btnCollectUserNameDown";
            btnCollectUserNameDown.Size = new Size(84, 27);
            btnCollectUserNameDown.TabIndex = 31;
            btnCollectUserNameDown.Text = "下 一 页";
            btnCollectUserNameDown.UseVisualStyleBackColor = true;
            btnCollectUserNameDown.Click += btnCollectUserNameDown_Click;
            // 
            // btnCollectUserNameUp
            // 
            btnCollectUserNameUp.Location = new Point(686, 378);
            btnCollectUserNameUp.Name = "btnCollectUserNameUp";
            btnCollectUserNameUp.Size = new Size(84, 27);
            btnCollectUserNameUp.TabIndex = 30;
            btnCollectUserNameUp.Text = "上 一 页";
            btnCollectUserNameUp.UseVisualStyleBackColor = true;
            btnCollectUserNameUp.Click += btnCollectUserNameUp_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(14, 20);
            label17.Name = "label17";
            label17.Size = new Size(44, 17);
            label17.TabIndex = 29;
            label17.Text = "用户名";
            // 
            // txtCollectUserNameSeach
            // 
            txtCollectUserNameSeach.Location = new Point(64, 16);
            txtCollectUserNameSeach.Name = "txtCollectUserNameSeach";
            txtCollectUserNameSeach.Size = new Size(138, 23);
            txtCollectUserNameSeach.TabIndex = 28;
            // 
            // btnCollectUsernameSeach
            // 
            btnCollectUsernameSeach.Location = new Point(634, 10);
            btnCollectUsernameSeach.Name = "btnCollectUsernameSeach";
            btnCollectUsernameSeach.Size = new Size(84, 27);
            btnCollectUsernameSeach.TabIndex = 27;
            btnCollectUsernameSeach.Text = "搜 索";
            btnCollectUsernameSeach.UseVisualStyleBackColor = true;
            btnCollectUsernameSeach.Click += btnCollectUsernameSeach_Click;
            // 
            // lsCollectUserName
            // 
            lsCollectUserName.Location = new Point(12, 49);
            lsCollectUserName.Name = "lsCollectUserName";
            lsCollectUserName.Size = new Size(1074, 316);
            lsCollectUserName.TabIndex = 26;
            lsCollectUserName.UseCompatibleStateImageBehavior = false;
            lsCollectUserName.ItemActivate += lsCollectUserName_ItemActivate;
            // 
            // tabPageVpn
            // 
            tabPageVpn.Controls.Add(btnVpnAdd);
            tabPageVpn.Controls.Add(btnPageGoto);
            tabPageVpn.Controls.Add(lblVpnListPage);
            tabPageVpn.Controls.Add(txtPageGoto);
            tabPageVpn.Controls.Add(btnNext);
            tabPageVpn.Controls.Add(btnPrev);
            tabPageVpn.Controls.Add(label3);
            tabPageVpn.Controls.Add(txtRemarkSeach);
            tabPageVpn.Controls.Add(label2);
            tabPageVpn.Controls.Add(txtHostSeach);
            tabPageVpn.Controls.Add(btnSeach);
            tabPageVpn.Controls.Add(lvVpnList);
            tabPageVpn.Location = new Point(4, 26);
            tabPageVpn.Name = "tabPageVpn";
            tabPageVpn.Size = new Size(1098, 418);
            tabPageVpn.TabIndex = 4;
            tabPageVpn.Text = "代理设置";
            tabPageVpn.UseVisualStyleBackColor = true;
            // 
            // btnVpnAdd
            // 
            btnVpnAdd.Location = new Point(1001, 13);
            btnVpnAdd.Name = "btnVpnAdd";
            btnVpnAdd.Size = new Size(84, 27);
            btnVpnAdd.TabIndex = 11;
            btnVpnAdd.Text = "新 增 代 理";
            btnVpnAdd.UseVisualStyleBackColor = true;
            btnVpnAdd.Click += btnVpnAdd_Click;
            // 
            // btnPageGoto
            // 
            btnPageGoto.Location = new Point(1001, 377);
            btnPageGoto.Name = "btnPageGoto";
            btnPageGoto.Size = new Size(84, 27);
            btnPageGoto.TabIndex = 10;
            btnPageGoto.Text = "跳 转";
            btnPageGoto.UseVisualStyleBackColor = true;
            btnPageGoto.Click += btnPageGoto_Click;
            // 
            // lblVpnListPage
            // 
            lblVpnListPage.AutoSize = true;
            lblVpnListPage.Location = new Point(799, 382);
            lblVpnListPage.Name = "lblVpnListPage";
            lblVpnListPage.Size = new Size(34, 17);
            lblVpnListPage.TabIndex = 9;
            lblVpnListPage.Text = "1/10";
            // 
            // txtPageGoto
            // 
            txtPageGoto.Location = new Point(965, 379);
            txtPageGoto.Name = "txtPageGoto";
            txtPageGoto.Size = new Size(30, 23);
            txtPageGoto.TabIndex = 8;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(861, 377);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(84, 27);
            btnNext.TabIndex = 7;
            btnNext.Text = "下 一 页";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click_1;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(685, 377);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(84, 27);
            btnPrev.TabIndex = 6;
            btnPrev.Text = "上 一 页";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(210, 19);
            label3.Name = "label3";
            label3.Size = new Size(32, 17);
            label3.TabIndex = 5;
            label3.Text = "备注";
            // 
            // txtRemarkSeach
            // 
            txtRemarkSeach.Location = new Point(248, 17);
            txtRemarkSeach.Name = "txtRemarkSeach";
            txtRemarkSeach.Size = new Size(138, 23);
            txtRemarkSeach.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 19);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 3;
            label2.Text = "主机";
            // 
            // txtHostSeach
            // 
            txtHostSeach.Location = new Point(51, 17);
            txtHostSeach.Name = "txtHostSeach";
            txtHostSeach.Size = new Size(138, 23);
            txtHostSeach.TabIndex = 2;
            // 
            // btnSeach
            // 
            btnSeach.Location = new Point(401, 13);
            btnSeach.Name = "btnSeach";
            btnSeach.Size = new Size(84, 27);
            btnSeach.TabIndex = 1;
            btnSeach.Text = "搜 索";
            btnSeach.UseVisualStyleBackColor = true;
            btnSeach.Click += btnSeach_Click;
            // 
            // lvVpnList
            // 
            lvVpnList.Location = new Point(11, 48);
            lvVpnList.Name = "lvVpnList";
            lvVpnList.Size = new Size(1074, 316);
            lvVpnList.TabIndex = 0;
            lvVpnList.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageWebwoser
            // 
            tabPageWebwoser.Controls.Add(btnWebwoserAddAccount);
            tabPageWebwoser.Controls.Add(btnWebwoserGoto);
            tabPageWebwoser.Controls.Add(lbWebwoserPageShow);
            tabPageWebwoser.Controls.Add(txtWebwoserGotoPage);
            tabPageWebwoser.Controls.Add(btnWebwoserNext);
            tabPageWebwoser.Controls.Add(btnWebwoserup);
            tabPageWebwoser.Controls.Add(label10);
            tabPageWebwoser.Controls.Add(textBox3);
            tabPageWebwoser.Controls.Add(label12);
            tabPageWebwoser.Controls.Add(textBox4);
            tabPageWebwoser.Controls.Add(button6);
            tabPageWebwoser.Controls.Add(lsWebwoserAccounts);
            tabPageWebwoser.Location = new Point(4, 26);
            tabPageWebwoser.Name = "tabPageWebwoser";
            tabPageWebwoser.Size = new Size(1098, 418);
            tabPageWebwoser.TabIndex = 6;
            tabPageWebwoser.Text = "浏览器控制";
            tabPageWebwoser.UseVisualStyleBackColor = true;
            // 
            // btnWebwoserAddAccount
            // 
            btnWebwoserAddAccount.Location = new Point(1002, 14);
            btnWebwoserAddAccount.Name = "btnWebwoserAddAccount";
            btnWebwoserAddAccount.Size = new Size(84, 27);
            btnWebwoserAddAccount.TabIndex = 23;
            btnWebwoserAddAccount.Text = "新 增 账 号";
            btnWebwoserAddAccount.UseVisualStyleBackColor = true;
            btnWebwoserAddAccount.Click += btnWebwoserAddAccount_Click;
            // 
            // btnWebwoserGoto
            // 
            btnWebwoserGoto.Location = new Point(1002, 378);
            btnWebwoserGoto.Name = "btnWebwoserGoto";
            btnWebwoserGoto.Size = new Size(84, 27);
            btnWebwoserGoto.TabIndex = 22;
            btnWebwoserGoto.Text = "跳 转";
            btnWebwoserGoto.UseVisualStyleBackColor = true;
            btnWebwoserGoto.Click += btnWebwoserGoto_Click;
            // 
            // lbWebwoserPageShow
            // 
            lbWebwoserPageShow.AutoSize = true;
            lbWebwoserPageShow.Location = new Point(800, 383);
            lbWebwoserPageShow.Name = "lbWebwoserPageShow";
            lbWebwoserPageShow.Size = new Size(34, 17);
            lbWebwoserPageShow.TabIndex = 21;
            lbWebwoserPageShow.Text = "1/10";
            // 
            // txtWebwoserGotoPage
            // 
            txtWebwoserGotoPage.Location = new Point(966, 380);
            txtWebwoserGotoPage.Name = "txtWebwoserGotoPage";
            txtWebwoserGotoPage.Size = new Size(30, 23);
            txtWebwoserGotoPage.TabIndex = 20;
            // 
            // btnWebwoserNext
            // 
            btnWebwoserNext.Location = new Point(862, 378);
            btnWebwoserNext.Name = "btnWebwoserNext";
            btnWebwoserNext.Size = new Size(84, 27);
            btnWebwoserNext.TabIndex = 19;
            btnWebwoserNext.Text = "下 一 页";
            btnWebwoserNext.UseVisualStyleBackColor = true;
            btnWebwoserNext.Click += btnWebwoserNext_Click;
            // 
            // btnWebwoserup
            // 
            btnWebwoserup.Location = new Point(686, 378);
            btnWebwoserup.Name = "btnWebwoserup";
            btnWebwoserup.Size = new Size(84, 27);
            btnWebwoserup.TabIndex = 18;
            btnWebwoserup.Text = "上 一 页";
            btnWebwoserup.UseVisualStyleBackColor = true;
            btnWebwoserup.Click += btnWebwoserup_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(211, 20);
            label10.Name = "label10";
            label10.Size = new Size(32, 17);
            label10.TabIndex = 17;
            label10.Text = "备注";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(249, 18);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(138, 23);
            textBox3.TabIndex = 16;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(14, 20);
            label12.Name = "label12";
            label12.Size = new Size(44, 17);
            label12.TabIndex = 15;
            label12.Text = "手机号";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(64, 17);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(138, 23);
            textBox4.TabIndex = 14;
            // 
            // button6
            // 
            button6.Location = new Point(402, 14);
            button6.Name = "button6";
            button6.Size = new Size(84, 27);
            button6.TabIndex = 13;
            button6.Text = "搜 索";
            button6.UseVisualStyleBackColor = true;
            // 
            // lsWebwoserAccounts
            // 
            lsWebwoserAccounts.Location = new Point(12, 49);
            lsWebwoserAccounts.Name = "lsWebwoserAccounts";
            lsWebwoserAccounts.Size = new Size(1074, 316);
            lsWebwoserAccounts.TabIndex = 12;
            lsWebwoserAccounts.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageWebbrowserCollect
            // 
            tabPageWebbrowserCollect.Controls.Add(btnWebbrowserCollectStart);
            tabPageWebbrowserCollect.Controls.Add(btnWebCollectAdd);
            tabPageWebbrowserCollect.Controls.Add(btnWebCollectGoto);
            tabPageWebbrowserCollect.Controls.Add(lbWebCollectText);
            tabPageWebbrowserCollect.Controls.Add(txtWebCollectGoto);
            tabPageWebbrowserCollect.Controls.Add(btnWebCollectNext);
            tabPageWebbrowserCollect.Controls.Add(btnWebCollectPrev);
            tabPageWebbrowserCollect.Controls.Add(label18);
            tabPageWebbrowserCollect.Controls.Add(txtWebCollectSearch);
            tabPageWebbrowserCollect.Controls.Add(btnWebCollectSearch);
            tabPageWebbrowserCollect.Controls.Add(lsWebCollect);
            tabPageWebbrowserCollect.Location = new Point(4, 26);
            tabPageWebbrowserCollect.Name = "tabPageWebbrowserCollect";
            tabPageWebbrowserCollect.Size = new Size(1098, 418);
            tabPageWebbrowserCollect.TabIndex = 7;
            tabPageWebbrowserCollect.Text = "浏览器采集";
            tabPageWebbrowserCollect.UseVisualStyleBackColor = true;
            // 
            // btnWebbrowserCollectStart
            // 
            btnWebbrowserCollectStart.Location = new Point(319, 14);
            btnWebbrowserCollectStart.Name = "btnWebbrowserCollectStart";
            btnWebbrowserCollectStart.Size = new Size(84, 27);
            btnWebbrowserCollectStart.TabIndex = 36;
            btnWebbrowserCollectStart.Text = "开 始 采 集";
            btnWebbrowserCollectStart.UseVisualStyleBackColor = true;
            btnWebbrowserCollectStart.Click += btnWebbrowserCollectStart_Click;
            // 
            // btnWebCollectAdd
            // 
            btnWebCollectAdd.Location = new Point(926, 14);
            btnWebCollectAdd.Name = "btnWebCollectAdd";
            btnWebCollectAdd.Size = new Size(160, 27);
            btnWebCollectAdd.TabIndex = 35;
            btnWebCollectAdd.Text = "新 增 采 集 群 组";
            btnWebCollectAdd.UseVisualStyleBackColor = true;
            btnWebCollectAdd.Click += btnWebCollectAdd_Click;
            // 
            // btnWebCollectGoto
            // 
            btnWebCollectGoto.Location = new Point(1002, 378);
            btnWebCollectGoto.Name = "btnWebCollectGoto";
            btnWebCollectGoto.Size = new Size(84, 27);
            btnWebCollectGoto.TabIndex = 34;
            btnWebCollectGoto.Text = "跳 转";
            btnWebCollectGoto.UseVisualStyleBackColor = true;
            btnWebCollectGoto.Click += btnWebCollectGoto_Click;
            // 
            // lbWebCollectText
            // 
            lbWebCollectText.AutoSize = true;
            lbWebCollectText.Location = new Point(800, 383);
            lbWebCollectText.Name = "lbWebCollectText";
            lbWebCollectText.Size = new Size(34, 17);
            lbWebCollectText.TabIndex = 33;
            lbWebCollectText.Text = "1/10";
            // 
            // txtWebCollectGoto
            // 
            txtWebCollectGoto.Location = new Point(966, 380);
            txtWebCollectGoto.Name = "txtWebCollectGoto";
            txtWebCollectGoto.Size = new Size(30, 23);
            txtWebCollectGoto.TabIndex = 32;
            // 
            // btnWebCollectNext
            // 
            btnWebCollectNext.Location = new Point(862, 378);
            btnWebCollectNext.Name = "btnWebCollectNext";
            btnWebCollectNext.Size = new Size(84, 27);
            btnWebCollectNext.TabIndex = 31;
            btnWebCollectNext.Text = "下 一 页";
            btnWebCollectNext.UseVisualStyleBackColor = true;
            btnWebCollectNext.Click += btnWebCollectNext_Click;
            // 
            // btnWebCollectPrev
            // 
            btnWebCollectPrev.Location = new Point(686, 378);
            btnWebCollectPrev.Name = "btnWebCollectPrev";
            btnWebCollectPrev.Size = new Size(84, 27);
            btnWebCollectPrev.TabIndex = 30;
            btnWebCollectPrev.Text = "上 一 页";
            btnWebCollectPrev.UseVisualStyleBackColor = true;
            btnWebCollectPrev.Click += btnWebCollectPrev_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(14, 20);
            label18.Name = "label18";
            label18.Size = new Size(44, 17);
            label18.TabIndex = 29;
            label18.Text = "用户名";
            // 
            // txtWebCollectSearch
            // 
            txtWebCollectSearch.Location = new Point(64, 16);
            txtWebCollectSearch.Name = "txtWebCollectSearch";
            txtWebCollectSearch.Size = new Size(138, 23);
            txtWebCollectSearch.TabIndex = 28;
            // 
            // btnWebCollectSearch
            // 
            btnWebCollectSearch.Location = new Point(209, 14);
            btnWebCollectSearch.Name = "btnWebCollectSearch";
            btnWebCollectSearch.Size = new Size(84, 27);
            btnWebCollectSearch.TabIndex = 27;
            btnWebCollectSearch.Text = "搜 索";
            btnWebCollectSearch.UseVisualStyleBackColor = true;
            btnWebCollectSearch.Click += btnWebCollectSearch_Click;
            // 
            // lsWebCollect
            // 
            lsWebCollect.Location = new Point(12, 49);
            lsWebCollect.Name = "lsWebCollect";
            lsWebCollect.Size = new Size(1074, 316);
            lsWebCollect.TabIndex = 26;
            lsWebCollect.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 482);
            label1.Name = "label1";
            label1.Size = new Size(32, 17);
            label1.TabIndex = 2;
            label1.Text = "日志";
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Location = new Point(12, 502);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.Size = new Size(1106, 167);
            richTextBoxLog.TabIndex = 3;
            richTextBoxLog.Text = "";
            // 
            // cmbMemberSelectSendStatus
            // 
            cmbMemberSelectSendStatus.FormattingEnabled = true;
            cmbMemberSelectSendStatus.Location = new Point(462, 14);
            cmbMemberSelectSendStatus.Name = "cmbMemberSelectSendStatus";
            cmbMemberSelectSendStatus.Size = new Size(121, 25);
            cmbMemberSelectSendStatus.TabIndex = 39;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 681);
            Controls.Add(richTextBoxLog);
            Controls.Add(label1);
            Controls.Add(tabControlMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "TG群控";
            Load += MainForm_Load;
            tabControlMain.ResumeLayout(false);
            tabPageSetting.ResumeLayout(false);
            tabPageSetting.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            gbWebGroupManagerSettings.ResumeLayout(false);
            gbWebGroupManagerSettings.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPageAccount.ResumeLayout(false);
            tabPageAccount.PerformLayout();
            tabPageCollectTask.ResumeLayout(false);
            tabPageCollectTask.PerformLayout();
            tabPageCollectMember.ResumeLayout(false);
            tabPageCollectMember.PerformLayout();
            tabPageVpn.ResumeLayout(false);
            tabPageVpn.PerformLayout();
            tabPageWebwoser.ResumeLayout(false);
            tabPageWebwoser.PerformLayout();
            tabPageWebbrowserCollect.ResumeLayout(false);
            tabPageWebbrowserCollect.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void LsCollectUserName_ItemActivate(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private TabControl tabControlMain;
        private TabPage tabPageSetting;
        private TabPage tabPageCollectTask;
        private Label label1;
        private RichTextBox richTextBoxLog;
        private TabPage tabPageVpn;
        private Label label2;
        private TextBox txtHostSeach;
        private Button btnSeach;
        private ListView lvVpnList;
        private Label lblVpnListPage;
        private TextBox txtPageGoto;
        private Button btnNext;
        private Button btnPrev;
        private Label label3;
        private TextBox txtRemarkSeach;
        private Button btnVpnAdd;
        private Button btnPageGoto;
        private Label label5;
        private TextBox textBox4;
        private TextBox txtWebCollectGoto;
        private GroupBox groupBox1;
        private Label label7;
        private TextBox txtApiId;
        private Label label8;
        private TextBox txtApiHash;
        private Button btnSettingSave;
        private GroupBox groupBox2;
        private Label label9;
        private ComboBox cboSettingVpn;
        private Button btnCollectAdd;
        private Button button2;
        private Label lbCollectTaskTxt;
        private TextBox textBox1;
        private Button btnCollectTaskNext;
        private Button btnCollectTaskPrev;
        private Label label11;
        private TextBox txtCollectTaskSeach;
        private Button btnCollectTaskSeach;
        private ListView lvCollect;
        private TabPage tabPageCollectMember;
        private TextBox txtCollectAdd;
        private TabPage tabPageWebwoser;
        private Button btnWebwoserAddAccount;
        private Button btnWebwoserGoto;
        private Label lbWebwoserPageShow;
        private TextBox txtWebwoserGotoPage;
        private Button btnWebwoserNext;
        private Button btnWebwoserup;
        private Label label10;
        private TextBox textBox3;
        private Label label12;
        private Button button6;
        private ListView lsWebwoserAccounts;
        private Button btnWebbrowserSendMsg;
        private GroupBox gbWebGroupManagerSettings;
        private TextBox txtWebGroupManagerSendMin;
        private Label label4;
        private TextBox txtWebGroupManagerSendMax;
        private TextBox txtWebManagerSendContext;
        private Label label13;
        private TextBox txtWebGroupManagerSendMaxCount;
        private Label label14;
        private Button btnCollectUserNameGoto;
        private Label lbCollectUsername;
        private TextBox txtCollectUserNameGoto;
        private Button btnCollectUserNameDown;
        private Button btnCollectUserNameUp;
        private Label label17;
        private TextBox txtCollectUserNameSeach;
        private Button btnCollectUsernameSeach;
        private ListView lsCollectUserName;
        private Label lbCollectUserNameShowCount;
        private TabPage tabPageWebbrowserCollect;
        private Button btnWebCollectAdd;
        private Button btnWebCollectGoto;
        private Label lbWebCollectText;
        private Button btnWebCollectNext;
        private Button btnWebCollectPrev;
        private Label label18;
        private TextBox txtWebCollectSearch;
        private Button btnWebCollectSearch;
        private ListView lsWebCollect;
        private Button btnWebbrowserCollectStart;
        private GroupBox groupBox3;
        private TextBox txtSessionSendMsgContexts;
        private Label label20;
        private Label label16;
        private TextBox txtSessionSendMsgCountMax;
        private TextBox txtSessionSendMsgDelayMaxs;
        private TextBox txtSessionSendMsgDelayMins;
        private Label label15;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private TextBox txtSessionCollectNickFilter;
        private TextBox txtWebCollectInfoFilter;
        private TextBox txtWebCollectNickFilter;
        private TextBox txtSessionCollectInfoFilter;
        private CheckBox cboxWebCollectInfoFilter;
        private CheckBox cboxWebCollectNickFilter;
        private CheckBox cboxSessionCollectInfoFilter;
        private CheckBox cboxSessionCollectNickFilter;
        private Button btnSessionSendMsgStart;
        private Button btnSessionCollectStart;
        private LinkLabel llabGetApi;
        private Label label23;
        private Label label26;
        private Label label24;
        private Label label22;
        private Label label21;
        private Label label28;
        private Label label27;
        private Label label29;
        private Label label25;
        private Label label19;
        private Label label30;
        private Label label31;
        private ComboBox cmbMemberP;
        private TabPage tabPageAccount;
        private Label lbAccountSelectPath;
        private Button btnAccountSelecetPath;
        private TextBox txtAccountSeach;
        private Button btnAccountRefresh;
        private Button btnAddAccount;
        private Button btnAccountGoto;
        private Label lbAccount;
        private TextBox txtAccountGoto;
        private Button btcAccountNext;
        private Button btnAccountUp;
        private Label label6;
        private Button btnAccountSeach;
        private ListView lvAccounts;
        private CheckBox cboxSessionSetingDownloadAvatar;
        private CheckBox cboxSessionCollectFilterManager;
        private CheckBox cboxSessionCollectFilterBot;
        private ComboBox cmbMemberOrderBy;
        private ComboBox cmbMemberSelectSendStatus;
    }
}
