using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Tienda_NetCore3.Models;

namespace Tienda_NetCore3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FrecuenciaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FrecuenciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Frecuencia frecuencia = new Frecuencia();
            if(id == null)
            {
                return View(frecuencia);
            }
            frecuencia = _unitOfWork.frecuencia.Get(id.GetValueOrDefault());
            if(frecuencia == null)
            {
                return NotFound();
            }
            return View(frecuencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Frecuencia frecuencia)
        {
            if (ModelState.IsValid)
            {
                if (frecuencia.Id == 0)
                {
                    _unitOfWork.frecuencia.Add(frecuencia);
                }
                else
                {
                    _unitOfWork.frecuencia.Update(frecuencia);
                }
                _unitOfWork.AplicarCambios();
                return RedirectToAction(nameof(Index));
            }
            return View(frecuencia);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            return Json(new { data = _unitOfWork.frecuencia.GetAll() });
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var frecuencia = _unitOfWork.frecuencia.Get(id);
            if (frecuencia == null)
            {
                return Json(new { success = false, message = "Error al eliminar la frecuencia" });
            }

            _unitOfWork.frecuencia.Remove(frecuencia);
            _unitOfWork.AplicarCambios();

            return Json(new { success = true, message = "La frecuencia seleccionada ha sido eliminada" });
        }

        #endregion




    }
}