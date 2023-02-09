using Rhino.PlugIns;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Plug-in Description Attributes - all of these are optional.
// These will show in Rhino's option dialog, in the tab Plug-ins.
[assembly: PlugInDescription(DescriptionType.Address, "")]
[assembly: PlugInDescription(DescriptionType.Country, "China")]
[assembly: PlugInDescription(DescriptionType.Email, "1123993881@qq.com")]
[assembly: PlugInDescription(DescriptionType.Phone, "13656693463")]
[assembly: PlugInDescription(DescriptionType.Fax, "")]
[assembly: PlugInDescription(DescriptionType.Organization, "中建五局设计技术科研院")]
[assembly: PlugInDescription(DescriptionType.UpdateUrl, "")]
[assembly: PlugInDescription(DescriptionType.WebSite, "")]

// Icons should be Windows .ico files and contain 32-bit images in the following sizes: 16, 24, 32, 48, and 256.
[assembly: PlugInDescription(DescriptionType.Icon, "DiscordPresence.EmbeddedResources.plugin-utility.ico")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
// This will also be the Guid of the Rhino plug-in
[assembly: Guid("ef526587-4b19-4084-b359-10ab3a25179c")]
