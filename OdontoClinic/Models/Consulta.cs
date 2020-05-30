using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OdontoClinic.Models {

    [Table("Consulta")]
    public class Consulta {
        [Column("Id"),Key]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Celular")]
        public string Celular { get; set; }

        [Column("Email")]
        public string Email{ get; set; }

        [Column("Data")]
        public DateTime Data { get; set; }
    }
}