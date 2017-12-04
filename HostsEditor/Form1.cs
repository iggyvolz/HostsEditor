using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace HostsEditor
{
    public class Form1:Form
    {
        private HostsController hosts;
        public Form1()
        {
            Text = "Hosts Editor";
            AutoScroll = true;
            VerticalScroll.Enabled = true;
            Dictionary<PlatformID, string> hostsLocations = new Dictionary<PlatformID, string>()
            {
                { PlatformID.MacOSX, "/etc/hosts" },
                { PlatformID.Unix, "/etc/hosts" },
                { PlatformID.Win32NT, "C:\\Windows\\System32\\drivers\\etc\\hosts" },
                { PlatformID.Win32S, "C:\\Windows\\System32\\drivers\\etc\\hosts" },
                { PlatformID.Win32Windows, "C:\\Windows\\System32\\drivers\\etc\\hosts" },
                { PlatformID.WinCE, "C:\\Windows\\System32\\drivers\\etc\\hosts" },
                // { PlatformID.Xbox, String.Empty }
            };
            hosts = new HostsController(hostsLocations[Environment.OSVersion.Platform]);
            // Initial GUI setup
            addButton.Text = "+";
            addButton.Click += (object _, EventArgs __) => { hosts.Entries.Add(new HostsController.Entry()); SetupGUI(); VerticalScroll.Value = VerticalScroll.Maximum; PerformLayout(); };
            saveButton.Text = "Save";
            saveButton.Click += (object _, EventArgs __) => hosts.Export();
            revertButton.Text = "Revert";
            revertButton.Click += (object _, EventArgs __) => hosts.Import();
            SetupGUI();
        }
        private Button addButton = new Button();
        private Button saveButton = new Button();
        private Button revertButton = new Button();
        private void SetupGUI()
        {
            Controls.Clear();
            int y = 5;
            foreach(HostsController.Entry entry in hosts.Entries)
            {
                // Add text box
                TextBox ipBox = new TextBox();
                ipBox.Location = new System.Drawing.Point(5, y);
                ipBox.Text = entry.IP;
                ipBox.TextChanged += (object _, EventArgs __) => entry.IP = ipBox.Text;
                Controls.Add(ipBox);
                TextBox nameBox = new TextBox();
                nameBox.Location = new System.Drawing.Point(10 + ipBox.Width, y);
                nameBox.Text = entry.Name;
                nameBox.TextChanged += (object _, EventArgs __) => entry.Name = nameBox.Text;
                Controls.Add(nameBox);
                // Remove button
                Button removeButton = new Button();
                removeButton.Location = new System.Drawing.Point(20 + ipBox.Width + nameBox.Width, y-1);
                removeButton.Text = "-";
                removeButton.Click += (object _, EventArgs __) => { hosts.Entries.Remove(entry); SetupGUI(); };
                removeButton.Width = 25;
                Controls.Add(removeButton);
                y += 10; // Add margin
                y += ipBox.Height; // Add height of textbox
            }
            // Add add entry button
            addButton.Location = new System.Drawing.Point(85, y - 1);
            y += 5; // Add margin
            y += addButton.Height;
            // Add save button
            saveButton.Location = new System.Drawing.Point(45, y - 1);
            // Add revert button
            revertButton.Location = new System.Drawing.Point(135, y - 1);
            Controls.Add(addButton);
            Controls.Add(saveButton);
            Controls.Add(revertButton);
        }
    }
}