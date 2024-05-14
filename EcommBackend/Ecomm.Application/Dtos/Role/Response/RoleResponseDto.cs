namespace Ecomm.Application.Dtos.Role.Response
{
    public class RoleResponseDto
    {
        public Guid RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? State { get; set; }
        public string? StateRole { get; set; }
    }
}
