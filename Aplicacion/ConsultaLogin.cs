using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Microservicio.Login.Api.Modelo;
using Microservicio.Login.Api.Persistencia;

namespace Microservicio.Login.Api.Aplicacion
{
    public class ConsultaLogin
    {
        public class ListaLogin : IRequest<List<LoginDto>> { }

        public class Manejador : IRequestHandler<ListaLogin, List<LoginDto>>
        {
            private readonly ContextoMongo _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoMongo contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LoginDto>> Handle(ListaLogin request, CancellationToken cancellationToken)
            {
                var logins = await _contexto.LoginCollection.Find(_ => true).ToListAsync();
                return _mapper.Map<List<LoginDto>>(logins);
            }
        }
    }
}
