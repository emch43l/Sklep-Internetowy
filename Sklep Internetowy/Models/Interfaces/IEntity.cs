namespace Sklep_Internetowy.Models.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }

        public Guid Guid { get; }
    }
}
