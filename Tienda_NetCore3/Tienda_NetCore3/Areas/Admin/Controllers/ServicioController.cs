using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Tienda_NetCore3.Models;
using Tienda_NetCore3.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Tienda_NetCore3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServicioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServicioViewModel servicioViewModel { get; set; }

        public ServicioController(IUnitOfWork unitOfWork, IWebHostEnvironment entornoHost)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = entornoHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            servicioViewModel = new ServicioViewModel()
            {
                Servicio = new Servicio(),
                ListadoCategorias = _unitOfWork.categoria.ObtenerListadoCategoriasParaDropDown(),
                ListadoFrecuencias = _unitOfWork.frecuencia.ObtenerListadoFrecuenciasParaDropDown(),
            };

            if(id != null)
            {
                // El usuario requiere editar el servicio
                servicioViewModel.Servicio = _unitOfWork.servicio.Get(id.GetValueOrDefault());
            }

            return View(servicioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (servicioViewModel.Servicio.Id == 0)
                {
                    // Nuevo servicio
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var archivoSubir = Path.Combine(webRootPath, @"images\servicios");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var lecturaArchivo = new FileStream(Path.Combine(archivoSubir, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(lecturaArchivo);
                    }

                    servicioViewModel.Servicio.UrlImagen = @"\images\servicios\" + nombreArchivo + extension;

                    _unitOfWork.servicio.Add(servicioViewModel.Servicio);
                }
                else
                {
                    // Editar servicio
                    var servicio = _unitOfWork.servicio.Get(servicioViewModel.Servicio.Id);
                    if (archivos.Count > 0)
                    {
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var archivoSubir = Path.Combine(webRootPath, @"images\servicios");
                        var nueva_extension = Path.GetExtension(archivos[0].FileName);

                        var directorioImagen = Path.Combine(webRootPath, servicio.UrlImagen.TrimStart('\\'));
                        if (System.IO.File.Exists(directorioImagen))
                        {
                            System.IO.File.Delete(directorioImagen);
                        }

                        using (var lecturaArchivo = new FileStream(Path.Combine(archivoSubir, nombreArchivo + nueva_extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(lecturaArchivo);
                        }

                        servicioViewModel.Servicio.UrlImagen = @"\images\servicios" + nombreArchivo + nueva_extension;
                    }
                    else
                    {
                        servicioViewModel.Servicio.UrlImagen = servicio.UrlImagen;
                    }

                    _unitOfWork.servicio.Update(servicioViewModel.Servicio);
                }
                _unitOfWork.AplicarCambios();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                servicioViewModel.ListadoCategorias = _unitOfWork.categoria.ObtenerListadoCategoriasParaDropDown();
                servicioViewModel.ListadoFrecuencias = _unitOfWork.frecuencia.ObtenerListadoFrecuenciasParaDropDown();

                return View(servicioViewModel);
            }
        }

        #region API Calls

        public IActionResult ObtenerTodas()
        {
            return Json(new { data = _unitOfWork.servicio.GetAll(incluirPropiedades: "Categoria,Frecuencia") });
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var servicio = _unitOfWork.servicio.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;

            var directorioImagen = Path.Combine(webRootPath, servicio.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(directorioImagen))
            {
                System.IO.File.Delete(directorioImagen);
            }

            if(servicio == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            _unitOfWork.servicio.Remove(servicio);
            _unitOfWork.AplicarCambios();

            return Json(new { success = true, message = "Eliminado satisfactoriamente" });
        }

        #endregion

    }
}