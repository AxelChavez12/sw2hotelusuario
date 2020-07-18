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
                NpgsqlCommand cmd = new NpgsqlCommand(String.Format( " select h.numhab, h.ubicacionhab, h.tiphabcod from reservahabitacion r, reservahab hab, habitacion h where hab.numhab=h.numhab and r.codreserva=hab.codreserva and r.checkin not between '{0}' and '{1}'  and r.checkout not between '{0}' and '{1}' union( (select  h.numhab, h.ubicacionhab, h.tiphabcod  from  habitacion h  ) except (select  h.numhab, h.ubicacionhab, h.tiphabcod  from reservahab hab, habitacion h where h.numhab=hab.numhab) ) order by numhab",f.checkin,f.checkout), conn);
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


         [HttpPost]
        public IActionResult Registrar(Reserva r){

            string s = r.ape;
            string  motiv,apepat, apemat,hab1,hab2;
            if(r.motivo!=null){
                motiv=r.motivo;
            }else{
                motiv=null;
            }
            int location = s.IndexOf(" ");
            if(location>0){
                apepat=s.Substring(0,location);
                apemat=s.Substring(location+1);
            }else{
                apepat=r.ape;
                apemat=null;
            }
            string habs=r.habitaciones;
            int l= habs.IndexOf(",");
            if(l>0){
                hab1=habs.Substring(0,l);
                hab2=habs.Substring(l+1);
            }else{
                hab1=r.habitaciones;
                hab2=null;
            }

            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(String.Format("Insert into cliente values((select coalesce(max(codcliente)+1,1) from cliente),'{0}','{1}','{2}','{3}','{4}','{5}',null,null,'{6}','{7}','{8}')",r.tipodoc,apepat,r.nom,motiv,r.fecha,r.correo,apemat,r.telefono,r.numdoc),conn);
                    var row = cmd.ExecuteNonQuery();
               
                NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("insert into reservahabitacion values((select coalesce(max(codreserva)+1,1) from reservahabitacion),CURRENT_DATE,'{0}','{1}','Deposito',(select max(codcliente) from cliente),0)",r.checkin,r.checkout),conn);
                    var row2 = cmd2.ExecuteNonQuery();
                
                NpgsqlCommand cmd3 = new NpgsqlCommand(String.Format("insert into reservahab values((select max(codreserva) from reservahabitacion),{0},'Reservado')",hab1),conn);
                    var row3 = cmd3.ExecuteNonQuery();
               
                NpgsqlCommand cmd5 = new NpgsqlCommand(String.Format(" update habitacion set estadohab='Reservado' where numhab='{0}'",hab1),conn);
                    var row5 = cmd5.ExecuteNonQuery();
                conn.Close();

                if(l>0){
                    conn.Open();
                NpgsqlCommand cmd4 = new NpgsqlCommand(String.Format("insert into reservahab values((select max(codreserva) from reservahabitacion),{0},'Reservado')",hab2),conn);
                    var row4 = cmd4.ExecuteNonQuery();
               
                NpgsqlCommand cmd6 = new NpgsqlCommand(String.Format(" update habitacion set estadohab='Reservado' where numhab='{0}'",hab2),conn);
                    var row6 = cmd6.ExecuteNonQuery();
                conn.Close();
                }

                
            



            return RedirectToAction("Datos");
        }

        public IActionResult Datos(){
            List<Reserva> rs= new List<Reserva>();
            
            double monto;
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(String.Format( "select h.numhab, precio, concat( c.nomcli,' ',c.apellpater,' ',c.apemater) ,to_char(r.checkin, 'DD/MM/YYYY') ,to_char(r.checkout, 'DD/MM/YYYY'),  th.nomtiphab, extract(epoch from (checkout-checkin))/24/60/60+1,(extract(epoch from (checkout-checkin))/24/60/60+1)*precio from habitacion h, tipohabitacion th,reservahab rh, reservahabitacion r,cliente c where th.codtiphab=h.tiphabcod and h.numhab=rh.numhab and r.codreserva=rh.codreserva and r.clientecod=c.codcliente and r.codreserva=(select max(codreserva)from reservahabitacion)"), conn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            Reserva r= new Reserva();
                            r.habitaciones=dr.GetValue(0).ToString();
                            r.precio=dr.GetDouble(1);
                            r.nom=dr.GetValue(2).ToString();
                            r.checkin=dr.GetValue(3).ToString();
                            r.checkout=dr.GetValue(4).ToString();
                            r.ape=dr.GetValue(5).ToString();
                            r.cant=dr.GetDouble(6);
                            r.sub=dr.GetDouble(7);
                            ViewBag.Reserva=r;
                            rs.Add(r);

                        }
                dr.Close();
                conn.Close();
            
            if(rs.Count()>1){
                monto=rs[0].sub + rs[1].sub;
            
                ViewBag.hab1= rs[0].habitaciones;
                ViewBag.tiphab2= rs[0].ape;
                ViewBag.precio= rs[0].precio;
                ViewBag.cant=rs[0].cant;
                ViewBag.sub=rs[0].sub;
            }else{
                monto=rs[0].sub;
            }
            
            conn.Open();
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format( " update reservahabitacion set monto={0}  where codreserva=(select max(codreserva)from reservahabitacion)",monto), conn);
                var row=cmd2.ExecuteNonQuery();
            conn.Close();

            ViewBag.Monto=monto;
            
            
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
