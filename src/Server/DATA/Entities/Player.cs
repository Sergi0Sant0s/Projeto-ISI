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
        [MaxLength(30,ErrorMessage = "Nome do jogador deve ter no maximo 30 caracteres")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "Nickname do jogador deve ter no maximo 30 caracteres")]
        public string Nickname { get; set; }
        [MinLength(0, ErrorMessage = "Idade do jogador deve ser maior que 0")]
        public int Age { get; set; }
        [MaxLength(30, ErrorMessage = "Nacionalidade do jogador deve ter no maximo 30 caracteres")]
        public string Nationality { get; set; }
        [MaxLength(100, ErrorMessage = "Facebook do jogador deve ter no maximo 100 caracteres")]
        public string Facebook { get; set; }
        [MaxLength(100, ErrorMessage = "Twitter do jogador deve ter no maximo 100 caracteres")]
        public string Twitter { get; set; }
        [MaxLength(100, ErrorMessage = "Instagram do jogador deve ter no maximo 100 caracteres")]
        public string Instagram { get; set; }


        /* RELATIONSHIP */
        [JsonIgnore]
        public virtual ICollection<StatPlayerOnMap> StatPlayerOnMap { get; set; }
    }
}
