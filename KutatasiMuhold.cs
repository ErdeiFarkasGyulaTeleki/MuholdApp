namespace MuholdApp
{
    public class KutatasiMuhold : Muhold
    {
        public int MuszerekSzama { get; set; }

        public KutatasiMuhold(string _azonosito, int _sulyKg, int _muszerekSzama) : base(_azonosito, _sulyKg)
        {
            MuszerekSzama = _muszerekSzama;
        }

        public override int NapiKoltseg()
        {
            return base.NapiKoltseg() + MuszerekSzama * 200;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Műszerek: {MuszerekSzama} db";
        }
    }
}
