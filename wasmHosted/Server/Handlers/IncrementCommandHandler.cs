﻿using System;
using System.Threading;
using System.Threading.Tasks;
using wasmHosted.Shared;
using MediatR;
using wasmHosted.Shared;

namespace wasmHosted.Server.Handlers
{
    public class IncrementCommandHandler : IRequestHandler<IncrementCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(IncrementCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("something got incremented");

            return CommandResponse.Ok("Increment things");

            return CommandResponse.Problem("Incrementing things went bad");
        }
    }
}