namespace Generic_IoT_PWA.Models.Abstracts.Entities
{
    public class EditableEntity : Entity
    {
        public bool Active { get; set; }

        public EditableEntity() { }

        public EditableEntity(bool? active = true)
        {
            Active = active ?? true;
        }
    }
}
