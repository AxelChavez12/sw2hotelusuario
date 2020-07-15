namespace web_user.Models
{
    public class Reserva
    {
        
        public int numdoc { get; set; }

        public string tipodoc { get; set; }
        public string ape { get; set; }

        public string nom{get;set;}
        public string motivo { get; set; }

        public string fecha { get; set; }

        public string correo { get; set; }
        public string tippago { get; set; }
        
        public int telefono{get;set;}
        public string checkin{get;set;}

        public string checkout{get;set;}
        public string habitaciones { get; set; }

        public double precio{get;set;}
        public double cant{get;set;}
        public double sub{get;set;}
    }
}