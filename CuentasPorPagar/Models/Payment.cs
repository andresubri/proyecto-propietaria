namespace CuentasPorPagar.Models
{
    class Payment : Parse.ParseObject
    {
        public Payment(string id, string concept, int monto, string supplier, bool state)
        {
            Id = id;
            Concept= concept;
            Monto = monto;
            Supplier = supplier;
            State = state;
        }

        public string Id { get; set; }
        public string Concept { get; set; }
        public int Monto { get; set; }
        public string Supplier { get; set; }
        public bool State { get; set; }
    }
}
