using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DATA.Entities
{
    public class Map
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MapId { get; set; }
        [MaxLength(20, ErrorMessage = "Descrição deve ter no maximo 20 caracteres")]
        public string Description { get; set; }

        /* RELATIONSHIP */

        [JsonIgnore]
        public virtual ICollection<MapOfGame> MapofGame { get; set; }
    }
}
