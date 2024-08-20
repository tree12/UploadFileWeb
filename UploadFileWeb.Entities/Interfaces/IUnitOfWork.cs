using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Entities.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext context { get; }
        int Complete();
    }
}
