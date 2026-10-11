using Microsoft.AspNetCore.Mvc;

namespace AutoDis.WebApi;

public static class ControllerBaseExtension
{
    public static OkObjectResult SendOkResult<T>(this ControllerBase controllerBase, T? value)
    {
        var response = new ApiResponse<T>() { Message = ApiResponseStatuses.Success, Data = value };

        return controllerBase.Ok(response);
    }
}
