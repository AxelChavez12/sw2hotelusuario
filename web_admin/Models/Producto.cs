namespace web_admin.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public string nom{get;set;}

        public double precio{get;set;}
        public int stock { get; set; }
    }
}