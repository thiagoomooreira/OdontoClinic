using OdontoClinic.Context;
using OdontoClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdontoClinic.Controllers
{
    public class ConsultaController : Controller
    {
        ContextModel _db = new ContextModel();
        // GET: Consulta
        public ActionResult Index()
        {
            var list = from c in _db.Consultas
                   orderby c.Data descending
                   select c;
            return View(list.ToList());
        }

        [HttpGet]
        public ActionResult Agendar() {

            return View();
        }
        [HttpPost]
        public ActionResult Agendar([Bind(Include = "nome,email,celular,data,hora")]Consulta consulta,string data,string hora) {
            consulta.Data = Convert.ToDateTime(data + " " + hora);
            _db.Consultas.Add(consulta);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditarConsulta(Consulta consulta) {
           
            return View(consulta);
        }

        [HttpPost]
        public ActionResult Editar([Bind(Include = "id,nome,celular,email,data,hora")]Consulta consulta, string data, string hora) {
            consulta.Data = Convert.ToDateTime(data + " " + hora);
            var consultaBanco = _db.Consultas.Where(l => l.Id == consulta.Id).FirstOrDefault();
            if(consultaBanco != null) {
                consultaBanco.Nome = consulta.Nome;
                consultaBanco.Celular = consulta.Celular;
                consultaBanco.Email = consulta.Email;
                consultaBanco.Data = consulta.Data;
                //consultaBanco.Hora = consulta.Hora;
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PesquisarPaciente([Bind(Include = "nome")]Consulta consulta) {
            if(consulta.Nome != null) {
                var resultado = _db.Consultas.Where(l => l.Nome == consulta.Nome);
                return View("Index", resultado.ToList<Consulta>());
            }
            else {
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public ActionResult Deletar(Consulta c) {
            var resultado = _db.Consultas.Where(l => l.Id == c.Id).FirstOrDefault();

            if(resultado != null) {
                _db.Consultas.Remove(resultado);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FiltrarLista(string filtro)
        {
            List<Consulta> consultas = new List<Consulta>();
            consultas = (from c in _db.Consultas
                   orderby c.Data descending
                   select c).ToList();

            if (filtro == "hoje")
            {
                consultas = (from c in _db.Consultas
                             where c.Data.Day == DateTime.Now.Day
                            && c.Data.Month == DateTime.Now.Month
                            && c.Data.Year == DateTime.Now.Year
                    select c).ToList();
            }

            return View("Index", consultas);
        }
    }
}