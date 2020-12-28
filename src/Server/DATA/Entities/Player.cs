using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Nome do jogador deve ter no maximo 30 caracteres")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Nickname do jogador deve ter no maximo 30 caracteres")]
        public string Nickname { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Idade deve ser maior ou igual a 1")]
        public int Age { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Nacionalidade do jogador deve ter no maximo 30 caracteres")]
        public string Nationality { get; set; }

        /*REDES SOCIAIS*/
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }


        /* RELATIONSHIP */
        [JsonIgnore]
        public virtual ICollection<StatPlayerOnMap> StatPlayerOnMap { get; set; }
    }
}
