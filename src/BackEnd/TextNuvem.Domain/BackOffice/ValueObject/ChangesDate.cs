namespace TextNuvem.Domain.BackOffice.ValueObject;

public sealed class ChangesDate
{
    public ChangesDate()
    {
        Quantity = 1;
        Date = DateTime.UtcNow;
    }
    public int Quantity { get; set; }
    public  DateTime Date { get; set; }

    public void AddQuantity(int quantityToAdd)
        => Quantity += quantityToAdd; 
    
    public static class Factory
    {
        public static ChangesDate Create()
            => new ChangesDate();
    }
}