using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using web_user.Models;
using Npgsql;

namespace hotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }
    

        

        [HttpPost]
        public IActionResult Reserva(Reserva f){

            List<Habitacion> habs= new List<Habitacion>();
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(String.Format( "select h.numhab, h.ubicacionhab, h.tiphabcod from reservahabitacion r, reservahab hab, habitacion h where hab.numhab=h.numhab and r.codreserva=hab.codreserva and r.checkin not between '{0}' and '{1}'  and r.checkout not between '{0}' and '{1}' union select h.numhab, h.ubicacionhab, h.tiphabcod  from reservahab hab, reservahabitacion r, habitacion h where h.numhab!=hab.numhab",f.checkin,f.checkout), conn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            Habitacion hab= new Habitacion();
                            hab.numhab=dr.GetInt32(0);
                            hab.ubi=dr.GetInt32(1);
                            hab.tipo=dr.GetInt32(2);
                            habs.Add(hab);

                        }
                    dr.Close();
                conn.Close();
            List<Habitacion>hab1= new List<Habitacion>();
            List<Habitacion>hab2= new List<Habitacion>();
            List<Habitacion>hab3= new List<Habitacion>();
            List<Habitacion>hab4= new List<Habitacion>();
    
            foreach( var item in habs){

                if(item.tipo==1){
                    hab1.Add(item);
                }
                if(item.tipo==2){
                    hab2.Add(item);
                }
                if(item.tipo==3){
                    hab3.Add(item);
                }
                if(item.tipo==4){
                    hab4.Add(item);
                }
            }
            
                
            ViewBag.Tipo1=hab1;
            ViewBag.Tipo2=hab2;
            ViewBag.Tipo3=hab3;
            ViewBag.Tipo4=hab4;
            ViewBag.checkin=f.checkin;
            ViewBag.checkout=f.checkout;

            return  View();
        }


       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
