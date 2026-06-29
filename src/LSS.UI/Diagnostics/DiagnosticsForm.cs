using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LSS.Core.Diagnostics;

namespace LSS.UI.Diagnostics;

/// <summary>
/// Developer diagnostics window for inspecting runtime state inside Word.
/// </summary>
public sealed class DiagnosticsForm : Form
{
    public DiagnosticsForm(IEnumerable<DiagnosticsSection> sections)
    {
        Text = "LSS Diagnostics";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(860, 580);
        MinimumSize = new Size(700, 420);

        var tabControl = new TabControl
        {
            Dock = DockStyle.Fill
        };

        foreach (var section in sections.DefaultIfEmpty(new DiagnosticsSection("Diagnostics", new[] { "No diagnostics available." })))
        {
            var textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false,
                Font = new Font(FontFamily.GenericMonospace, 10),
                Text = string.Join(Environment.NewLine, section.Lines)
            };

            var page = new TabPage(section.Title);
            page.Controls.Add(textBox);
            tabControl.TabPages.Add(page);
        }

        var copyButton = new Button
        {
            Text = "Copy Current Tab",
            Dock = DockStyle.Left,
            Width = 140
        };
        copyButton.Click += (_, _) => CopyCurrentTab(tabControl);

        var closeButton = new Button
        {
            Text = "Close",
            Dock = DockStyle.Right,
            Width = 100,
            DialogResult = DialogResult.OK
        };

        var panel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 44,
            Padding = new Padding(8)
        };
        panel.Controls.Add(copyButton);
        panel.Controls.Add(closeButton);

        Controls.Add(tabControl);
        Controls.Add(panel);
        AcceptButton = closeButton;
    }

    private static void CopyCurrentTab(TabControl tabControl)
    {
        if (tabControl.SelectedTab?.Controls.OfType<TextBox>().FirstOrDefault() is { } textBox)
        {
            Clipboard.SetText(textBox.Text);
        }
    }
}
