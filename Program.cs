using MuholdApp;

List<Muhold> muholdak = [];

int hibasSorokSzama = 0;

try
{
    foreach (string sor in File.ReadAllLines("muholdak.txt").Skip(1))
    {
        string[] adatok = sor.Split('|');

        if (adatok.Length != 4)
        {
            hibasSorokSzama++;
            continue;
        }

        string tipus = adatok[0];

        if (!(tipus == "M" || tipus == "K"))
        {
            hibasSorokSzama++; 
            continue;
        }

        string azonosito = adatok[1];

        if (string.IsNullOrWhiteSpace(azonosito))
        {
            hibasSorokSzama++;
            continue;
        }

        if (!int.TryParse(adatok[2], out int sulyKg))
        {
            hibasSorokSzama++;
            continue;
        }

        if (!int.TryParse(adatok[3], out int extra)) {
            hibasSorokSzama++;
            continue;
        }

        if (tipus == "M")
        {
            muholdak.Add(new Muhold(azonosito, sulyKg));
        }
        else
        {
            muholdak.Add(new KutatasiMuhold(azonosito, sulyKg, extra));
        }
    }
}
catch (Exception exc)
{
    Console.WriteLine(exc);
}

Console.WriteLine($"Sikeresen beolvasott műholdak: {muholdak.Count} db");
Console.WriteLine($"Kiszűrt hibás sorok száma: {hibasSorokSzama} db");

Console.WriteLine("\nBeolvasott műholdak listája:");
muholdak.ForEach(Console.WriteLine);

Console.WriteLine("\nLINQ Statisztikák:");
Console.WriteLine($"Műholdak átlagos tömege: {muholdak.Average(m => m.SulyKg)} kg");

var legnehezebb = muholdak.MaxBy(m => m.SulyKg);
if (legnehezebb != null)
{
    Console.WriteLine($"Legnehezebb műhold: {legnehezebb.Azonosito} ({legnehezebb.SulyKg} kg)");
}

var legolcsobb = muholdak.MinBy(m => m.NapiKoltseg());
if (legolcsobb != null)
{
    Console.WriteLine($"Legalacsonyabb napi költségű műhold: {legolcsobb.Azonosito} ({legolcsobb.NapiKoltseg()} EUR)");
}

Console.WriteLine($"Legalább 1000 kg-os műholdak száma: {muholdak.Count(m => m.SulyKg > 1000)} db");

Console.WriteLine("\nKutatási műholdak külön elemzése:");
muholdak.ForEach(m =>
{
    if (m is KutatasiMuhold k)
    {
        Console.WriteLine($"- {k.Azonosito} kutatási műhold műszereinek száma: {k.MuszerekSzama} db.");
    }
});

string fajlNev = "riport.txt";

try
{
    var rendezettMuholdak = muholdak
        .Where(m => m.NapiKoltseg() > 2000)
        .OrderByDescending(m => m.NapiKoltseg())
        .Select(m => $"{m.Azonosito};{m.SulyKg};{m.NapiKoltseg()}");

    File.WriteAllLines(fajlNev, rendezettMuholdak);
}
catch (Exception exc)
{
    Console.WriteLine(exc);
}

Console.WriteLine($"\nA {fajlNev} fájl sikeresen elkészült a kimeneti mappában.");
Console.WriteLine($"Műhold példányok száma (static): {Muhold.OsszesMuholdSzama} db");