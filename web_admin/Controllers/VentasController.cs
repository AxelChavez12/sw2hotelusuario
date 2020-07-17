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
    public class VentasController : Controller
    {
        public List<Producto> prods= new List<Producto>();
        public IActionResult Index(){

            List<Hab> habs= new List<Hab>();
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(" SELECT h.numhab,t.nomtiphab FROM  tipohabitacion t, habitacion h  where  h.tiphabcod=t.codtiphab and h.estadohab='Ocupado' order by numhab",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Hab hab= new Hab();
                hab.numhab=dr.GetInt32(0);
                hab.tipo=dr.GetValue(1).ToString();
                habs.Add(hab);
            }
            
            List<Hab> habs1= new List<Hab>();
            List<Hab> habs2= new List<Hab>();
            List<Hab> habs3= new List<Hab>();
            List<Hab> habs4= new List<Hab>();

            foreach(var item in habs){
                if(item.numhab<300){
                    habs1.Add(item);
                }else if(item.numhab<400 && item.numhab>=300){
                    habs2.Add(item);
                }else if(item.numhab<500 && item.numhab>=400){
                    habs3.Add(item);
                }else{
                    habs4.Add(item);
                }
            }
            ViewBag.Hab2=habs1;
            ViewBag.Hab3=habs2;
            ViewBag.Hab4=habs3;
            ViewBag.Hab5=habs4;

            return View();
        }


        public IActionResult Registrar(int num){

            
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select codproducto, tipoprod, nomproducto, precventa,stock from producto",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Producto p= new Producto();
                p.id=dr.GetInt32(0);
                p.tipo=dr.GetValue(1).ToString();
                p.nom=dr.GetValue(2).ToString();
                p.precio=dr.GetDouble(3);
                p.stock=1;
                prods.Add(p);
            }
            
            ViewBag.Prod=prods;

            return View();
        }

        public IActionResult RegistrarProducto(Pedido p){

           

            
            return RedirectToAction("Registrar");
        }

        public IActionResult Buscar(){
            List<Producto> prods= new List<Producto>();
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select codproducto, tipoprod, nomproducto, precventa,stock from producto",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Producto p= new Producto();
                p.id=dr.GetInt32(0);
                p.tipo=dr.GetValue(1).ToString();
                p.nom=dr.GetValue(2).ToString();
                p.precio=dr.GetDouble(3);
                p.stock=1;
                prods.Add(p);
            }
            
            ViewBag.Prod=prods;

            return View();
        }
    }
}