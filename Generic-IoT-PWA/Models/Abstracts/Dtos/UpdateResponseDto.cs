namespace Generic_IoT_PWA.Models.Abstracts.Dtos
{
    // message will be mainly used for complex errors or pointing to where that action can be done
    public record PropertyResult(bool Success, string? Message = null);
    public class UpdateResponseDto<T> where T : Dto
    {
        public T Data { get; set; }
        public Dictionary<string, PropertyResult> PropertyResults { get; set; }

        public UpdateResponseDto() { }

        public UpdateResponseDto(T data, Dictionary<string, PropertyResult> propertyResults)
        {
            Data = data;
            PropertyResults = propertyResults;
        }
    }
}
