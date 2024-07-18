// See https://aka.ms/new-console-template for more information

using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Services.Compress;
using MetroidFusionExtractor.Model.Services.Draw;
using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory;
using MetroidFusionExtractor.Model.Services.Memory.Factory;
using Microsoft.Extensions.DependencyInjection;

void ConfigureServices(IServiceCollection services)
{
    //Misc
    services.AddSingleton<RomService>();
    services.AddSingleton<MemoryService>();

    //Compression
    services.AddSingleton<RleService>();

    //Draw
    services.AddSingleton<FullDrawer>();
    services.AddSingleton<RoomDrawer>();

    //Misc Factories
    services.AddSingleton<FileDataFactory>();

    //Game Factories
    services.AddSingleton<BgFactory>();
    services.AddSingleton<ClipDataFactory>();
    services.AddSingleton<GameFactory>();
    services.AddSingleton<RoomFactory>();
    services.AddSingleton<TilesetFactory>();
    services.AddSingleton<BackgroundPaletteFactory>();

    //Rom Factories
    services.AddSingleton<RomFactory>();
    services.AddSingleton<RomRoomFactory>();
}

ServiceCollection services = new();
ConfigureServices(services);
using var provider = services.BuildServiceProvider();
// var drawer = provider.GetRequiredService<FullDrawer>();

// drawer.DrawMap();

var drawer = provider.GetRequiredService<RoomDrawer>();
var gameFactory = provider.GetRequiredService<GameFactory>();

var game = gameFactory.Build();
// var room = game.GetRoom(Area.MainDeck, roomId);

foreach (Area area in Enum.GetValues(typeof(Area)))
// foreach (var area in new Area[] {Area.MainDeck})
{
    Console.WriteLine($"Drawing area : {area}...");
    var i = 0;
    foreach (var room in game.GetRooms(area))
        drawer.Draw(room, $"{area}/{i++}");
}