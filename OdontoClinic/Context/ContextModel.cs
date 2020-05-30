using OdontoClinic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdontoClinic.Context {
    public class ContextModel:DbContext{
        public ContextModel() : base("bancoOdonto") {

        }
        public virtual DbSet<Consulta> Consultas {
            get;set;
        }
    }
}