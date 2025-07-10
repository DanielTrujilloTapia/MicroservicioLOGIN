using FluentValidation;
using MediatR;
using Microservicio.Login.Api.Modelo;
using Microservicio.Login.Api.Persistencia;

namespace Microservicio.Login.Api.Aplicacion
{
    public class NuevoLogin
    {
        // 📌 DTO de Request que implementa IRequest
        public class Ejecuta : IRequest<Unit>
        {
            public string Usuario { get; set; }
            public string Password { get; set; }
            
            // Nuevos campos
            public string PreguntaRecuperacion { get; set; }
            public string RespuestaRecuperacion { get; set; }
        }

        // 📌 Validación de FluentValidation
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Usuario).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
                RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria");
                // Validación para los nuevos campos
                RuleFor(x => x.PreguntaRecuperacion).NotEmpty().WithMessage("La pregunta de recuperación es obligatoria");
                RuleFor(x => x.RespuestaRecuperacion).NotEmpty().WithMessage("La respuesta de recuperación es obligatoria");
            }
        }

        // 📌 Manejador de MediatR
        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly ContextoMongo _context;

            public Manejador(ContextoMongo context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var login = new Modelo.Login
                {
                    Usuario = request.Usuario,
                    Password = request.Password,
                    FechaRegistro = DateTime.UtcNow,
                    LoginGuid = Guid.NewGuid().ToString(),
                    // Nuevos campos
                    PreguntaRecuperacion = request.PreguntaRecuperacion,
                    RespuestaRecuperacion = request.RespuestaRecuperacion
                };

                await _context.LoginCollection.InsertOneAsync(login);

                return Unit.Value;
            }
        }
    }
}
