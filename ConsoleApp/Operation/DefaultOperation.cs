using ConsoleApp.Operation.Interfaces;
using static System.Guid;

namespace ConsoleApp.Operation;
public class DefaultOperation : ITransientOperation,
                                IScopedOperation,
                                ISingletonOperation
{
  public string OperationId { get; } = NewGuid().ToString()[^4..];
}