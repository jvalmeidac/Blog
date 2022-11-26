using Blog.SharedKernel.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.SharedKernel.Helpers
{
    public static class ControllerHelper
    {
        public static ActionResult ParseToActionResult<T>(
            this ControllerBase controller, 
            ValidatedResult<T> validation) where T : class
        {
            if (validation.BadRequest)
            {
                return controller.BadRequest(new { Errors = validation.ErrorMessages });
            }

            if(validation.NotFound)
            {
                return controller.NotFound(new { Errors = validation.ErrorMessages });
            }

            return controller.Ok(new { validation.Data });
        }
    }
}
