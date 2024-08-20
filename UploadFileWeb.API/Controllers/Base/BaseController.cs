using Microsoft.AspNetCore.Mvc;
using UploadFileWeb.Shared.Interfaces;

namespace UploadFileWeb.API.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase where T : IService
    {
        protected T service;

        public BaseController(IServiceProvider serviceProvider)
        {
            service = serviceProvider.GetService<T>();
        }
        protected string GetExceptionMessage(Exception e)
        {
            string message = "";
            if (e.InnerException != null)
            {
                message = GetExceptionMessage(e.InnerException) + ", ";
            }
            return string.IsNullOrEmpty(message) ? e.Message : message;
        }
    }
}
