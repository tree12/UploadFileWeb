using System.Xml.Serialization;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Models
{
    [XmlRoot(ElementName = "Transactions")]
    public class Transactions
    {
        [XmlElement(ElementName = "Transaction")]
        public List<Transaction> transactions { get; set; }
    }

    public class Transaction {
        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlElement(ElementName = "TransactionDate")]
        public string TransactionDate { get; set; }
        [XmlElement(ElementName = "PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
    }
    public class PaymentDetails {
        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
