using MediatR;

namespace Loader.Application.Commands.SaveDolar;

public class SaveDolarCommand : IRequest<Unit>
{
    public SaveDolarCommand(double value)
    {
        Value = value;
    }

    public double Value { get; set; }
}
