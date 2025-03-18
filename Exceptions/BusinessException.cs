using System.Net;

namespace BackEndApi.Exceptions;

public class BusinessException(string message, HttpStatusCode statusCode, string errCode) : Exception(message)
{
    public string ErrorCode { get; } = errCode;

    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class BadRequestException(string errCode = "BAD_REQUEST", string msg = "BAD_REQUEST")
    : BusinessException(msg, HttpStatusCode.BadRequest, errCode)
{
}

public class NotFoundException(string errCode ="NOT_FOUND", string msg = "NOT_FOUND")
    : BusinessException(msg, HttpStatusCode.NotFound, errCode)
{
}

public class ForbiddenException(string errCode ="FORBIDDEN", string msg = "FORBIDDEN")
    : BusinessException(msg, HttpStatusCode.Forbidden, errCode)
{
}