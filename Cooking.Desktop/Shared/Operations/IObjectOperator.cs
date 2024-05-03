using System;

namespace Cooking.Desktop.Shared.Operations;

public interface IObjectOperator<TObject>
{
    void Operate(TObject @object, OperationOptions<TObject> operationOptions);

    bool HasChanges { get; }
}

public record struct OperationOptions<TObject>(Action<TObject> OperationResultCallback);
