using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Tienda_NetCore3.Models;

namespace Tienda_NetCore3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if(id == null)
            {
                return View(categoria);
            }
            categoria = _unitOfWork.categoria.Get(id.GetValueOrDefault());
            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                if(categoria.Id == 0)
                {
                    _unitOfWork.categoria.Add(categoria);
                }
                else
                {
                    _unitOfWork.categoria.Update(categoria);
                }
                _unitOfWork.AplicarCambios();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            return Json(new { data = _unitOfWork.categoria.ObtenerListadoCategorias() });
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var categoria = _unitOfWork.categoria.Get(id);
            if(categoria == null)
            {
                return Json(new { success = false, message = "Error al eliminar la categoría" });
            }

            _unitOfWork.categoria.Remove(categoria);
            _unitOfWork.AplicarCambios();

            return Json(new { success = true, message = "La categoría seleccionada ha sido eliminada" });
        }

        #endregion

    }
}