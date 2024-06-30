// See https://aka.ms/new-console-template for more information

using MetroidFusionExtractor.Model.Services;
using MetroidFusionExtractor.Model.Services.Memory.Factory;
using Microsoft.Extensions.DependencyInjection;

const string romPath = "rom/Metroid Fusion (Europe) (En,Fr,De,Es,It).gba";

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<RomParser>();

    services.AddScoped<RomFactory>();
    services.AddScoped<RoomFactory>();
}

ServiceCollection services = new();
ConfigureServices(services);
using var provider = services.BuildServiceProvider();
var romParser = provider.GetService<RomParser>();

romParser!.Parse(romPath);