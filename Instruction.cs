using Godot;

public abstract class Instruction
{
    public abstract void Evaluate();
    public abstract void Execute(); 
}

public class FillInstruction : Instruction
{
    public FillInstruction(){}
    public override void Evaluate(){}
    public override void Execute() 
    { 
        Canvas.Fill();
    }
}
public class SpawnInstruction : Instruction
{
    private readonly Expression posX;
    private readonly Expression posY;

    public SpawnInstruction(Expression posX, Expression posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
    public override void Evaluate()
    {
        if(posX.GetValue() is int
        && posY.GetValue() is int)
        return;
        else throw new ExecutionError("La instruccion Spawn solo soporta valores de tipo int");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.Spawn((int)posX.GetValue(), (int)posY.GetValue());
    }
}

public class ColorInstruction : Instruction
{
    private readonly Expression color;

    public ColorInstruction(Expression color)
    {
        this.color = color;
    }
    public override void Evaluate()
    {
        if(color.GetValue() is string) return;
        else throw new ExecutionError("La instruccion Color() solo soporta valores de tipo string");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.ColorAsing((string)color.GetValue());
    }
}
public class SizeInstruction : Instruction
{
    private readonly Expression size;

    public SizeInstruction(Expression size)
    {
        this.size = size;
    }
    public override void Evaluate()
    {
        if(size.GetValue() is int) return;
        else throw new ExecutionError("La instruccion Size() solo recibe valores de tipo int");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.Size((int) size.GetValue());
    }
}
public class DrawLineInstruction : Instruction
{
    private readonly Expression dirX;
    private readonly Expression dirY;
    private readonly Expression distance;

    public DrawLineInstruction(Expression dirX, Expression dirY, Expression distance)
    {
        this.dirX = dirX;
        this.dirY = dirY;
        this.distance = distance;
    }
    public override void Evaluate()
    {
        if( dirX.GetValue() is  int
        && dirY.GetValue() is  int
        && distance.GetValue() is  int) return;
        else throw new ExecutionError("La instruccion DrawLine() solo recibe valores de tipo int");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.DrawLine((int)dirX.GetValue(), (int)dirY.GetValue(), (int)distance.GetValue());
    }
}
public class DrawCircleInstruction : Instruction
{
    private readonly Expression dirX;
    private readonly Expression dirY;
    private readonly Expression radius;

    public DrawCircleInstruction(Expression dirX, Expression dirY, Expression radius)
    {
        this.dirX = dirX;
        this.dirY = dirY;
        this.radius = radius;
    }
    public override void Evaluate()
    {
        if( dirX.GetValue() is  int
        && dirY.GetValue() is  int
        && radius.GetValue() is  int) return;
        else throw new ExecutionError("La instruccion DrawCircle() solo recibe valores de tipo int");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.DrawCircle((int)dirX.GetValue(), (int)dirY.GetValue(), (int)radius.GetValue());
    }
}
public class DrawRectangleInstruction : Instruction
{
    private readonly Expression dirX;
    private readonly Expression dirY;
    private readonly Expression distance;
    private readonly Expression width;
    private readonly Expression height;

    public DrawRectangleInstruction(Expression dirX, Expression dirY, Expression distance, Expression width, Expression height)
    {
        this.dirX = dirX;
        this.dirY = dirY;
        this.distance = distance;
        this.width = width;
        this.height = height;
    }
    public override void Evaluate()
    {
         if( dirX.GetValue() is  int
        && dirY.GetValue() is  int
        && distance.GetValue() is  int
        && width.GetValue() is int
        && height.GetValue() is int) return;
        else throw new ExecutionError("La instruccion DrawRectangle() solo recibe valores de tipo int");
    }
    public override void Execute()
    {
        Evaluate();
        Canvas.DrawRectangle((int)dirX.GetValue(), (int)dirY.GetValue(), (int)distance.GetValue(),
         (int)width.GetValue(), (int)height.GetValue());
    }
}
