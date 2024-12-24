namespace CollegeKids2._0.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string DetalleConsulta { get; set; }
        public string Seccion { get; set; }
        public string Grado { get; set; }
        public bool Atendida { get; set; }

        public DateTime FechaConsulta { get; set; }
    }

}
