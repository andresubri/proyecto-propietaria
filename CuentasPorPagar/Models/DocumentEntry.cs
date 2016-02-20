using Parse;

namespace CuentasPorPagar.Models
{
    [ParseClassName("DocumentEntry")]
    class DocumentEntry : ParseObject
    {
        [ParseFieldName("objectId")]
        public string DocumentNumber
        {
            get { return GetProperty<string>(); }
        }
        [ParseFieldName("receiptNum")]
        public int ReceiptNumber
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("documentDate")]
        public string DocumentDate
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("amount")]
        public int Amount
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("registerDate")]
        public string RegisterDate
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("supplier")]
        public string Supplier
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("state")]
        public string state
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
    }
}
