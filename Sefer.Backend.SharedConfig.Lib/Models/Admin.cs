// Note, this class is used to parse a json node and this not initiated in compile time. 
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Sefer.Backend.SharedConfig.Lib.Models;

internal class Admin
{
    public string Host { get; set; }

    public string Environment { get; set; }
}