// Note, this class is used to parse a json node and this not initiated in compile time.
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global
namespace Sefer.Backend.SharedConfig.Lib.Models;

internal class Index
{
    public List<string> Sites { get; set; }

    public List<string> Regions { get; set; }

    public List<string> Environments { get; set; }

    public List<Admin> Admins { get; set; }
}

