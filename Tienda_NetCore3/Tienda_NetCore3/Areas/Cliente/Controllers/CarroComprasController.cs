using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.Models;
using Tienda_NetCore3.Models.ViewModels;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Http;
using Tienda_NetCore3.Utility;
using Tienda_NetCore3.Extensions;

namespace Tienda_NetCore3.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CarroComprasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarroComprasViewModel CarroComprasViewModel { get; set; }

        public CarroComprasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CarroComprasViewModel = new CarroComprasViewModel()
            {
                EncabezadoCompra = new EncabezadoCompra(),
                ListadoServicios = new List<Servicio>()
            };
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras) != null)
            {
                List<int> listadoIdServicios = new List<int>();
                listadoIdServicios = HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras);
                foreach (int idServicio in listadoIdServicios)
                {
                    CarroComprasViewModel.ListadoServicios.Add(_unitOfWork.servicio.GetFirstOrDefault(u => u.Id == idServicio, incluirPropiedades: "Frecuencia,Categoria"));
                }
            }

            return View(CarroComprasViewModel);
        }

        public IActionResult Remover(int idServicio)
        {
            List<int> listadoIdServicios = new List<int>();
            listadoIdServicios = HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras);
            listadoIdServicios.Remove(idServicio);
            HttpContext.Session.SetObject(SD.SesionCarritoCompras, listadoIdServicios);

            return RedirectToAction(nameof(Index));
        }

    }
}