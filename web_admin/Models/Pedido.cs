namespace web_admin.Models
{
    public class Pedido
    {
        public int idprod { get; set; }
        public int cantidad{get;set;}
        public int numhab { get; set; }
        public int codcli { get; set; }

        public string tipo{get;set;}
        public string nombre{get;set;}
        public double precio{get;set;}
        public double subtotal{get;set;}
        
        public int iddetve { get; set; }
    }
}