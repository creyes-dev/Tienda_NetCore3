using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda_NetCore3.AccesoDatos.Data.Repository;
using Tienda_NetCore3.Models;
using Tienda_NetCore3.Utility;

namespace Tienda_NetCore3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Identificamos al claim NameIdentifier igual al id del usuario porque es unico
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_unitOfWork.usuario.GetAll(u => u.Id != claims.Value));
        }

        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));            
        }

        public IActionResult Desbloquear(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _unitOfWork.usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

    }
}