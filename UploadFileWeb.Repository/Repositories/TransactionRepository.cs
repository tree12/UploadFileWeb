using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UploadFileWeb.Entities;
using UploadFileWeb.Entities.Interfaces;
using UploadFileWeb.Repository.Repositories.Base;

namespace UploadFileWeb.Repository.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction, string>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
