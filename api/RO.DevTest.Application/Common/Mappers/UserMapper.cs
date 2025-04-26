using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Common.Mappers;

public static class UserMapper {
    public static UserDTO ToDTO(Domain.Entities.User src) {
        return new UserDTO(src.Id, src.UserName!, src.Name, src.Email!);
    }
}