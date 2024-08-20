using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Shared.Constants
{
    public static class TransactionXMLStatus
    {
        public const string Approved = "A";
        public const string Rejected = "R";
        public const string Done = "D";
    }

    public static class TransactionCSVStatus
    {
        public const string Approved = "A";
        public const string Failed = "R";
        public const string Finished = "D";
    }
}
