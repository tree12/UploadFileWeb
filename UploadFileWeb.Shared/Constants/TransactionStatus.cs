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
  
        public static string GetMemberValue(string name)
        {
           var property = typeof(TransactionXMLStatus).GetField(name,BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return property?.GetValue(null)?.ToString();
            
        }
        public static string GetMemberName(string value)
        {
            if (value == "A")
                return nameof(TransactionXMLStatus.Approved);
            if (value == "R")
                return nameof(TransactionXMLStatus.Rejected);
            if (value == "D")
                return nameof(TransactionXMLStatus.Done);
            else
                return string.Empty;
            //TODO:: This code not work
            //var properties = typeof(TransactionXMLStatus).GetProperties();
            //string name = string.Empty;
            //foreach (var item in properties)
            //{
            //    if (value == item?.GetValue(null)?.ToString())
            //    {
            //        name = item.Name;
            //        break;
            //    }
            //}
            //return name;

        }
    }

    public static class TransactionCSVStatus
    {
        public const string Approved = "A";
        public const string Failed = "R";
        public const string Finished = "D";

        public static string GetMemberValue(string name)
        {
            var property = typeof(TransactionCSVStatus).GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return property?.GetValue(null)?.ToString();

        }
        public static string GetMemberName(string value)
        {
            if (value == "A")
                return nameof(TransactionCSVStatus.Approved);
            if (value == "R")
                return nameof(TransactionCSVStatus.Failed);
            if (value == "D")
                return nameof(TransactionCSVStatus.Finished);
            else
                return string.Empty;
            //TODO:: This code not work

            //var properties = typeof(TransactionCSVStatus).GetProperties(BindingFlags.Public | BindingFlags.Static |  BindingFlags.Instance |BindingFlags.FlattenHierarchy);
            //string name = string.Empty;
            //foreach (var item in properties)
            //{
            //    if (value == item?.GetValue(null)?.ToString()) 
            //    {
            //        name = item.Name;
            //        break;
            //    }

            //}
            //return name;

        }
    }


}
