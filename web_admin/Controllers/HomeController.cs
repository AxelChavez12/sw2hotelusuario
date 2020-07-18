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
    public IActionResult Index(Cliente num){
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT nomcli,  concat(apellpater,' ',apemater), correocli, motivohosp, telefonocli FROM cliente where numdoccli='"+num.numdocli+"'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            
            if(dr.Read()){
                ViewBag.nomcli = dr.GetValue(0).ToString();
                ViewBag.apellpater = dr.GetValue(1).ToString();
               // ViewBag.telefonocli = dr.GetValue(2).ToString();
                ViewBag.correocli = dr.GetValue(2).ToString();
                ViewBag.moti=dr.GetValue(3).ToString();
                ViewBag.tele=dr.GetValue(4).ToString();
                ViewBag.doc=num.numdocli;
            }
            dr.Close();
            List<Pago> p = new List<Pago>();
            List<Room> rooms = new List<Room>();
         
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("select to_char(fechareserva, 'DD/MM/YYYY'),tippago, monto from reservahabitacion where clidocres='{0}'",num.numdocli), conn);
            NpgsqlDataReader dataReader = cmd2.ExecuteReader();
                while(dataReader.Read())
            {  Random r =new Random();
                Pago pago= new Pago();
                pago.fecope=dataReader.GetValue(0).ToString();
                pago.numope= r.Next(1111,9999);
                pago.refe=dataReader.GetValue(1).ToString();
                pago.monto=dataReader.GetDouble(2);
                p.Add(pago);
            }
            dataReader.Close();
            NpgsqlCommand cmd3 = new NpgsqlCommand(String.Format("select d.iddetventa,p.nomproducto,d.cantidad,d.subtotal from cliente c, ventas v , detalleventa d, producto p where  c.numdoccli=v.clinumven and v.codventa=d.ventascod and d.productocod=p.codproducto and c.numdoccli='{0}'", num.numdocli), conn);
            NpgsqlDataReader datar = cmd3.ExecuteReader();
            while(datar.Read()){
                
                Room ro= new Room();
                ro.id=dr.GetValue(0).ToString();
                ro.prodnom=dr.GetValue(1).ToString();
                ro.cantidad=dr.GetInt32(2);
                ro.total=dr.GetDouble(3);
                rooms.Add(ro);
            }
            datar.Close();
            conn.Close();
            ViewBag.Pago= p;
            ViewBag.Rooms= rooms;
         
            return View();
    }  
        
  

    
        public IActionResult Habitaciones(){
            List<Hab> habs= new List<Hab>();
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(" SELECT h.numhab, h.estadohab,t.nomtiphab FROM  tipohabitacion t, habitacion h  where  h.tiphabcod=t.codtiphab union select numhab,estadohab, th.nomtiphab from habitacion h, tipohabitacion th where estadohab='Disponible' and th.codtiphab=h.tiphabcod  order by numhab",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Hab hab= new Hab();
                hab.numhab=dr.GetInt32(0);
                hab.estado=dr.GetValue(1).ToString();
                hab.tipo=dr.GetValue(2).ToString();
                habs.Add(hab);
            }
            
            ViewBag.Hab=habs;
            conn.Close();
            return View();
        }       

        public IActionResult Ventas()
        {
            return View();
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
