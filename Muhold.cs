namespace MuholdApp
{
    public class Muhold
    {
        public string Azonosito { get; set; }

        protected int sulyKg;
        public int SulyKg
        {
            get => sulyKg;
            set
            {
                sulyKg = value > 0 ? value : 100;
            }
        }

        private static int osszesMuholdSzama;
        public static int OsszesMuholdSzama
        {
            get => osszesMuholdSzama;
        }

        public Muhold(string _azonosito, int _sulyKg)
        {
            Azonosito = _azonosito;
            SulyKg = _sulyKg;

            osszesMuholdSzama++;
        }

        public virtual int NapiKoltseg()
        {
            return SulyKg * 5;
        }

        public override string ToString()
        {
            return $"Műhold {Azonosito} | Tömeg: {SulyKg} kg | Napi költség: {NapiKoltseg()} EUR";
        }
    }
}
