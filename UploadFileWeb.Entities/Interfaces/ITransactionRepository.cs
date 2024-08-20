using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Entities.Data.Entities;
using UploadFileWeb.Entities.Interfaces.Base;

namespace UploadFileWeb.Entities.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction, string>
    {
    }
}
