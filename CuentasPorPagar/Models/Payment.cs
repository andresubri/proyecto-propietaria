using Parse;
namespace CuentasPorPagar.Models
{
    [ParseClassName("Payment")]
    class Payment : ParseObject
    {
        public Payment(string id, string concept, int amount, string supplier, bool state)
        {
            Id = id;
            Concept = concept;
            Amount = amount;
            Supplier = supplier;
            State = state;
        }

        [ParseFieldName("objectIt")]
        public string Id { get; set; }
        [ParseFieldName("concept")]
        public string Concept { get; set; }
        [ParseFieldName("amount")]
        public int Amount { get; set; }
        [ParseFieldName("supplier")]
        public string Supplier { get; set; }
        [ParseFieldName("state")]
        public bool State { get; set; }
    }
}
