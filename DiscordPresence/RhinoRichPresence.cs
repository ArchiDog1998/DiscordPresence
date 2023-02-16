using DiscordRPC;
using DiscordRPC.Logging;
using Grasshopper;
using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPresence
{
    internal static class RhinoRichPresence
    {
        const string CLIENT_ID = "1073051898619367515";
        public static DiscordRpcClient client;

        private static void Initialize()
        {
            if (client != null) return;

            client = new DiscordRpcClient(CLIENT_ID);

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Assets = new Assets()
                {
                    LargeImageKey = "rhinoceros3d",
                    //LargeImageText = "I don't what to say.",
                },
                Timestamps = new Timestamps(startTime),
            });
        }

        public static void UpdateValue() => Task.Run(() =>
            {
                Initialize();
                UpdateRHInfo();
                UpdateGHInfo();
            });

        static DateTime startTime = DateTime.UtcNow;
        static string rhinoFile;
        static void UpdateRHInfo()
        {
            if (!DiscordPresencePlugin.Instance.ShowRhinocerosFile
                || RhinoDoc.ActiveDoc == null)
            {
                client.UpdateDetails("");
                rhinoFile = "";
                return;
            }

            var rhFile = RhinoDoc.ActiveDoc.Name ?? "Untitled.3dm";
            client.UpdateDetails(rhFile);

            if (rhFile != rhinoFile)
            {
                rhinoFile = rhFile;
                UpdateTime();
            }
        }

        static string grasshopperFile;
        static void UpdateGHInfo()
        {
            if(!DiscordPresencePlugin.Instance.ShowGrasshopperFile
                || Instances.ActiveCanvas?.Document == null)
            {
                client.UpdateSmallAsset("");
                client.UpdateState("");
                grasshopperFile = "";
                return;
            }

            string ghFile = Instances.ActiveCanvas.Document.DisplayName + ".gh";
            client.UpdateState(ghFile);
            client.UpdateSmallAsset("grasshopper");

            if(ghFile != grasshopperFile)
            {
                grasshopperFile = ghFile;
                UpdateTime();
            }
        }

        static void UpdateTime() => client.UpdateStartTime(DateTime.UtcNow);


        public static void Deinitialize()
        {
            client.Dispose();
        }
    }
}
