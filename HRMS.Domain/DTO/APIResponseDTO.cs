namespace HRMS.Domain.DTO;

public record APIResponseDTO(bool IsSuccess, string? Message = null, object? Response = null);
