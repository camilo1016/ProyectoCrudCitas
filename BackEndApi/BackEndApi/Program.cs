using BackEndApi.Models;
using Microsoft.EntityFrameworkCore;

using BackEndApi.Services.Contrato;
using BackEndApi.Services.Implementacion;

using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbpacienteContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region PETICIONES API REST
app.MapGet("/tipoidentificacion/lista", async(
    ITipoIdentificacionService _tipoidentificacionServicio,
    IMapper _mapper
    ) => 
{

    var listaTipoIdentificacion = await _tipoidentificacionServicio.GetList();
    var listaTipoIdentificacionDTO = _mapper.Map<List<TipoIdentificacionDTO>>(listaTipoIdentificacion);

    if (listaTipoIdentificacionDTO.Count > 0)
        return Results.Ok(listaTipoIdentificacionDTO);
    else
        return Results.NotFound();

});

app.MapGet("/paciente/lista", async (

    IPacienteService _pacienteServicio,
    IMapper _mapper
    ) =>

{

    var listaPaciente = await _pacienteServicio.GetList();
    var listaPacienteDTO = _mapper.Map<List<PacienteDTO>>(listaPaciente);

    if (listaPacienteDTO.Count > 0)
        return Results.Ok(listaPacienteDTO);
    else
        return Results.NotFound();

});

app.MapPost("/paciente/guardar", async (

    PacienteDTO modelo,
    IPacienteService _pacienteServicio,
    IMapper _mapper

    ) => {
        
        var _paciente = _mapper.Map<Paciente>(modelo);
        var _pacienteCreado = await _pacienteServicio.Add(_paciente);

        if (_pacienteCreado.IdPaciente != 0)
            return Results.Ok(_mapper.Map<PacienteDTO>(_pacienteCreado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

});


app.MapPut("/paciente/actualizar/{idPaciente}", async (

    int idPaciente,
    PacienteDTO modelo,
    IPacienteService _pacienteServicio,
    IMapper _mapper

    ) => {

        var _encontrado = await _pacienteServicio.Get(idPaciente);

        if(_encontrado is null) 
            return Results.NotFound();

        var _paciente = _mapper.Map<Paciente>(modelo);

        _encontrado.NombreCompleto = _paciente.NombreCompleto;
        _encontrado.IdTipoId = _paciente.IdTipoId;
        _encontrado.NumeroId = _paciente.NumeroId;
        _encontrado.Consulta = _paciente.Consulta;
        _encontrado.FechaConsulta = _paciente.FechaConsulta;

        var respuesta = await _pacienteServicio.Update(_encontrado);

        if (respuesta)
            return Results.Ok(_mapper.Map<PacienteDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

app.MapDelete("/paciente/eliminar/{idPaciente}", async (
    
    int idPaciente,
    IPacienteService _pacienteServicio

    ) => {

        var _encontrado = await _pacienteServicio.Get(idPaciente);

        if (_encontrado is null) 
            return Results.NotFound();

        var respuesta = await _pacienteServicio.Delete(_encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

#endregion

app.UseCors("NuevaPolitica");

app.Run();
