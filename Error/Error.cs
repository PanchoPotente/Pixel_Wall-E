using System;
class SyntacticError : Exception
{
    public SyntacticError(string Message) : base(Message){}
}
class SemanticError : Exception
{
    public SemanticError(string Message) : base(Message){}
}
class ExecutionError : Exception
{
    public ExecutionError(string Message) : base(Message){}
}