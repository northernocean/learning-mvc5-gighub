using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models

{
    public class Genre
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}