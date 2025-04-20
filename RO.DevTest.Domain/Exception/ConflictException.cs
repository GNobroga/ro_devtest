using System.Net;

namespace RO.DevTest.Domain.Exception;

public class ConflictException(string error) : ApiException(error) {
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
