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
        NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");

        public List<Producto> prods= new List<Producto>();
        public IActionResult Index(){

            List<Hab> habs= new List<Hab>();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(" SELECT h.numhab,t.nomtiphab FROM  tipohabitacion t, habitacion h , reservahab rh where  h.tiphabcod=t.codtiphab and h.numhab=rh.numhab and rh.estado='Ocupado' order by numhab",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Hab hab= new Hab();
                hab.numhab=dr.GetInt32(0);
                hab.tipo=dr.GetValue(1).ToString();
                habs.Add(hab);
            }
            dr.Close();
            conn.Close();
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


        public IActionResult Registrar(int id){

            List<Pedido> pds= new List<Pedido>();
            int codd=0;
            int cliente=0;
            double monto=0;
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select codproducto, tipoprod, nomproducto, precventa,stock from producto",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Producto p= new Producto();
                p.idprod=dr.GetInt32(0);
                p.tipo=dr.GetValue(1).ToString();
                p.nom=dr.GetValue(2).ToString();
                p.precio=dr.GetDouble(3);
                p.stock=1;
                prods.Add(p);
            }
            dr.Close();
            ViewBag.Prod=prods;
        if(id!=0){
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("select rh.numhab,concat(c.nomcli,' ',c.apellpater,' ',c.apemater),to_char(current_timestamp ,'HH12:MI'),c.codcliente  from reservahab rh,reservahabitacion r, cliente c where c.codcliente=r.clientecod and rh.codreserva=r.codreserva and rh.numhab='{0}' and current_date between r.checkin and r.checkout",id),conn);
            
                NpgsqlDataReader dr2 = cmd2.ExecuteReader();
                if(dr2.Read()){
                    ViewBag.num=id;
                    ViewBag.nom=dr2.GetValue(1).ToString();
                    ViewBag.hora=dr2.GetValue(2).ToString();
                    cliente=dr2.GetInt32(3);
                    ViewBag.cli=cliente;
                }
            dr2.Close();
        
            Random r = new Random();
            int num=r.Next(100001,999999);
            NpgsqlCommand cmd3= new NpgsqlCommand(String.Format(" insert into ventas values((select coalesce(max(codventa)+1,1) from ventas),'{0}',0,{1},1,{2},current_date )",num,cliente,id),conn);
            var row=cmd3.ExecuteNonQuery();
            conn.Close();
        }else{
            NpgsqlCommand command= new NpgsqlCommand("select p.tipoprod, p.nomproducto, d.cantidad ,p.precventa, d.cantidad*p.precventa, v.habnum, d.iddetventa  from detalleventa d, producto p, ventas v where d.productocod=p.codproducto and v.codventa=d.ventascod  and d.ventascod=(select max( v.codventa) from ventas v)",conn);
            NpgsqlDataReader dr2=command.ExecuteReader();
            while(dr2.Read()){
                Pedido pe= new Pedido();
                pe.tipo=dr2.GetValue(0).ToString();
                pe.nombre=dr2.GetValue(1).ToString();
                pe.cantidad=dr2.GetInt32(2);
                pe.precio=dr2.GetDouble(3);
                pe.subtotal=dr2.GetDouble(4);
                codd=dr2.GetInt32(5);
                pe.iddetve=dr2.GetInt32(6);
                pds.Add(pe);
            }
            dr2.Close();
            foreach(var item in pds){
                monto=monto+item.subtotal;
            }
            ViewBag.Pedido=pds;

           
            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("select rh.numhab,concat(c.nomcli,' ',c.apellpater,' ',c.apemater),to_char(current_timestamp ,'HH24:MI'),c.codcliente  from reservahab rh,reservahabitacion r, cliente c where c.codcliente=r.clientecod and rh.codreserva=r.codreserva and rh.numhab='{0}' and current_date between r.checkin and r.checkout",codd),conn);
            
                NpgsqlDataReader dr3 = cmd2.ExecuteReader();
                if(dr3.Read()){
                    ViewBag.num=dr3.GetInt32(0);
                    ViewBag.nom=dr3.GetValue(1).ToString();
                    ViewBag.hora=dr3.GetValue(2).ToString();
                    ViewBag.cli=dr3.GetInt32(3);
                
                }
                ViewBag.monto=monto;
            dr2.Close();
        }
            return View();
        }

     


[HttpPost]
        public IActionResult Registrar( Pedido p){
            List<Pedido> pds= new List<Pedido>();
            int codd=0;
            double monto=0;
            conn.Open();
            NpgsqlCommand cm2 = new NpgsqlCommand(String.Format("insert into detalleventa values((select coalesce(max(iddetventa)+1,1) from detalleventa),{0} ,'compra',(select precventa from producto where codproducto={1})*{0},(select max(codventa) from ventas),{1} )",p.cantidad,p.idprod),conn);
            var row2 =cm2.ExecuteNonQuery();
            NpgsqlCommand cmd = new NpgsqlCommand("select codproducto, tipoprod, nomproducto, precventa,stock from producto",conn);
            
                NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                Producto pr= new Producto();
                pr.idprod=dr.GetInt32(0);
                pr.tipo=dr.GetValue(1).ToString();
                pr.nom=dr.GetValue(2).ToString();
                pr.precio=dr.GetDouble(3);
                pr.stock=1;
                prods.Add(pr);
            }
            dr.Close();
            ViewBag.Prod=prods;
            
            NpgsqlCommand command= new NpgsqlCommand("select p.tipoprod, p.nomproducto, d.cantidad ,p.precventa, d.cantidad*p.precventa, v.habnum, d.iddetventa  from detalleventa d, producto p, ventas v where d.productocod=p.codproducto and v.codventa=d.ventascod  and d.ventascod=(select max( v.codventa) from ventas v)",conn);
            NpgsqlDataReader dr2=command.ExecuteReader();
            while(dr2.Read()){
                Pedido pe= new Pedido();
                pe.tipo=dr2.GetValue(0).ToString();
                pe.nombre=dr2.GetValue(1).ToString();
                pe.cantidad=dr2.GetInt32(2);
                pe.precio=dr2.GetDouble(3);
                pe.subtotal=dr2.GetDouble(4);
                codd=dr2.GetInt32(5);
                pe.iddetve=dr2.GetInt32(6);
                pds.Add(pe);
            }
            dr2.Close();
            foreach(var item in pds){
                monto=monto+item.subtotal;
            }
            ViewBag.Pedido=pds;

            NpgsqlCommand cm1 = new NpgsqlCommand(String.Format("update ventas set total={0} where codventa= (select max(codventa) from ventas)",monto),conn);
            var row1 =cm1.ExecuteNonQuery();

            NpgsqlCommand cmd2 = new NpgsqlCommand(String.Format("select rh.numhab,concat(c.nomcli,' ',c.apellpater,' ',c.apemater),to_char(current_timestamp ,'HH24:MI'),c.codcliente  from reservahab rh,reservahabitacion r, cliente c where c.codcliente=r.clientecod and rh.codreserva=r.codreserva and rh.numhab='{0}' and current_date between r.checkin and r.checkout",codd),conn);
            
                NpgsqlDataReader dr3 = cmd2.ExecuteReader();
                if(dr3.Read()){
                    ViewBag.num=dr3.GetInt32(0);
                    ViewBag.nom=dr3.GetValue(1).ToString();
                    ViewBag.hora=dr3.GetValue(2).ToString();
                    ViewBag.cli=dr3.GetInt32(3);
                
                }
                ViewBag.monto=monto;
            dr3.Close();
            conn.Close();
            return View();
        }

        public IActionResult Eliminar(int idd){

            conn.Open();
            NpgsqlCommand cmd= new NpgsqlCommand(String.Format("update ventas set total=(select total from ventas where codventa=(select d.ventascod from detalleventa d where d.iddetventa={0} ))-(select d.subtotal from detalleventa d where d.iddetventa={0}) where codventa=(select d.ventascod from detalleventa d where d.iddetventa={0} )",idd),conn);
            var row = cmd.ExecuteNonQuery();
            NpgsqlCommand cm2 = new NpgsqlCommand(String.Format("delete from detalleventa where iddetventa={0}",idd),conn);
            var row2 =cm2.ExecuteNonQuery();
            int id=0;
            conn.Close();
            return RedirectToAction("Registrar",id);

        }

        public IActionResult Detalle(){

            List<Pedido> p = new List<Pedido>();
            conn.Open();
            NpgsqlCommand command= new NpgsqlCommand("select p.tipoprod, p.nomproducto, d.cantidad ,p.precventa, d.cantidad*p.precventa, v.habnum, concat(c.nomcli,' ',c.apellpater,' ',c.apemater), v.total from detalleventa d, producto p, ventas v , cliente c where d.productocod=p.codproducto and v.codventa=d.ventascod and v.clientecod=c.codcliente  and d.ventascod=(select max(v.codventa) from ventas v)",conn);
            NpgsqlDataReader data = command.ExecuteReader();
            while(data.Read()){
                Pedido pe= new Pedido();
                pe.tipo=data.GetValue(0).ToString();
                pe.nombre=data.GetValue(1).ToString();
                pe.cantidad=data.GetInt32(2);
                pe.precio=data.GetDouble(3);
                pe.subtotal=data.GetDouble(4);
                ViewBag.num=data.GetInt32(5);
                ViewBag.nombre=data.GetValue(6).ToString();
                ViewBag.total=data.GetDouble(7);
                p.Add(pe);

            }
            conn.Close();

            ViewBag.peds=p;
            return View();
        }
    
    }
}