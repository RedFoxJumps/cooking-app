using System;

namespace Cooking.Desktop.Shared.Operations;

public interface IObjectOperator<TObject>
{
    void Operate(TObject @object, OperationOptions<TObject> operationOptions);

    bool HasChanges { get; }
}

public record class OperationOptions<TObject>(Action<TObject> OperationResultCallback);

public interface IObjectOperator<TObject, TParams>
{
    void Operate(TObject @object, OperationOptions<TObject, TParams> operationOptions);

    bool HasChanges { get; }
}

public record class OperationOptions<TObject, TParams>(
    Action<TObject> OperationResultCallback,
    TParams Params);
