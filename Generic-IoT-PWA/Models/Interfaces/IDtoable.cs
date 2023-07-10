using Generic_IoT_PWA.Models.Abstracts.Dtos;

namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface IDtoable<D>
    where D : Dto
    {
        public D ToDto();
    }
}
