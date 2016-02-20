using Parse;

namespace CuentasPorPagar.Models
{
    [ParseClassName("Supplier")]
    class Supplier : ParseObject
    {
        [ParseFieldName("identification")]
        public int Identification { get; set; }
        [ParseFieldName("balance")]
        public int Balance { get; set; }
        [ParseFieldName("objectId")]
        public string Id { get; }
        [ParseFieldName("name")]
        public string Name { get; set; }
        [ParseFieldName("type")]
        public string Type { get; set; }
        [ParseFieldName("state")]
        public bool State{ get; set; }
    }
}
