using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tienda_NetCore3.Models;
using Tienda_NetCore3.Models.ViewModels;
using Tienda_NetCore3.AccesoDatos.Data.Repository;

namespace Tienda_NetCore3.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel _homeViewModel;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            _homeViewModel = new HomeViewModel()
            {
                ListadoCategorias = _unitOfWork.categoria.GetAll(),
                ListadoServicios = _unitOfWork.servicio.GetAll(incluirPropiedades: "Frecuencia")
            };

            return View(_homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
