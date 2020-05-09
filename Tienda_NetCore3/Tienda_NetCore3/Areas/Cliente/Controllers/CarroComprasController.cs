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

        public IActionResult Resumen()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras) != null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Resumen")]
        public IActionResult ResumenPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras) != null)
            {
                List<int> listadoIdServicios = new List<int>();
                listadoIdServicios = HttpContext.Session.GetObject<List<int>>(SD.SesionCarritoCompras);
                CarroComprasViewModel.ListadoServicios = new List<Servicio>();
                foreach (int idServicio in listadoIdServicios)
                {
                    CarroComprasViewModel.ListadoServicios.Add(_unitOfWork.servicio.GetFirstOrDefault(u => u.Id == idServicio, incluirPropiedades: "Frecuencia,Categoria"));
                }
            }
            
            if(!ModelState.IsValid)
            {
                return View(CarroComprasViewModel);
            }
            else
            {
                // Generar una nueva compra
                CarroComprasViewModel.EncabezadoCompra.FechaCompra = DateTime.Now;
                CarroComprasViewModel.EncabezadoCompra.Estado = SD.EstadoEnviado;
                CarroComprasViewModel.EncabezadoCompra.ConteoServicios = CarroComprasViewModel.ListadoServicios.Count;
                _unitOfWork.encabezadoCompra.Add(CarroComprasViewModel.EncabezadoCompra);
                _unitOfWork.AplicarCambios(); // CarroCOmprasViewModel.EncabezadoCompra ahora adquiere su id

                foreach (var item in CarroComprasViewModel.ListadoServicios)
                {
                    // Por cada servicio comprado agregar un detalle de compra
                    DetalleCompra detalle = new DetalleCompra()
                    {
                        IdServicio = item.Id,
                        IdEncabezadoCompra = CarroComprasViewModel.EncabezadoCompra.Id,
                        NombreServicio = item.Nombre,
                        Precio = item.Precio
                    };

                    _unitOfWork.detalleCompra.Add(detalle);
                }

                // Al final aplicar los cambios
                _unitOfWork.AplicarCambios();

                // Reiniciar la variable de sesión del carro de compras
                HttpContext.Session.SetObject(SD.SesionCarritoCompras, new List<int>());
                return RedirectToAction("ConfirmacionCompra", "CarroCompras",new { id = CarroComprasViewModel.EncabezadoCompra.Id });
            }            
        }

        public IActionResult ConfirmacionCompra(int idEncabezadoCompra)
        {
            return View(idEncabezadoCompra);
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