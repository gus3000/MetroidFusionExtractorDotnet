// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model;

var rom = new ROM("rom/Metroid Fusion (Europe) (En,Fr,De,Es,It).gba");

rom.Debug();
// Console.WriteLine($"size of struct: 0x{Marshal.SizeOf(new RoomEntry()):X}");
var mapViz = new MapViz(rom);