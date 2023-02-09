using Eto.Drawing;
using Eto.Forms;
using Rhino;
using Rhino.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPresence
{
    internal class DiscordOptions : OptionsDialogPage
    {
        private OptionsPagePanel _panel;
        public DiscordOptions() : base("Discord Options")
        {
        }

        public override object PageControl => _panel ?? (_panel = new OptionsPagePanel());

        public override System.Drawing.Image PageImage => Properties.Resources.logo;

        public override string LocalPageTitle => "Discord Presence";
    }

    internal class OptionsPagePanel : Panel
    {
        public OptionsPagePanel()
        {
            var rhinoCheckBox = new CheckBox
            { 
                Text = "Show Rhinocero File",
                Checked = DiscordPresencePlugin.Instance.ShowRhinocerosFile,
            };
            rhinoCheckBox.CheckedChanged += (sender, e) => 
            DiscordPresencePlugin.Instance.ShowRhinocerosFile = !DiscordPresencePlugin.Instance.ShowRhinocerosFile;

            var grasshopperCheckBox = new CheckBox
            {
                Text = "Show Grasshopper File",
                Checked = DiscordPresencePlugin.Instance.ShowGrasshopperFile,
            };
            grasshopperCheckBox.CheckedChanged += (sender, e) =>
            DiscordPresencePlugin.Instance.ShowGrasshopperFile = !DiscordPresencePlugin.Instance.ShowGrasshopperFile;

            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };
            layout.AddSeparateRow(rhinoCheckBox, null);
            layout.AddSeparateRow(grasshopperCheckBox, null);
            layout.Add(null);
            Content = layout;
        }
    }
}
