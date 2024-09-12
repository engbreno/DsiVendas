namespace DsiVendas.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }

        // Preço e SubTotal são calculados com base no produto
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal => Quantidade * PrecoUnitario;

        // Relacionamento com Produto e Venda
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }
    }
}