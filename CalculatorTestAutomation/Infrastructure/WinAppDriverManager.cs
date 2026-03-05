using System.Diagnostics;
using System.Net.Sockets;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace CalculatorTestAutomation.Infrastructure;

public class WinAppDriverManager
{
    private Process? _winAppDriverProcess;

        private const string WinAppDriverPath =
            @"C:\Program Files\Windows Application Driver\WinAppDriver.exe";

        private const string Host = "127.0.0.1";
        private const int Port = 4723;

        public void StartWinAppDriver()
        {
            StartWinAppDriverIfNeeded();
        }

        public WindowsDriver<WindowsElement> CreateCalculatorSession()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("platformName", "Windows");
            options.AddAdditionalCapability("app", Constants.CalculatorAppId);

            return new WindowsDriver<WindowsElement>(
                new Uri($"http://{Constants.Host}:{Constants.Port}"), options);
        }

        public void CloseCalculatorProcess()
        {
            foreach (var process in Process.GetProcessesByName("Calculator"))
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch { }
            }
        }
        
        public void StopWinAppDriver()
        {
            if (_winAppDriverProcess == null || _winAppDriverProcess.HasExited) return;
            _winAppDriverProcess.Kill();
            _winAppDriverProcess.Dispose();
        }

        private void StartWinAppDriverIfNeeded()
        {
            if (IsPortOpen(Port))
                return;

            var startInfo = new ProcessStartInfo
            {
                FileName = WinAppDriverPath,
                Arguments = $"{Host} {Port}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _winAppDriverProcess = Process.Start(startInfo);
            WaitForPort(Port, TimeSpan.FromSeconds(10));
        }

        private static bool IsPortOpen(int port)
        {
            try
            {
                using var client = new TcpClient("127.0.0.1", port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void WaitForPort(int port, TimeSpan timeout)
        {
            var start = DateTime.UtcNow;
            while (!IsPortOpen(port))
            {
                if (DateTime.UtcNow - start > timeout)
                    throw new TimeoutException("WinAppDriver did not start in time");

                Thread.Sleep(200);
            }
        }
}