using Api.Dto;
using Api.Entities;
using Api.Repository;
using Common.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Impl
{
    [Service(Scope = "Transient")]
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration config;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(IConfiguration config, IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
        {
            this.config = config;
            this.usuarioRepository = usuarioRepository;
            this.logger = logger;
        }
        public ObjectResult Listar_Usuario()
        {
            try
            {
                return this.usuarioRepository.Listar_Usuario();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Listar_Usuario.", ex);
            }
        }
        public ObjectResult Listar_Usuario_Por_Id(int idUsuario)
        {
            try
            {
                return this.usuarioRepository.Listar_Usuario_Por_Id(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Listar_Usuario.", ex);
            }
        }
        public ObjectResult Insertar_Usuario(UsuarioRequest usuarioRequest)
        {
            try
            {
                return this.usuarioRepository.Insertar_Usuario(usuarioRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Insertar_Usuario.", ex);
            }
        }
        public ObjectResult Actualizar_Usuario(ActUsuRequest usuarioRequest)
        {
            try
            {
                return this.usuarioRepository.Actualizar_Usuario(usuarioRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Actualizar_Usuario.", ex);
            }
        }
        public ObjectResult Eliminar_Usuario(int idUsuario, byte estatus)
        {
            try
            {
                return this.usuarioRepository.Eliminar_Usuario(idUsuario, estatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Eliminar_Usuario.", ex);
            }
        }
    }
}
