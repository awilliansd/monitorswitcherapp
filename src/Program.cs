using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MonitorSwitcherApp;

internal static class Program
{
    private const string Tool = "MultiMonitorTool.exe";
    private const string ConfigFile = "display_config.txt";
    private static string _display1 = "";
    private static string _display2 = "";

    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        if (!File.Exists(Tool))
        {
            MessageBox.Show($"Arquivo '{Tool}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!File.Exists(ConfigFile))
        {
            MessageBox.Show($"Arquivo '{ConfigFile}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        LoadDisplayConfig();

        var trayIcon = new NotifyIcon
        {
            Icon = new Icon("monitorswitcher.ico"),
            Text = "Monitor Switcher",
            Visible = true,
            ContextMenuStrip = new ContextMenuStrip()
        };

        trayIcon.ContextMenuStrip.Items.Add("Modo Reunião", null, (_, _) => SetPrimary(_display2, "Modo Reunião"));
        trayIcon.ContextMenuStrip.Items.Add("Modo Jogo", null, (_, _) => SetPrimary(_display1, "Modo Jogo"));
        trayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
        trayIcon.ContextMenuStrip.Items.Add("Sair", null, (_, _) =>
        {
            trayIcon.Visible = false;
            Application.Exit();
        });

        Application.Run();
    }

    static void LoadDisplayConfig()
    {
        var lines = File.ReadAllLines(ConfigFile);
        foreach (var line in lines)
        {
            if (line.StartsWith("DISPLAY1="))
                _display1 = line.Substring("DISPLAY1=".Length).Trim();
            else if (line.StartsWith("DISPLAY2="))
                _display2 = line.Substring("DISPLAY2=".Length).Trim();
        }
    }

    private static void SetPrimary(string displayId, string modeName)
    {
        if (string.IsNullOrWhiteSpace(displayId)) return;

        var psi = new ProcessStartInfo
        {
            FileName = Tool,
            Arguments = $"/SetPrimary \"{displayId}\"",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        try
        {
            Process.Start(psi);
            ShowBalloon($"Alterado para {modeName}.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao definir monitor principal:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ShowBalloon(string message)
    {
        var balloon = new NotifyIcon
        {
            Icon = new Icon("monitorswitcher.ico"),
            Visible = true
        };
        
        balloon.ShowBalloonTip(3000, "Monitor Switcher", message, ToolTipIcon.Info);
        
        // Oculta após o tempo do balão
        var t = new Timer { Interval = 4000 };
        
        t.Tick += (_, _) =>
        {
            balloon.Dispose();
            t.Stop();
        };
        
        t.Start();
    }
}