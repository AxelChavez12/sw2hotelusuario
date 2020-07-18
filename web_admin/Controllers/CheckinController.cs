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
    public class CheckinController: Controller
    {

        NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
        
        public IActionResult Index(){
            List<Hab> habs= new List<Hab>();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(" SELECT h.numhab,t.nomtiphab, h.estado, c.numerodoccli FROM  tipohabitacion t, habitacion ha ,reservahab h, reservahabitacion r , cliente c where ha.numhab=h.numhab and  ha.tiphabcod=t.codtiphab and h.codreserva=r.codreserva and r.clientecod=c.codcliente and current_date=r.checkin",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Hab hab= new Hab();
                hab.numhab=dr.GetInt32(0);
                hab.tipo=dr.GetValue(1).ToString();
                hab.estado=dr.GetValue(2).ToString();
                hab.codcli=dr.GetValue(3).ToString();
                habs.Add(hab);
            }
            List<Hab> habb=new List<Hab>();
            foreach( var item in habs){
                if(item.estado.Equals("Reservado")){
                    habb.Add(item);
                }
            }
            ViewBag.Habb=habb;
            List<Hab> habs1= new List<Hab>();
            List<Hab> habs2= new List<Hab>();
            List<Hab> habs3= new List<Hab>();
            List<Hab> habs4= new List<Hab>();

            conn.Close();
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

        public IActionResult Check(int num){
            List<Pago> p =new List<Pago>();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(String.Format("select nomcli,  concat(apellpater,' ',apemater), correocli, motivohosp, telefonocli, numerodoccli, to_char(fechareserva, 'DD/MM/YYYY'),tippago,rh.numhab ,th.precio from reservahabitacion r, reservahab rh, cliente c, habitacion h, tipohabitacion th where c.codcliente=r.clientecod and r.codreserva=rh.codreserva and rh.numhab=h.numhab  and h.numhab={0}  and r.checkin=current_date and th.codtiphab=h.tiphabcod",num), conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            
            if(dr.Read()){
                ViewBag.nomcli = dr.GetValue(0).ToString();
                ViewBag.apellpater = dr.GetValue(1).ToString();
               // ViewBag.telefonocli = dr.GetValue(2).ToString();
                ViewBag.correocli = dr.GetValue(2).ToString();
                ViewBag.moti=dr.GetValue(3).ToString();
                ViewBag.tele=dr.GetValue(4).ToString();
                ViewBag.doc=dr.GetValue(5).ToString();
                ViewBag.hab=num;
                Pago pago= new Pago();
                pago.fecope=dr.GetValue(6).ToString();
                Random r= new Random();
                pago.numope=r.Next(1001,9999);
                pago.refe=dr.GetValue(7).ToString();
                pago.hab=dr.GetInt32(8);
                pago.monto+=dr.GetDouble(9);
                p.Add(pago);
            }
            dr.Close();
            conn.Close();
            ViewBag.Pago= p;
            return View();
        }

        [HttpPost]

        public IActionResult Registrar (Acompanante a){
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(String.Format("insert into acompañante values ((select coalesce(max(codacompañante)+1,1) from acompañante),'{0}', '{1}', '{2}', (select codcliente from cliente where numerodoccli='{3}'),'{4}')",a.tipdoccodacomp, a.apellacomp, a.nomacomp,a.clinumdoacomp,a.numdocacomp), conn);
            var row = cmd.ExecuteNonQuery();
             NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("update reservahab set estado='Ocupado' where numhab='{0}' and codreserva=(select r.codreserva from reservahabitacion r, reservahab rh where r.codreserva=rh.codreserva and rh.numhab='{0}'and current_date =r.checkin ) ",a.numhab), conn);
            var row2 = cmd2.ExecuteNonQuery();
            int num=a.numhab;


            conn.Close();
            return RedirectToAction("Index");
        }
        public IActionResult Reservar(int numhab){
            conn.Open();
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("update reservahab set estado='Ocupado' where numhab='{0}' and codreserva=(select r.codreserva from reservahabitacion r, reservahab rh where r.codreserva=rh.codreserva and rh.numhab='{0}'and current_date =r.checkin ) ",numhab), conn);
            var row2 = cmd2.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}