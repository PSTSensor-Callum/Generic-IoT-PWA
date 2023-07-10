namespace Generic_IoT_PWA.Models.Abstracts.Dtos
{
    public abstract class CreateDto
    {
        public bool? Active { get; set; }

        public CreateDto(bool? active = true)
        {
            Active = active;
        }
    }
}
