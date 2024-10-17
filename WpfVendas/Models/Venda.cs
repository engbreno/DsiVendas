namespace DsiVendas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; }
        public decimal Total => ItensVenda?.Sum(item => item.SubTotal) ?? 0;

        public ICollection<ItemVenda> ItensVenda { get; set; }
    }
}