using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

internal static class Program
{
    static string tool = "MultiMonitorTool.exe";
    static string configFile = "display_config.txt";
    static string display1 = "";
    static string display2 = "";

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        if (!File.Exists(tool))
        {
            MessageBox.Show($"Arquivo '{tool}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!File.Exists(configFile))
        {
            MessageBox.Show($"Arquivo '{configFile}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        LoadDisplayConfig();

        NotifyIcon trayIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Text = "Monitor Switcher",
            Visible = true,
            ContextMenuStrip = new ContextMenuStrip()
        };

        trayIcon.ContextMenuStrip.Items.Add("Modo Reunião", null, (_, __) => SetPrimary(display2, "Modo Reunião"));
        trayIcon.ContextMenuStrip.Items.Add("Modo Jogo", null, (_, __) => SetPrimary(display1, "Modo Jogo"));
        trayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
        trayIcon.ContextMenuStrip.Items.Add("Sair", null, (_, __) =>
        {
            trayIcon.Visible = false;
            Application.Exit();
        });

        Application.Run();
    }

    static void LoadDisplayConfig()
    {
        var lines = File.ReadAllLines(configFile);
        foreach (var line in lines)
        {
            if (line.StartsWith("DISPLAY1="))
                display1 = line.Substring("DISPLAY1=".Length).Trim();
            else if (line.StartsWith("DISPLAY2="))
                display2 = line.Substring("DISPLAY2=".Length).Trim();
        }
    }

    static void SetPrimary(string displayId, string modeName)
    {
        if (string.IsNullOrWhiteSpace(displayId)) return;

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = tool,
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

    static void ShowBalloon(string message)
    {
        NotifyIcon balloon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Visible = true
        };
        balloon.ShowBalloonTip(3000, "Monitor Switcher", message, ToolTipIcon.Info);
        // Oculta após o tempo do balão
        Timer t = new Timer { Interval = 4000 };
        t.Tick += (sender, args) =>
        {
            balloon.Dispose();
            t.Stop();
        };
        t.Start();
    }
}
