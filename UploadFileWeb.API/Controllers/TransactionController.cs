using Microsoft.AspNetCore.Mvc;
using UploadFileWeb.API.Controllers.Base;
using UploadFileWeb.Shared.Interfaces;

namespace UploadFileWeb.API.Controllers
{
    public class TransactionController : BaseController<ITransactionService>
    {
        public TransactionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

     
    }
}
