using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Microservicio.Login.Api.Modelo;
using Microservicio.Login.Api.Persistencia;

namespace Microservicio.Login.Api.Aplicacion
{
    public class ConsultaLoginPorUsuario
    {
        public class Ejecuta : IRequest<LoginDto>
        {
            public string Usuario { get; set; } // Parámetro de búsqueda
        }

        public class Manejador : IRequestHandler<Ejecuta, LoginDto>
        {
            private readonly ContextoMongo _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoMongo contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LoginDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // 1. Búsqueda CASE-SENSITIVE exacta
                var login = await _contexto.LoginCollection
                    .Find(x => x.Usuario == request.Usuario) // Sin ToLower()
                    .FirstOrDefaultAsync();

                // 2. Validación estricta
                if (login == null)
                    throw new KeyNotFoundException("Usuario no encontrado o el nombre de usuario no esta escrito correctamente"); // Excepción específica

                return _mapper.Map<LoginDto>(login);
            }
        }
    }
}