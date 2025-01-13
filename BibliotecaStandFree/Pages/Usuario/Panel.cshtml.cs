using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace BibliotecaStandFree.Pages.Usuario
{
    public class PanelModel : PageModel
    {
        public string UsuarioStatus { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        public List<Carrito> Carritos { get; set; }

        public void OnGet()
        {
            // Simulando datos del usuario
            UsuarioStatus = "ACT"; // Puedes obtener esto de tu base de datos
            UsuarioNombre = "Carlos";
            UsuarioApellido = "Constante";

            // Simulando datos de carritos
            Carritos = new List<Carrito>
            {
                new Carrito { Codigo = "C-00003", Fecha = DateTime.Now.AddHours(-2), Estado = "ACT" },
                new Carrito { Codigo = "C-00002", Fecha = DateTime.Now.AddHours(-6), Estado = "ACT" },
                new Carrito { Codigo = "C-00001", Fecha = DateTime.Now.AddHours(-12), Estado = "ACT" },
            };
        }
    }

    public class Carrito
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}