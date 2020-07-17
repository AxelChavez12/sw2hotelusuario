using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using web_admin.Models;

namespace web_admin.Controllers
{
    public class HabitacionController : Controller
    {
        NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            

        public IActionResult Index(){

            List<Producto> productos= new List<Producto>();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select tipoprod, nomproducto, precventa from producto",conn);
            NpgsqlDataReader nd= cmd.ExecuteReader();

            while(nd.Read()){
                Producto p = new Producto();
                p.tipo=nd.GetValue(0).ToString();
                p.nom= nd.GetValue(1).ToString();
                p.precio=nd.GetDouble(2);

                productos.Add(p);
            }

            conn.Close();

            ViewBag.productos=productos;
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Producto p){

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(String.Format("INSERT INTO producto values((select coalesce(max(codproducto)+1,1) from producto),'{0}',{1},{2},'{3}')",p.nom,p.stock,p.precio,p.tipo),conn);
            var row= cmd.ExecuteNonQuery();
            conn.Close();

        
            return RedirectToAction("Index");
        }    
    }
}