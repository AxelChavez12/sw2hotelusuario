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
            return View();
     }  
        
    [HttpPost]

    public IActionResult Registrar (Cliente a){
        NpgsqlConnection conn = new NpgsqlConnection("Host = ec2-34-197-141-7.compute-1.amazonaws.com; Username=ndjaxklicmdweo;Password= 1ce8484d6fcc56b48073eca44510227bab6703584f2b994f37b8a0de42570940;Database = d6pb7d8nu1qd7t; Port= 5432; SSL Mode= Require; Trust Server certificate = true");
        conn.Open();
        NpgsqlCommand cmd = new NpgsqlCommand(String.Format("insert into acompañante values ('{0}', '{1}', '{2}', '{3}','{4}')",a.numdocacomp , a.tipdoccodacomp, a.apellacomp, a.nomacomp,a.numdocli), conn);
        var row = cmd.ExecuteNonQuery();
        conn.Close();
        return RedirectToAction("Index");
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
