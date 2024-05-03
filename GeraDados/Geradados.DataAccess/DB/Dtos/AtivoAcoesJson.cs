namespace Geradados.DataAccess.DB.Dtos
{
    public class AtivoAcoesJson
    {
        public AtivoAcoesJson()
        {
            Ticker = string.Empty;
            Nome = string.Empty;
            Ultimo = string.Empty;
            Decimal = string.Empty;
        }
        public string Ticker { get; set; }
        public string Nome { get; set; }
        public string Ultimo { get; set; }
        public string Decimal { get; set; }
    }
}
