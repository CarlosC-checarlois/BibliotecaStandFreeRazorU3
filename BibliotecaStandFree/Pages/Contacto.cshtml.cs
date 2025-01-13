﻿using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace BibliotecaStandFree.Pages
{
    public class ContactoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContactoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contacto FormularioContacto { get; set; }

        public void OnGet()
        {
            // Inicializa datos si es necesario
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ResponseMessage"] = "Por favor, completa todos los campos correctamente.";
                return Page();
            }

            // Guardar en la base de datos
            FormularioContacto.FechaEnvio = DateTime.Now;
            _context.Contactos.Add(FormularioContacto);
            _context.SaveChanges();

            TempData["ResponseMessage"] = "¡Gracias por tu mensaje! Nos pondremos en contacto contigo pronto.";
            return RedirectToPage("/Contacto");
        }
    }
}