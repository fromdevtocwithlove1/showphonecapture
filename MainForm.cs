using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ScrcpyController
{
    public partial class MainForm : Form
    {
        private const string IP_HISTORY_FILE = "ip_history.json";
        private const int MAX_HISTORY = 10;
        private List<DeviceEntry> ipHistory = new List<DeviceEntry>();
        private string adbPath = "adb.exe";
        private string scrcpyPath = "scrcpy.exe";
        private System.Threading.CancellationTokenSource? _connectCts = null;
        private Process? _currentAdbProcess = null;

        private class DeviceEntry
        {
            public string IP { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;

            public override string ToString()
            {
                return string.IsNullOrWhiteSpace(Name) ? IP : $"{Name} ({IP})";
            }
        }

        public MainForm()
        {
            InitializeComponent();
            ApplyModernChrome();
        }

        private void ApplyModernChrome()
        {
            SetDoubleBuffered(headerPanel);
            SetDoubleBuffered(connectionPanel);
            SetDoubleBuffered(historyPanel);
            SetDoubleBuffered(logPanel);

            RoundControl(headerPanel, 22, Color.FromArgb(222, 231, 245));
            RoundControl(connectionPanel, 20, Color.FromArgb(222, 231, 245));
            RoundControl(historyPanel, 20, Color.FromArgb(222, 231, 245));
            RoundControl(logPanel, 20, Color.FromArgb(222, 231, 245));
            RoundControl(txtOutput, 14, Color.FromArgb(226, 232, 240));
            RoundControl(lstIPHistory, 14, Color.FromArgb(226, 232, 240));
            RoundControl(txtDeviceIP, 10, Color.FromArgb(203, 213, 225));

            foreach (var button in new[] { btnConnect, btnRefresh, btnSetupTcpip, btnCheckDevices, btnPushFile, btnClearHistory, btnConnectUSB })
            {
                RoundControl(button, 12, Color.Transparent);
            }
        }

        private static void RoundControl(Control control, int radius, Color borderColor)
        {
            void ApplyRegion()
            {
                control.Region?.Dispose();
                control.Region = new Region(CreateRoundedRectangle(control.ClientRectangle, radius));
            }

            ApplyRegion();
            control.Resize += (_, _) => ApplyRegion();
            control.Paint += (_, e) =>
            {
                if (borderColor == Color.Transparent) return;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = CreateRoundedRectangle(new Rectangle(0, 0, control.Width - 1, control.Height - 1), radius);
                using var pen = new Pen(borderColor, 1);
                e.Graphics.DrawPath(pen, path);
            };
        }

        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();

            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return path;
            }

            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void SetDoubleBuffered(Control control)
        {
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(control, true, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Tìm đường dẫn adb và scrcpy
            FindExecutables();
            
            // Load lịch sử IP
            LoadIPHistory();
            
            LogOutput("Ứng dụng đã sẵn sàng!");
            LogOutput($"ADB: {adbPath}");
            LogOutput($"Scrcpy: {scrcpyPath}");
        }

        private void FindExecutables()
        {
            // Tìm trong thư mục hiện tại
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string parentDir = Directory.GetParent(currentDir)?.FullName ?? currentDir;

            // Tìm adb.exe
            string[] adbPaths = new[]
            {
                Path.Combine(currentDir, "adb.exe"),
                Path.Combine(parentDir, "adb.exe"),
                "adb.exe" // Trong PATH
            };

            foreach (var path in adbPaths)
            {
                if (File.Exists(path) || IsInPath("adb.exe"))
                {
                    adbPath = path;
                    break;
                }
            }

            // Tìm scrcpy.exe
            string[] scrcpyPaths = new[]
            {
                Path.Combine(currentDir, "scrcpy.exe"),
                Path.Combine(parentDir, "scrcpy.exe"),
                "scrcpy.exe" // Trong PATH
            };

            foreach (var path in scrcpyPaths)
            {
                if (File.Exists(path) || IsInPath("scrcpy.exe"))
                {
                    scrcpyPath = path;
                    break;
                }
            }
        }

        private bool IsInPath(string fileName)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "where",
                        Arguments = fileName,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private void LoadIPHistory()
        {
            try
            {
                if (!File.Exists(IP_HISTORY_FILE))
                {
                    return;
                }

                string json = File.ReadAllText(IP_HISTORY_FILE);
                ipHistory = new List<DeviceEntry>();

                try
                {
                    var newFormat = JsonConvert.DeserializeObject<List<DeviceEntry>>(json);
                    if (newFormat != null && newFormat.Count > 0 && newFormat[0] != null
                        && !string.IsNullOrEmpty(newFormat[0].IP))
                    {
                        ipHistory = newFormat.Where(e => e != null && !string.IsNullOrEmpty(e.IP)).ToList();
                    }
                }
                catch
                {
                }

                if (ipHistory.Count == 0)
                {
                    try
                    {
                        var legacy = JsonConvert.DeserializeObject<List<string>>(json);
                        if (legacy != null)
                        {
                            ipHistory = legacy
                                .Where(ip => !string.IsNullOrWhiteSpace(ip))
                                .Select(ip => new DeviceEntry { IP = ip, Name = string.Empty })
                                .ToList();
                        }
                    }
                    catch
                    {
                    }
                }

                UpdateIPHistoryList();
                LogOutput($"Đã tải {ipHistory.Count} IP từ lịch sử");
            }
            catch (Exception ex)
            {
                LogOutput($"Lỗi khi tải lịch sử: {ex.Message}");
            }
        }

        private void SaveIPHistory()
        {
            try
            {
                string json = JsonConvert.SerializeObject(ipHistory, Formatting.Indented);
                File.WriteAllText(IP_HISTORY_FILE, json);
            }
            catch (Exception ex)
            {
                LogOutput($"Lỗi khi lưu lịch sử: {ex.Message}");
            }
        }

        private void AddToIPHistory(string ip, string? deviceName = null)
        {
            if (string.IsNullOrWhiteSpace(ip)) return;

            // Tìm bản ghi cũ để giữ lại tên nếu lần này chưa có
            var existing = ipHistory.FirstOrDefault(e => e.IP == ip);
            string nameToUse = deviceName;
            if (string.IsNullOrWhiteSpace(nameToUse) && existing != null)
            {
                nameToUse = existing.Name;
            }

            if (existing != null)
            {
                ipHistory.Remove(existing);
            }

            ipHistory.Insert(0, new DeviceEntry
            {
                IP = ip,
                Name = nameToUse ?? string.Empty
            });

            if (ipHistory.Count > MAX_HISTORY)
            {
                ipHistory = ipHistory.Take(MAX_HISTORY).ToList();
            }

            UpdateIPHistoryList();
            SaveIPHistory();
        }

        private void UpdateIPHistoryList()
        {
            lstIPHistory.Items.Clear();
            foreach (var entry in ipHistory)
            {
                lstIPHistory.Items.Add(entry);
            }
        }

        private string GetDeviceFriendlyName(string deviceSerial)
        {
            try
            {
                string brand = ExecuteCommand(adbPath, $"-s {deviceSerial} shell getprop ro.product.brand").Trim();
                string model = ExecuteCommand(adbPath, $"-s {deviceSerial} shell getprop ro.product.model").Trim();

                brand = (brand ?? string.Empty).Replace("\r", "").Replace("\n", "").Trim();
                model = (model ?? string.Empty).Replace("\r", "").Replace("\n", "").Trim();

                if (string.IsNullOrEmpty(brand) && string.IsNullOrEmpty(model))
                {
                    return string.Empty;
                }

                if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(model))
                {
                    if (model.StartsWith(brand, StringComparison.OrdinalIgnoreCase))
                    {
                        return model;
                    }
                    return $"{Capitalize(brand)} {model}";
                }

                return string.IsNullOrEmpty(model) ? Capitalize(brand) : model;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string Capitalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        private void LogOutput(string message)
        {
            if (txtOutput.InvokeRequired)
            {
                txtOutput.Invoke(new Action(() => LogOutput(message)));
                return;
            }

            txtOutput.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        private string ExecuteCommand(string fileName, string arguments)
        {
            return ExecuteCommand(fileName, arguments, System.Threading.CancellationToken.None);
        }

        private string ExecuteCommand(string fileName, string arguments, System.Threading.CancellationToken token)
        {
            Process? process = null;
            try
            {
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        WorkingDirectory = Path.GetDirectoryName(fileName) ?? AppDomain.CurrentDomain.BaseDirectory
                    }
                };

                process.Start();
                _currentAdbProcess = process;

                // Đọc output trong khi chờ, để có thể kill nếu bị cancel
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                while (!process.WaitForExit(200))
                {
                    if (token.IsCancellationRequested)
                    {
                        try { process.Kill(); } catch { }
                        return string.Empty;
                    }
                }

                string output = outputTask.Result;
                string error = errorTask.Result;
                _currentAdbProcess = null;

                return string.IsNullOrEmpty(error) ? output : output + "\n" + error;
            }
            catch (Exception ex)
            {
                _currentAdbProcess = null;
                if (token.IsCancellationRequested) return string.Empty;
                return $"Lỗi: {ex.Message}";
            }
        }

        private void btnCheckDevices_Click(object sender, EventArgs e)
        {
            LogOutput("Đang kiểm tra thiết bị USB...");
            btnCheckDevices.Enabled = false;
            
            System.Threading.Tasks.Task.Run(() =>
            {
                string result = ExecuteCommand(adbPath, "devices");
                
                Invoke(new Action(() =>
                {
                    LogOutput(result);
                    
                    // Kiểm tra xem có thiết bị nào không
                    var lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var deviceLines = lines.Where(l => l.Contains("device") && !l.Contains("List of devices")).ToList();
                    
                    if (deviceLines.Count > 0)
                    {
                        LogOutput($"✓ Tìm thấy {deviceLines.Count} thiết bị!");
                        LogOutput("Bây giờ nhấn 'Cài đặt TCP/IP' để bật kết nối WiFi");
                    }
                    else
                    {
                        LogOutput("✗ Không tìm thấy thiết bị USB nào!");
                        LogOutput("Kiểm tra: 1) Dây USB, 2) USB Debugging đã bật, 3) Chấp nhận popup trên điện thoại");
                    }
                    
                    btnCheckDevices.Enabled = true;
                }));
            });
        }

        private void btnSetupTcpip_Click(object sender, EventArgs e)
        {
            LogOutput("Đang cài đặt ADB TCP/IP trên cổng 5555...");
            
            btnSetupTcpip.Enabled = false;
            
            // Chạy trong background thread
            System.Threading.Tasks.Task.Run(() =>
            {
                // Kiểm tra thiết bị trước
                string devicesResult = ExecuteCommand(adbPath, "devices");
                var lines = devicesResult.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var deviceLines = lines.Where(l => l.Contains("device") && !l.Contains("List of devices")).ToList();
                
                if (deviceLines.Count == 0)
                {
                    Invoke(new Action(() =>
                    {
                        LogOutput("✗ Không tìm thấy thiết bị USB!");
                        LogOutput("Vui lòng kết nối USB và nhấn 'Kiểm tra thiết bị USB' trước");
                        btnSetupTcpip.Enabled = true;
                    }));
                    return;
                }
                
                // Có nhiều thiết bị, lấy thiết bị đầu tiên
                string deviceSerial = deviceLines[0].Split('\t')[0].Trim();
                
                string result = ExecuteCommand(adbPath, $"-s {deviceSerial} tcpip 5555");
                
                Invoke(new Action(() =>
                {
                    LogOutput(result);
                    if (result.Contains("restarting") || result.Contains("5555"))
                    {
                        LogOutput("✓ Đã cài đặt thành công! Có thể rút dây USB.");
                        LogOutput("Bây giờ hãy nhập IP thiết bị và nhấn 'Kết nối Scrcpy'");
                        
                        // Lấy IP từ thiết bị nếu có thể
                        string ipResult = ExecuteCommand(adbPath, $"-s {deviceSerial} shell ip addr show wlan0");
                        var ipMatch = System.Text.RegularExpressions.Regex.Match(ipResult, @"inet (\d+\.\d+\.\d+\.\d+)");
                        if (ipMatch.Success)
                        {
                            string detectedIP = ipMatch.Groups[1].Value;
                            txtDeviceIP.Text = detectedIP;
                            LogOutput($"→ Đã tự động điền IP: {detectedIP}");
                        }
                    }
                    else if (result.Contains("more than one device"))
                    {
                        LogOutput("✗ Có nhiều thiết bị. Đã thử chọn thiết bị đầu tiên.");
                    }
                    else
                    {
                        LogOutput("✗ Có lỗi xảy ra. Kiểm tra kết nối USB và USB debugging.");
                    }
                    btnSetupTcpip.Enabled = true;
                }));
            });
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string deviceIP = txtDeviceIP.Text.Trim();
            
            if (string.IsNullOrEmpty(deviceIP))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ IP thiết bị!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogOutput($"Đang kết nối tới {deviceIP}:5555...");
            btnConnect.Enabled = false;
            btnRefresh.Visible = true;

            _connectCts?.Cancel();
            _connectCts?.Dispose();
            _connectCts = new System.Threading.CancellationTokenSource();
            var token = _connectCts.Token;

            System.Threading.Tasks.Task.Run(() =>
            {
                // Kết nối ADB
                string connectResult = ExecuteCommand(adbPath, $"connect {deviceIP}:5555", token);

                if (token.IsCancellationRequested)
                {
                    Invoke(new Action(() =>
                    {
                        btnConnect.Enabled = true;
                        btnRefresh.Visible = false;
                    }));
                    return;
                }

                bool connected = connectResult.Contains("connected") || connectResult.Contains("already connected");
                string deviceName = string.Empty;

                if (connected)
                {
                    // Lấy tên thiết bị qua adb getprop
                    deviceName = GetDeviceFriendlyName($"{deviceIP}:5555");
                }

                Invoke(new Action(() =>
                {
                    if (token.IsCancellationRequested)
                    {
                        btnConnect.Enabled = true;
                        btnRefresh.Visible = false;
                        return;
                    }

                    LogOutput(connectResult);

                    if (connected)
                    {
                        LogOutput("✓ Kết nối ADB thành công!");

                        if (!string.IsNullOrEmpty(deviceName))
                        {
                            LogOutput($"→ Thiết bị: {deviceName}");
                        }

                        // Lưu IP + tên thiết bị vào lịch sử
                        AddToIPHistory(deviceIP, deviceName);

                        // Khởi chạy scrcpy
                        LaunchScrcpy(deviceIP);
                    }
                    else
                    {
                        LogOutput("✗ Không thể kết nối. Kiểm tra IP và đảm bảo đã chạy 'adb tcpip 5555'");
                        btnConnect.Enabled = true;
                        btnRefresh.Visible = false;
                    }
                }));
            });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Hủy token để dừng vòng lặp WaitForExit trong ExecuteCommand
            _connectCts?.Cancel();

            // Kill process adb đang chạy nếu có
            try { _currentAdbProcess?.Kill(); } catch { }
            _currentAdbProcess = null;

            // Reset UI về trạng thái ban đầu
            btnConnect.Enabled = true;
            btnRefresh.Visible = false;

            LogOutput("↺ Đã hủy kết nối.");
        }

        private void LaunchScrcpy(string deviceIP)
        {
            LogOutput("Đang khởi động Scrcpy...");
            
            // Xây dựng tham số
            string arguments = $"-s {deviceIP}:5555";
            
            if (chkTurnOffScreen.Checked)
            {
                arguments += " --turn-screen-off";
                LogOutput("→ Tắt màn hình thiết bị");
            }
            
            if (chk720p.Checked)
            {
                arguments += " --max-size=1280";
                LogOutput("→ Độ phân giải 720p (1280px)");
            }

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = scrcpyPath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        WorkingDirectory = Path.GetDirectoryName(scrcpyPath) ?? AppDomain.CurrentDomain.BaseDirectory
                    }
                };

                process.Start();
                LogOutput("✓ Scrcpy đã được khởi động!");
                LogOutput($"Lệnh: {scrcpyPath} {arguments}");
            }
            catch (Exception ex)
            {
                LogOutput($"✗ Lỗi khi khởi động Scrcpy: {ex.Message}");
            }
            finally
            {
                Invoke(new Action(() =>
                {
                    btnConnect.Enabled = true;
                    btnRefresh.Visible = false;
                    _connectCts?.Dispose();
                    _connectCts = null;
                }));
            }
        }

        private void btnConnectUSB_Click(object sender, EventArgs e)
        {
            LogOutput("════════════════════════");
            LogOutput("Đang tìm thiết bị kết nối qua dây USB...");
            btnConnectUSB.Enabled = false;

            System.Threading.Tasks.Task.Run(() =>
            {
                string devicesResult = ExecuteCommand(adbPath, "devices");
                var lines = devicesResult.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Lọc thiết bị USB thực (serial không chứa dấu ':' là USB, không phải WiFi)
                var usbDevices = lines
                    .Where(l => !l.StartsWith("List of devices") && l.Contains("\t"))
                    .Select(l => new
                    {
                        Serial = l.Split('\t')[0].Trim(),
                        Status = l.Split('\t')[1].Trim()
                    })
                    .Where(d => d.Status.Equals("device", StringComparison.OrdinalIgnoreCase)
                                && !d.Serial.Contains(":")) // USB serial không có ':'
                    .ToList();

                if (usbDevices.Count == 0)
                {
                    Invoke(new Action(() =>
                    {
                        LogOutput("✗ Không tìm thấy thiết bị USB nào!");
                        LogOutput("→ Kiểm tra: 1) Dây USB, 2) USB Debugging đã bật, 3) Chấp nhận popup trên điện thoại.");
                        btnConnectUSB.Enabled = true;
                    }));
                    return;
                }

                // Lấy thiết bị đầu tiên
                string serial = usbDevices[0].Serial;
                string deviceName = GetDeviceFriendlyName(serial);

                Invoke(new Action(() =>
                {
                    LogOutput($"✓ Tìm thấy thiết bị USB: {serial}");
                    if (!string.IsNullOrEmpty(deviceName))
                        LogOutput($"→ Thiết bị: {deviceName}");
                    LaunchScrcpyUSB(serial);
                }));
            });
        }

        private void LaunchScrcpyUSB(string deviceSerial)
        {
            LogOutput("Đang khởi động Scrcpy qua USB...");

            string arguments = $"-s {deviceSerial}";

            if (chkTurnOffScreen.Checked)
            {
                arguments += " --turn-screen-off";
                LogOutput("→ Tắt màn hình thiết bị");
            }

            if (chk720p.Checked)
            {
                arguments += " --max-size=1280";
                LogOutput("→ Độ phân giải 720p (1280px)");
            }

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = scrcpyPath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        WorkingDirectory = Path.GetDirectoryName(scrcpyPath) ?? AppDomain.CurrentDomain.BaseDirectory
                    }
                };

                process.Start();
                LogOutput("✓ Scrcpy đã được khởi động qua USB!");
                LogOutput($"Lệnh: {scrcpyPath} {arguments}");
            }
            catch (Exception ex)
            {
                LogOutput($"✗ Lỗi khi khởi động Scrcpy: {ex.Message}");
            }
            finally
            {
                btnConnectUSB.Enabled = true;
            }
        }

        private void lstIPHistory_Click(object sender, EventArgs e)
        {
            if (lstIPHistory.SelectedItem is DeviceEntry entry)
            {
                txtDeviceIP.Text = entry.IP;
            }
        }

        private void lstIPHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lstIPHistory.SelectedItem is DeviceEntry entry)
            {
                txtDeviceIP.Text = entry.IP;
                if (!string.IsNullOrEmpty(entry.Name))
                {
                    LogOutput($"✓ Đã chọn: {entry.Name} ({entry.IP})");
                }
                else
                {
                    LogOutput($"✓ Đã chọn IP: {entry.IP}");
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn xóa toàn bộ lịch sử?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                ipHistory.Clear();
                UpdateIPHistoryList();
                SaveIPHistory();
                LogOutput("Đã xóa lịch sử IP");
            }
        }

        private void btnPushFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file để gửi vào điện thoại (DCIM/Camera)";
                ofd.Multiselect = true;
                ofd.Filter = "Hình ảnh & Video|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp;*.heic;*.mp4;*.mov;*.avi;*.mkv;*.3gp;*.webm" +
                             "|Hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp;*.heic" +
                             "|Video|*.mp4;*.mov;*.avi;*.mkv;*.3gp;*.webm" +
                             "|Tất cả file|*.*";

                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                PushFilesToDevice(ofd.FileNames);
            }
        }

        private void PushFilesToDevice(string[] files)
        {
            if (files == null || files.Length == 0) return;

            btnPushFile.Enabled = false;
            LogOutput("════════════════════════");
            LogOutput($"Chuẩn bị gửi {files.Length} file vào điện thoại...");

            string preferredIP = txtDeviceIP.Text.Trim();

            System.Threading.Tasks.Task.Run(() =>
            {
                string deviceSerial = GetActiveDeviceSerial(preferredIP);

                if (string.IsNullOrEmpty(deviceSerial))
                {
                    Invoke(new Action(() =>
                    {
                        LogOutput("✗ Không tìm thấy thiết bị nào đã kết nối!");
                        LogOutput("→ Hãy cắm USB hoặc kết nối WiFi (Scrcpy) trước rồi thử lại.");
                        btnPushFile.Enabled = true;
                    }));
                    return;
                }

                Invoke(new Action(() => LogOutput($"→ Sử dụng thiết bị: {deviceSerial}")));

                const string targetDir = "/sdcard/DCIM/Camera/";

                string mkdirResult = ExecuteCommand(adbPath, $"-s {deviceSerial} shell mkdir -p {targetDir}");
                if (!string.IsNullOrWhiteSpace(mkdirResult))
                {
                    Invoke(new Action(() => LogOutput($"  (mkdir) {mkdirResult.Trim()}")));
                }

                int successCount = 0;
                int failCount = 0;
                var pushedFiles = new List<string>();

                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    string fileName = Path.GetFileName(file);
                    int idx = i + 1;

                    if (!File.Exists(file))
                    {
                        failCount++;
                        Invoke(new Action(() => LogOutput($"[{idx}/{files.Length}] ✗ Không tồn tại: {file}")));
                        continue;
                    }

                    long sizeBytes = new FileInfo(file).Length;
                    string sizeStr = FormatFileSize(sizeBytes);

                    Invoke(new Action(() => LogOutput($"[{idx}/{files.Length}] Đang gửi: {fileName} ({sizeStr})...")));

                    string pushResult = ExecuteCommand(adbPath, $"-s {deviceSerial} push \"{file}\" \"{targetDir}\"");
                    string trimmed = (pushResult ?? string.Empty).Trim();

                    bool failed = trimmed.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0
                                  || trimmed.IndexOf("failed", StringComparison.OrdinalIgnoreCase) >= 0
                                  || trimmed.IndexOf("adb: ", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (failed)
                    {
                        failCount++;
                        Invoke(new Action(() => LogOutput($"  ✗ Thất bại: {trimmed}")));
                    }
                    else
                    {
                        successCount++;
                        pushedFiles.Add(fileName);
                        string shortMsg = trimmed.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "OK";
                        Invoke(new Action(() => LogOutput($"  ✓ {shortMsg}")));
                    }
                }

                if (pushedFiles.Count > 0)
                {
                    Invoke(new Action(() => LogOutput("→ Đang yêu cầu Android quét lại thư viện ảnh...")));

                    foreach (var name in pushedFiles)
                    {
                        string remotePath = targetDir + name;
                        string encodedPath = UriEncodePath(remotePath);
                        ExecuteCommand(adbPath,
                            $"-s {deviceSerial} shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d \"file://{encodedPath}\"");
                    }

                    ExecuteCommand(adbPath,
                        $"-s {deviceSerial} shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d \"file:///sdcard/DCIM/Camera\"");
                }

                Invoke(new Action(() =>
                {
                    LogOutput("────────────────────────");
                    LogOutput($"✓ Hoàn tất! Thành công: {successCount} | Thất bại: {failCount}");
                    LogOutput($"Đường dẫn: {targetDir}");
                    if (successCount > 0)
                    {
                        LogOutput("Mở Gallery / Photos trên điện thoại để xem ảnh/video mới.");
                    }
                    btnPushFile.Enabled = true;
                }));
            });
        }

        private string GetActiveDeviceSerial(string preferredIP)
        {
            string devicesResult = ExecuteCommand(adbPath, "devices");
            var lines = devicesResult.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var devices = lines
                .Where(l => !l.StartsWith("List of devices") && l.Contains("\t"))
                .Select(l => new
                {
                    Serial = l.Split('\t')[0].Trim(),
                    Status = l.Split('\t')[1].Trim()
                })
                .Where(d => d.Status.Equals("device", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (devices.Count == 0) return string.Empty;

            if (!string.IsNullOrEmpty(preferredIP))
            {
                var match = devices.FirstOrDefault(d => d.Serial.StartsWith(preferredIP + ":"));
                if (match != null) return match.Serial;
            }

            return devices[0].Serial;
        }

        private static string FormatFileSize(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB" };
            double size = bytes;
            int unit = 0;
            while (size >= 1024 && unit < units.Length - 1)
            {
                size /= 1024;
                unit++;
            }
            return $"{size:0.##} {units[unit]}";
        }

        private static string UriEncodePath(string path)
        {
            var sb = new System.Text.StringBuilder();
            foreach (char c in path)
            {
                if (c == '/' || c == ':' ||
                    (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') ||
                    c == '-' || c == '_' || c == '.' || c == '~')
                {
                    sb.Append(c);
                }
                else
                {
                    foreach (byte b in System.Text.Encoding.UTF8.GetBytes(new[] { c }))
                    {
                        sb.Append('%').Append(b.ToString("X2"));
                    }
                }
            }
            return sb.ToString();
        }
    }
}
