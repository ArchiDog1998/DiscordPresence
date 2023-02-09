using Grasshopper;
using Rhino;
using Rhino.PlugIns;
using Rhino.UI;
using System;
using System.Collections.Generic;
using System.Runtime;

namespace DiscordPresence
{
    public class DiscordPresencePlugin : PlugIn
    {
        public bool  ShowRhinocerosFile 
        { 
            get => Settings.TryGetBool(nameof(ShowRhinocerosFile), out var value) ? value : true;
            set
            {
                Settings.SetBool(nameof(ShowRhinocerosFile), value);
                RhinoRichPresence.UpdateValue();
            }
        }

        public bool ShowGrasshopperFile
        {
            get => Settings.TryGetBool(nameof(ShowGrasshopperFile), out var value) ? value : true;
            set
            {
                Settings.SetBool(nameof(ShowGrasshopperFile), value);
                RhinoRichPresence.UpdateValue();
            }
        }

        public override PlugInLoadTime LoadTime => PlugInLoadTime.AtStartup;
        public DiscordPresencePlugin()
        {
            Instance = this;
            RhinoRichPresence.Initialize();

            RhinoDoc.ActiveDocumentChanged += RhinoDoc_ActiveDocumentChanged;
            Instances.CanvasCreated += Instances_CanvasCreated;
        }

        private void Instances_CanvasCreated(Grasshopper.GUI.Canvas.GH_Canvas canvas)
        {
            Instances.CanvasCreated -= Instances_CanvasCreated;
            canvas.DocumentChanged += Canvas_DocumentChanged;
        }

        private void Canvas_DocumentChanged(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.Canvas.GH_CanvasDocumentChangedEventArgs e)
        {
            RhinoRichPresence.UpdateValue();
        }

        private void RhinoDoc_ActiveDocumentChanged(object sender, DocumentEventArgs e)
        {
            RhinoRichPresence.UpdateValue();
        }

        ///<summary>Gets the only instance of the DiscordPresencePlugin plug-in.</summary>
        public static DiscordPresencePlugin Instance { get; private set; }


        protected override void OptionsDialogPages(List<OptionsDialogPage> pages)
        {
            pages.Add(new DiscordOptions());
        }

        ~ DiscordPresencePlugin()
        {
            try
            {
                RhinoRichPresence.Deinitialize();
                RhinoDoc.ActiveDocumentChanged -= RhinoDoc_ActiveDocumentChanged;
                Instances.CanvasCreated -= Instances_CanvasCreated;
                Instances.ActiveCanvas.DocumentChanged -= Canvas_DocumentChanged;
            }
            catch
            {

            }
        }
    }
}