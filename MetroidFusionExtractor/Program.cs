// See https://aka.ms/new-console-template for more information

using MetroidFusionExtractor.Model.Services;
using MetroidFusionExtractor.Model.Services.Draw;
using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory;
using MetroidFusionExtractor.Model.Services.Memory.Factory;
using Microsoft.Extensions.DependencyInjection;

void ConfigureServices(IServiceCollection services)
{
    //Misc
    services.AddSingleton<RomService>();

    //Draw
    services.AddSingleton<MapDrawer>();
    
    //Misc Factories
    services.AddSingleton<FileDataFactory>();
    
    //Game Factories
    services.AddSingleton<GameFactory>();
    services.AddSingleton<RoomFactory>();

    //Rom Factories
    services.AddSingleton<RomFactory>();
    services.AddSingleton<RomRoomFactory>();
}

ServiceCollection services = new();
ConfigureServices(services);
using var provider = services.BuildServiceProvider();
var drawer = provider.GetRequiredService<MapDrawer>();

drawer.DrawMap();