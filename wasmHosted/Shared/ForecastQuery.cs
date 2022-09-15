using System;
using System.Collections.Generic;
using MediatR;

namespace wasmHosted.Shared
{
    public class ForecastQuery : IRequest<IEnumerable<WeatherForecast>>
    {
    }
}