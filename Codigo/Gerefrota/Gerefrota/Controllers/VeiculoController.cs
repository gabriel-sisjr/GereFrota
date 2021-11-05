﻿using Domain.Abstract.Services;
using Gerefrota.Extensions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Gerefrota.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IFrotaService _frotaService;
        private readonly IUnidadeService _unidadeService;
        public VeiculoController(IVeiculoService veiculoService, IFrotaService frotaService, IUnidadeService unidadeService)
            => (_veiculoService, _frotaService, _unidadeService) = (veiculoService, frotaService, unidadeService);

        [HttpGet]
        public IActionResult Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            var usuario = identity.GetIdentityUser();
            var frotas = _frotaService.ObterTodasAsFrotasPorUnidade(usuario.Usuario.IdUnidade);
            var unidade = _unidadeService.Get(u => u.Id == usuario.Usuario.IdUnidade);
            var veiculos = frotas.Select(frota => _veiculoService.ObterTodosVeiculosDaFrota(frota.Id));

            return Ok(new { veiculos, unidade });
        }
    }
}
