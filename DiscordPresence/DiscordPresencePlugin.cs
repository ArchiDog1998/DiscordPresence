using Grasshopper;
using Rhino;
using Rhino.PlugIns;
using Rhino.UI;
using System;
using System.Collections.Generic;

namespace DiscordPresence
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class DiscordPresencePlugin : Rhino.PlugIns.PlugIn
    {
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
            base.OptionsDialogPages(pages);
        }

        ~ DiscordPresencePlugin()
        {
            RhinoRichPresence.Deinitialize();
            RhinoDoc.ActiveDocumentChanged -= RhinoDoc_ActiveDocumentChanged;
            Instances.CanvasCreated -= Instances_CanvasCreated;
            Instances.ActiveCanvas.DocumentChanged -= Canvas_DocumentChanged;
        }
    }
}