using System.Diagnostics.CodeAnalysis;
using static Farsica.Framework.Test.Common.Constants;

namespace Farsica.Framework.Test.Common;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class SessionSettings
{
	public BrowserType BrowserType { get; set; } = BrowserType.Chrome;
	public bool Headless { get; set; }
	public string? DownloadDirectory { get; set; } = string.Empty;
	public uint DefaultTimeoutSeconds { get; set; }
	public string? UserName { get; set; }
	public string? Password { get; set; }
	public Uri? ApplicationUrl { get; set; }
	public string? ScreenshotsDirectory { get; set; } = DefaultScreenshotsDirectory;
}