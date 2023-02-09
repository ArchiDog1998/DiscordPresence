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

        public static void Initialize()
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

            UpdateValue();
        }


        public static void UpdateValue()
        {
            UpdateRHInfo();
            UpdateGHInfo();
            client.UpdateStartTime(startTime);
        }


        static DateTime startTime = DateTime.UtcNow;
        static string rhinoFile;
        static void UpdateRHInfo()
        {
            if (!true) return;
            if (RhinoDoc.ActiveDoc == null) return;

            var rhFile = RhinoDoc.ActiveDoc.Name;
            client.UpdateDetails(rhFile);

            if (true && rhFile != rhinoFile)
            {
                rhinoFile = rhFile;
                startTime = DateTime.UtcNow;
            }
        }


        static string grasshopperFile;
        static void UpdateGHInfo()
        {
            if(!true)
            {
                client.UpdateSmallAsset();
                return;
            }
            if (Instances.ActiveCanvas?.Document == null) return;

            string ghFile = Instances.ActiveCanvas.Document.DisplayName + ".gh";
            client.UpdateState(ghFile);
            client.UpdateSmallAsset("grasshopper");

            if(true && ghFile != grasshopperFile)
            {
                grasshopperFile = ghFile;
                startTime = DateTime.Now;
            }
        }

        public static void Deinitialize()
        {
            client.Dispose();
        }
    }
}
