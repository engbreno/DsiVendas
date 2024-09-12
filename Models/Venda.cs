namespace DsiVendas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; }

        // Relacionamento com Cliente e ItensVenda
        public Cliente Cliente { get; set; }
        public ICollection<ItemVenda> ItensVenda { get; set; }

        // Propriedade calculada para o total da venda
        public decimal Total => ItensVenda?.Sum(item => item.SubTotal) ?? 0;
    }
}