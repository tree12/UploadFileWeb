using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Shared.Constants
{
    public static class TransactionXMLStatus
    {
        public const string Approved = "A";
        public const string Rejected = "R";
        public const string Done = "D";
  
        public static string GetMemberName(string name)
        {
           var property = typeof(TransactionXMLStatus).GetField(name,BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return property?.GetValue(null)?.ToString();
            
        }
    }

    public static class TransactionCSVStatus
    {
        public const string Approved = "A";
        public const string Failed = "R";
        public const string Finished = "D";

        public static string GetMemberName(string name)
        {
            var property = typeof(TransactionCSVStatus).GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return property?.GetValue(null)?.ToString();

        }
    }


}
