

namespace BackEnd.Core.Dto;

public class CancelacionDto
{

    public DateTimeOffset CancelAt { get; set; }
    public string? Actor { get; set; }
    public string? Reason { get; set; }

}
