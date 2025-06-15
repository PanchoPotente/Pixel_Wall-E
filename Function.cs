public abstract class Function : Expression
{

}
public class GetActualXFunction : Function
{
    public GetActualXFunction()
    {

    }
    public override object GetValue() => Canvas.GetActualX();
    public override void Evaluate()
    {
    }

}
public class GetActualYFunction : Function
{
    public GetActualYFunction()
    {

    }
    public override object GetValue() => Canvas.GetActualY();
    public override void Evaluate(){}
}
public class GetCanvasSizeFunction : Function
{
    public GetCanvasSizeFunction(){}
    public override object GetValue() => Canvas.GetCanvasSize();
    public override void Evaluate(){}
}
public class GetColorCountFunction : Function
{
    private readonly Expression color;
    private readonly Expression startX;
    private readonly Expression startY;
    private readonly Expression endX;
    private readonly Expression endY;

    public GetColorCountFunction(Expression color, Expression startX, Expression startY, Expression endX, Expression endY)
    {
        this.color = color;
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
    }
    public override object GetValue()
    {
        Evaluate();
        return Canvas.GetColorCount((string)color.GetValue(), (int)startX.GetValue(), 
        (int)startY.GetValue(), (int)endX.GetValue(), (int)endY.GetValue());
    }
    public override void Evaluate()
    {
        if(color.GetValue() is string
        && startX.GetValue() is int
        && startY.GetValue() is int
        && endX.GetValue() is int
        && endY.GetValue() is int) return;
        else throw new ExecutionError("");
    }
}
public class IsBrushColorFunction : Function
{
    private readonly Expression color;

    public IsBrushColorFunction(Expression color)
    {
        this.color = color;
    }
    public override void Evaluate()
    {
        if(color.GetValue() is string) return;
        else throw new ExecutionError("La funcion IsBrushColor() solo recibe argumentos de tipo string");
    }
    public override object GetValue()
    {
        Evaluate();
        return Canvas.IsBrushColor((string)color.GetValue());
    }
}
public class IsBrushSizeFunction : Function
{
    private readonly Expression size;

    public IsBrushSizeFunction(Expression size)
    {
        this.size = size;
    }
    public override void Evaluate()
    {
        if(size.GetValue() is int) return;
        else throw new ExecutionError("La funcion IsBrushSize solo recibe parametros de tipo int");
    }
    public override object GetValue()
    {
        Evaluate();
        return Canvas.IsBrushSize((int)size.GetValue());
    }
}
public class IsCanvasColorFunction : Function
{
    private readonly Expression color;
    private readonly Expression vertical;
    private readonly Expression horizontal;

    public IsCanvasColorFunction(Expression color, Expression vertical, Expression horizontal)
    {
        this.color = color;
        this.vertical = vertical;
        this.horizontal = horizontal;
    }
    public override void Evaluate()
    {
        if(color.GetValue() is string
        && vertical.GetValue() is int
        && horizontal.GetValue() is int) return;
        else throw new ExecutionError("Argumento invalido en la funcion IsCanvasColor()");
    }
    public override object GetValue()
    {
        Evaluate();
        return Canvas.IsCanvasColor((string) color.GetValue(), (int)vertical.GetValue(), (int) horizontal.GetValue());
    }

}