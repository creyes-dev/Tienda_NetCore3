using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Tienda_NetCore3.Models;
using Tienda_NetCore3.Models.ViewModels;
using Tienda_NetCore3.Utility;

namespace Tienda_NetCore3.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CompraController : Controller
    {
            private readonly IUnitOfWork _unitOfWork;

            public CompraController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Detalles(int id)
        {
            CompraViewModel compra = new CompraViewModel
            {
                EncabezadoCompra = _unitOfWork.encabezadoCompra.Get(id),
                DetallesCompra = _unitOfWork.detalleCompra.GetAll(filtro: c => c.IdEncabezadoCompra == id)
            };
            return View(compra);
        }

        public IActionResult Aprobar(int id)
        {
            var compra = _unitOfWork.encabezadoCompra.Get(id);
            if(compra == null)
            {
                return NotFound();
            }

            _unitOfWork.encabezadoCompra.CambiarEstadoCompra(id, SD.EstadoAprobado);
            return View(nameof(Index));
        }

        public IActionResult Rechazar(int id)
        {
            var compra = _unitOfWork.encabezadoCompra.Get(id);
            if (compra == null)
            {
                return NotFound();
            }

            _unitOfWork.encabezadoCompra.CambiarEstadoCompra(id, SD.EstadoRechazado);
            return View(nameof(Index));
        }


        #region API Calls

        public IActionResult ObtenerTodas()
            {
                return Json(new { data = _unitOfWork.encabezadoCompra.GetAll() });
            }

            public IActionResult ObtenerTodasComprasPendientes()
            {
                return Json(new { data = _unitOfWork.encabezadoCompra.GetAll(filtro: o => o.Estado == SD.EstadoEnviado) });
            }

            public IActionResult ObtenerTodasComprasAprobadas()
            {
                return Json(new { data = _unitOfWork.encabezadoCompra.GetAll(filtro: o => o.Estado == SD.EstadoAprobado) });
            }

            #endregion

    }
}