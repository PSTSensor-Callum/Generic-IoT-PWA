namespace Generic_IoT_PWA.Models.Abstracts.Dtos
{
    public abstract class Dto
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Dto() { }

        public Dto(Guid id, bool active = true)
        {
            Id = id;
            Active = active;
        }
    }
}
