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

        public IActionResult Index(){
            List<Hab> habs= new List<Hab>();
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(" SELECT h.numhab,t.nomtiphab FROM  tipohabitacion t, habitacion ha ,reservahab h, reservahabitacion r  where ha.numhab=h.numhab and  ha.tiphabcod=t.codtiphab and h.codreserva=r.codreserva and current_date=r.checkin",conn);
            
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

        public IActionResult Check(int num){
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select nomcli,  concat(apellpater,' ',apemater), correocli, motivohosp, telefonocli, numerodoccli FROM cliente c , reservahabitacion r, reservahab rh where c.codcliente=r.clientecod and r.codreserva=rh.codreserva and rh.numhab='"+num+"'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            
            if(dr.Read()){
                ViewBag.nomcli = dr.GetValue(0).ToString();
                ViewBag.apellpater = dr.GetValue(1).ToString();
               // ViewBag.telefonocli = dr.GetValue(2).ToString();
                ViewBag.correocli = dr.GetValue(2).ToString();
                ViewBag.moti=dr.GetValue(3).ToString();
                ViewBag.tele=dr.GetValue(4).ToString();
                ViewBag.doc=dr.GetValue(5).ToString();
            }
            dr.Close();
            List<Pago> p = new List<Pago>();
            List<Room> rooms = new List<Room>();
        
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("select to_char(fechareserva, 'DD/MM/YYYY'),tippago, monto from reservahabitacion r, reservahab rh where r.codreserva=rh.codreserva and rh.numhab='{0}' and rh.numhab=h.numhab and h.estadohab='Reservado'",num), conn);
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
        
            ViewBag.Pago= p;
            return View();
        }

          [HttpPost]

        public IActionResult Registrar (Acompanante a){
            NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(String.Format("insert into acompañante values ((select coalesce(max(codacompañante)+1,1) from acompañante),'{0}', '{1}', '{2}', '{3}','{4}')",a.tipdoccodacomp, a.apellacomp, a.nomacomp,a.clinumdoacomp,a.numdocacomp), conn);
            var row = cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}