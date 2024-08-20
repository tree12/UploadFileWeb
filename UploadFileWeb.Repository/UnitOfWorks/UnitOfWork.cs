using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Entities;
using UploadFileWeb.Entities.Interfaces;
using UploadFileWeb.Repository.Repositories;

namespace UploadFileWeb.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context { get; private set; }

        public IFileLogRepository fileLogRepository { get; private set; }

        public ITransactionRepository transactionRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            transactionRepository = new TransactionRepository(context);
            fileLogRepository = new FileLogRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
