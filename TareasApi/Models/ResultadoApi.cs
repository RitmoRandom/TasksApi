namespace TareasApi.Models
{
    public class ResultadoApi
    {
        public string httpResponseCode { get; set; }
        public List<Tarea> LTareas { get; set; }
        public Tarea Tarea { get; set; }
    }
}
