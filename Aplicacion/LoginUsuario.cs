using MediatR;
using MongoDB.Driver;
using Microservicio.Login.Api.Modelo;
using Microservicio.Login.Api.Persistencia;
using AutoMapper;

namespace Microservicio.Login.Api.Aplicacion
{
    public class LoginUsuario
    {
        public class EjecutaLogin : IRequest<UsuarioDto>
        {
            public string Usuario { get; set; }
            public string Password { get; set; }
        }

        public class Manejador : IRequestHandler<EjecutaLogin, UsuarioDto>
        {
            private readonly ContextoMongo _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoMongo contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<UsuarioDto> Handle(EjecutaLogin request, CancellationToken cancellationToken)
            {
                // 1. Buscar usuario exacto (case-sensitive)
                var usuario = await _contexto.UsuarioCollection
                    .Find(x => x.Usuario == request.Usuario)
                    .FirstOrDefaultAsync();

                // 2. Validar existencia
                if (usuario == null)
                    throw new KeyNotFoundException("El usuario no existe");

                // 3. Validar contraseña
                if (usuario.Password != request.Password)
                    throw new UnauthorizedAccessException("La contraseña es incorrecta");

                // 4. Mapear y devolver
                return _mapper.Map<UsuarioDto>(usuario);
            }
        }
    }
}
