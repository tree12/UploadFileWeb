using UploadFileWeb.Entities.Interfaces;

namespace UploadFileWeb.API.Services.Base
{
    public abstract class BaseService
    {
        protected IUnitOfWork unitOfWork;

        public BaseService(IServiceProvider services)
        {
            this.unitOfWork = services.GetService<IUnitOfWork>();
        }
    }
}
