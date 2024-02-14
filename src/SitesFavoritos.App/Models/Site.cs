using SQLite;

namespace SitesFavoritos.App.Models
{
    [Table("sites")]
    public class Site
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string Endereco { get; set; }
    }
}
