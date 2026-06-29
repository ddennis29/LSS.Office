using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LSS.UI.Diagnostics;

public sealed class DiagnosticsForm : Form
{
    private readonly TextBox _output;

    public DiagnosticsForm(IEnumerable<string> lines)
    {
        Text = "LSS Diagnostics";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(760, 520);
        MinimumSize = new Size(600, 360);

        _output = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Both,
            WordWrap = false,
            Font = new Font(FontFamily.GenericMonospace, 10),
            Text = string.Join(Environment.NewLine, lines)
        };

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
            Height = 42,
            Padding = new Padding(8)
        };
        panel.Controls.Add(closeButton);

        Controls.Add(_output);
        Controls.Add(panel);
        AcceptButton = closeButton;
    }
}
