using CoreFirebase.Helpers;
using CoreFirebase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreFirebase.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly DataContext _context;
        private readonly IUploadStorage _uploadStorage;

        public EmpleadosController(DataContext context,IUploadStorage uploadStorage )
        {
            _context = context;
            _uploadStorage = uploadStorage;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados                
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {

                //RECIBIR LOS DATOS DEL FORMULARIO
                Stream image = Imagen.OpenReadStream();
                string urlimagen = await _uploadStorage.SubirStorage(image, Imagen.FileName);

                try
                {
                    empleado.URLImagen = urlimagen;
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ViewBag.Error(exception.Message);
                }
            }
            return View(empleado);
        }
    }
}
