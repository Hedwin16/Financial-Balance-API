namespace DB.Models;

public partial class TransactionsType
{
    public int Id { get; set; }

    public int IdType { get; set; }

    public int IdTransaction { get; set; }

    public virtual TypeOfTransaction IdTypeNavigation { get; set; } = null!;
}
