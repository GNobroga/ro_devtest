using System.Net;

namespace RO.DevTest.Domain.Exception;


public class EntityNotFoundException(string error) : ApiException(error) {
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
