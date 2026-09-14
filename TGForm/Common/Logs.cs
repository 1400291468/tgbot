using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Common
{
    public enum LogLevel
    {
        Info,
        Success,
        Warning,
        Error,
        Debug
    }

    public static class Logger
    {
        // ========== 引用你的控件 ==========
        private static RichTextBox _logControl;

        // ========== 文件日志 ==========
        private static string _logFilePath;
        private static readonly object _fileLock = new object();

        // ========== 性能优化 ==========
        private static readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private static readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private static Task _flushTask;
        private static int _maxLines = 1000;
        private static bool _isInitialized = false;

        // ========== 颜色配置 ==========
        private static readonly Dictionary<LogLevel, Color> _levelColors = new Dictionary<LogLevel, Color>
    {
        { LogLevel.Info, Color.Black },
        { LogLevel.Success, Color.Green },
        { LogLevel.Warning, Color.Orange },
        { LogLevel.Error, Color.Red },
        { LogLevel.Debug, Color.Gray }
    };

        private static readonly Dictionary<LogLevel, string> _levelPrefix = new Dictionary<LogLevel, string>
    {
        { LogLevel.Info, "[INFO]" },
        { LogLevel.Success, "[SUCCESS]" },
        { LogLevel.Warning, "[WARNING]" },
        { LogLevel.Error, "[ERROR]" },
        { LogLevel.Debug, "[DEBUG]" }
    };

        // ========== 初始化（传入你的控件） ==========
        public static void Initialize(RichTextBox logControl, string logFileName = null, int maxLines = 1000)
        {
            if (_isInitialized) return;

            _logControl = logControl ?? throw new ArgumentNullException(nameof(logControl));
            _maxLines = maxLines;

            // 设置日志文件路径
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            string fileName = logFileName ?? $"log_{DateTime.Now:yyyyMMdd}.txt";
            _logFilePath = Path.Combine(logDir, fileName);

            // 设置 RichTextBox 属性
            _logControl.ReadOnly = true;
            _logControl.WordWrap = false;
            _logControl.BackColor = Color.White;

            // 启动后台写入任务
            _flushTask = Task.Run(FlushLogQueue);

            _isInitialized = true;
            Log("日志系统初始化成功", LogLevel.Success);
        }

        // ========== 核心日志方法（线程安全） ==========
        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (!_isInitialized) return;

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logEntry = $"[{timestamp}] {_levelPrefix[level]} {message}";

            // 1. 写入队列（异步写入文件）
            _logQueue.Enqueue(logEntry);

            // 2. 更新 UI（跨线程安全）
            UpdateUI(logEntry, level);
        }

        // ========== UI 更新（跨线程安全） ==========
        private static void UpdateUI(string logEntry, LogLevel level)
        {
            if (_logControl == null || _logControl.IsDisposed) return;

            if (_logControl.InvokeRequired)
            {
                _logControl.BeginInvoke(new Action<string, LogLevel>(UpdateUI), logEntry, level);
                return;
            }

            try
            {
                // 限制行数
                if (_logControl.Lines.Length > _maxLines)
                {
                    int removeLines = _logControl.Lines.Length - _maxLines + 50;
                    int removeChars = 0;
                    for (int i = 0; i < removeLines && i < _logControl.Lines.Length; i++)
                    {
                        removeChars += _logControl.Lines[i].Length + 1;
                    }
                    _logControl.SelectionStart = 0;
                    _logControl.SelectionLength = removeChars;
                    _logControl.SelectedText = "";
                }

                // 设置颜色并追加文本
                _logControl.SelectionStart = _logControl.TextLength;
                _logControl.SelectionLength = 0;
                _logControl.SelectionColor = _levelColors[level];
                _logControl.AppendText(logEntry + "\n");
                _logControl.ScrollToCaret();
                _logControl.SelectionColor = Color.Black;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UI 更新失败: {ex.Message}");
            }
        }

        // ========== 后台写入文件 ==========
        private static async Task FlushLogQueue()
        {
            while (!_cancellationToken.Token.IsCancellationRequested)
            {
                try
                {
                    var batch = new System.Collections.Generic.List<string>();
                    while (_logQueue.TryDequeue(out string entry))
                    {
                        batch.Add(entry);
                        if (batch.Count >= 50) break;
                    }

                    if (batch.Count > 0)
                    {
                        await File.AppendAllLinesAsync(_logFilePath, batch);
                    }

                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"写入文件失败: {ex.Message}");
                    await Task.Delay(1000);
                }
            }
        }

        // ========== 手动刷新（确保所有日志写入文件） ==========
        public static void Flush()
        {
            var batch = new System.Collections.Generic.List<string>();
            while (_logQueue.TryDequeue(out string entry))
            {
                batch.Add(entry);
            }

            if (batch.Count > 0)
            {
                lock (_fileLock)
                {
                    File.AppendAllLines(_logFilePath, batch);
                }
            }
        }

        // ========== 关闭日志系统 ==========
        public static void Shutdown()
        {
            _cancellationToken.Cancel();
            Flush();
            _flushTask?.Wait(3000);
            _isInitialized = false;
        }

        // ========== 清空界面日志 ==========
        public static void ClearUI()
        {
            if (_logControl == null || _logControl.IsDisposed) return;

            if (_logControl.InvokeRequired)
            {
                _logControl.BeginInvoke(new Action(ClearUI));
                return;
            }

            _logControl.Clear();
        }

        // ========== 辅助方法 ==========
        public static string GetLogFilePath() => _logFilePath;

        public static void OpenLogFolder()
        {
            string folder = Path.GetDirectoryName(_logFilePath);
            if (Directory.Exists(folder))
            {
                System.Diagnostics.Process.Start("explorer.exe", folder);
            }
        }

        public static void LogException(Exception ex, string customMessage = "")
        {
            string msg = string.IsNullOrEmpty(customMessage)
                ? ex.ToString()
                : $"{customMessage}\n{ex}";
            Log(msg, LogLevel.Error);
        }
    }
}
