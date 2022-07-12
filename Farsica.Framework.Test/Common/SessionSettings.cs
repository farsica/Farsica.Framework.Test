using System.Diagnostics.CodeAnalysis;

namespace Farsica.Framework.Test.Common;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class SessionSettings
{
    public BrowserType BrowserType { get; set; }
    public bool Headless { get; set; }
    public string? DownloadDirectory { get; set; } = string.Empty;
    public uint DefaultTimeoutSeconds { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public Uri? ApplicationUrl { get; set; }
}