using System;
using Godot;

public partial class Canvas : Node
{
    public static int PositionX = 0;
    public static int PositionY = 0;
    public static Color BrushColor = Color.Transparent;
    public static int BrushSize = 1;
    public static Color[,] WorkZone = ClearCanvas(10);

    public static void Reset()
    {
        PositionX = 0;
        PositionY = 0;
        BrushColor = Color.Transparent;
        BrushSize = 1;
        WorkZone = ClearCanvas(10);
    }
    public static void Spawn(int x, int y)
    {
        if (IsInRange(x,y)) 
        {
            PositionX = x;
            PositionY = y;        
        }
        else throw new ExecutionError("El valor de los argumentos de la Instruccion Spawn() deben estar dentro de los limites del Canvas");
    }
    private static bool IsInRange(int x, int y)
    {
        return x < WorkZone.GetLength(0) && x >= 0 && y < WorkZone.GetLength(1) && y >= 0;
    }
    private static Color[,] ClearCanvas(int x)
    {
        Color[,] colors = new Color[x,x]; 
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                colors[i,j] = Color.White;
            }
        }
        return colors;
    }

    public static void ColorAsing(string color)
    {
        switch (color)
        {
            case "White":
            BrushColor = global::Color.White;
            return;
            case "Black":
            BrushColor = global::Color.Black;
            return;
            case "Yellow":
            BrushColor = global::Color.Yellow;
            return;
            case "Green":
            BrushColor = global::Color.Green;
            return;
            case "Purple":
            BrushColor = global::Color.Purple;
            return;
            case "Orange": 
            BrushColor = global::Color.Orange;
            return;
            case "Blue":
            BrushColor = Color.Blue; 
            return;
            case "Red":
            BrushColor = Color.Red;
            return;
            case "Transparent":
            BrushColor = global::Color.Transparent;
            return;
            default:
            throw new ExecutionError("El string de la Instruccion Color no representa un color valido");
        }
    }

    public static void Size(int k)
    {
        if(k > 0) 
        {
            if (k % 2 == 0) BrushSize = k - 1;
            else BrushSize = k;
        }
        else throw new ExecutionError("El valor recibido por la intruccion Size() debe ser mayor que cero");
    }

    public static void DrawLine(int dirX, int dirY, int distance)
    {
        if((dirX != 1 && dirX != 0 && dirX != -1 )|| (dirY != 1 && dirY != -1 && dirY != 0)  )
        throw new ExecutionError("Los valores de direccion de la Instruccion DrawLine() deben ser 0 , 1 , -1");
        if(distance <= 0) throw new ExecutionError("El radio debe tener un valor mayor o igual que cero");
        for (int i = 0; i < distance; i++)
        {
            DrawPoint(PositionX, PositionY);
            PositionX += dirX;
            PositionY += dirY; 
        }
        PositionX += BrushSize;
        PositionY += BrushSize;
    }

    private static void DrawPoint(int posX, int posY)
    {
        for (int i = 0; i < BrushSize; i++)
        {
            for (int j = 0; j < BrushSize; j++)
            {
                if (IsInRange(posX + i, posY + j))
                {
                    if(BrushColor != Color.Transparent) WorkZone[posX + i, posY +j] = BrushColor;
                }
                else throw new ExecutionError("No es posible dibujar fuera de los limites del Canvas");
            }
        }
    }

    public static void DrawCircle(int dirX, int dirY, int radius)
    {
        if(radius <= 0) throw new ExecutionError("El radio debe tener un valor mayor o igual que cero");
        if((dirX != 1 && dirX != 0 && dirX != -1 )|| (dirY != 1 && dirY != -1 && dirY != 0)  )
        throw new ExecutionError("Los valores de direccion de la Instruccion DrawLine() deben ser 0 , 1 , -1");
        PositionX += radius * dirX;
        PositionY += radius * dirY; 
        if(IsInRange(PositionX,PositionY))
        {

        }
        else throw new ExecutionError("La posicion no puede quedar fuera de los limites del Canvas");
    }

    public static void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
    {
        if((dirX != 1 && dirX != 0 && dirX != -1 )|| (dirY != 1 && dirY != -1 && dirY != 0)  )
        throw new ExecutionError("Los valores de direccion de la Instruccion DrawLine() deben ser 0 , 1 , -1");
        if(distance <= 0) throw new ExecutionError("El radio debe tener un valor mayor o igual que cero");
        PositionX += distance * dirX;
        PositionY += distance * dirY; 
        if(IsInRange(PositionX,PositionY))
        {
            int left = width/2;
            int rigth = width - (width/2);
            int up = height / 2;
            int down = height - (height/2);
            int posX = PositionX;
            int posY = PositionY;

            for (int i = 0; i <= width; i++)
            {
                DrawPoint(PositionX - left + i, PositionY - up);
                DrawPoint(PositionX - left + i, PositionY + down);
            }
            for (int i = 0; i <= height; i++)
            {
                DrawPoint(PositionX - left, PositionY - up + i);
                DrawPoint(PositionX + rigth, PositionY - up + i);
            }
        }
        else throw new ExecutionError("La posicion no puede quedar fuera de los limites del Canvas");
    }

    public static void Fill()
    {
        if(BrushColor == Color.Transparent) return;
        Color color = WorkZone[ PositionX, PositionY ];
        bool[,] mask = new bool[WorkZone.GetLength(0), WorkZone.GetLength(1)];
        Fill(PositionX, PositionY, mask, color);
    }

    private static void Fill(int posX, int posY, bool[,] mask, Color color)
    {
        mask[posX,posY] = true;
        WorkZone[posX,  posY] = BrushColor;
        int[][] directions = new int[][] { new int[] {1,0}, new int[] {0,1}, new int[] {-1,0}, new int[] {0,-1}};
        for (int i = 0; i < directions.Length; i++)
        {
            if(IsInRange(posX + directions[i][0], posY + directions[i][1])
            && WorkZone[posX + directions[i][0], posY + directions[i][1]] == color
            && mask[posX + directions[i][0], posY + directions[i][1]] == false) 
            Fill(posX + directions[i][0], posY + directions[i][1], mask, color);
        }
        
        
    }

    public static int GetActualX() => PositionX;

    public static int GetActualY() => PositionY;

    public static int GetCanvasSize() => WorkZone.GetLength(0);

    public static int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        if(x1 > x2 || y1 > y2) throw new ExecutionError("Los valores de la funcion GetColorCount no son validos");
        if(Enum.TryParse(Color.Black.GetType(), color,out object result))
        {
            if(IsInRange(x1,y1) && IsInRange(x2,y2))
            {
                int count = 0;
                for (int i = x1; i < x1 + x2; i++)
                {
                    for (int j = y1; j < y1 + y2; j++)
                    {
                        if(WorkZone[i,j] == (Color)result) count++;
                    }
                }
                return count;
            }
            else return 0 ;
        }
        else throw new ExecutionError($"{color} no es un color valido");
    }
    public static int IsBrushColor(string color)
    {
        if(Enum.TryParse(Color.Black.GetType(), color,out object result))
        {
            if((Color)result == BrushColor) return 1;
            else return 0;
        }
        else throw new ExecutionError($"{color} no es un color valido");
    }
    public static int IsBrushSize(int k)
    {
        if(k == BrushSize) return 1;
        else return 0;
    }
    public static int IsCanvasColor(string color, int vertical, int horizontal)
    {
        if(Enum.TryParse(Color.Black.GetType(), color,out object result))
        {
            if(IsInRange(PositionX + horizontal, PositionY + horizontal))
            {
                if (WorkZone[PositionX + horizontal, PositionY + vertical] == (Color)result) return 1;
            }
            return 0;
        }
        else throw new ExecutionError($"{color} no es un color valido");
    }

    

}