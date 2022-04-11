namespace Codelux.Common.Models
{
    public interface IRole
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
    }
}
