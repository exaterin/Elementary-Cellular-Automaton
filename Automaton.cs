using static System.Console;

class Cell{
    public int [] cells;
    public int [] automata;
    public int lengthOfCell;
    public Cell(int automata, int[] cells){

        this.cells = cells;
        this.lengthOfCell = cells.Length;
        int[] a = new int[8]; 
        for(int i = 0; i < 8; ++i)
            a[i] = 0;

        int n = automata; 
        int count = 0;   
        for(int i = 0; n > 0; i++){      
            a[i] = n%2;      
            n = n/2;
            ++ count;   
        }

        this.automata = a;
    }
}

class Neighbors{
    public int left; public int middle; public int right;
    public Neighbors(int left, int middle, int right){ this.left = left; this.middle = middle; this.right = right; 
    }   
}

class Automaton{
public static int [] createANewGeneration(Cell cell){
    int [] nextGeneration = new int[cell.lengthOfCell];
    int [,] puttern = {{0,0,0}, {0,0,1}, {0,1,0}, {0,1,1}, {1,0,0}, {1,0,1}, {1,1,0}, {1,1,1}};

    int convert(Neighbors trio){
        int i;
        int [] choosenPattern = new int[3];

        for(i = 0; i < puttern.Length; ++i)
            if (trio.left == puttern[i,0] && trio.middle == puttern[i,1] && trio.right == puttern[i,2]){
                for(int k = 0; k < 3; ++k)
                    choosenPattern[k] = puttern[i,k];
                return i;
            }
        return i;
    }

    Neighbors trio = new Neighbors(0,0,0);

    for(int i = 0; i < cell.lengthOfCell; ++i){
        if(i == 0)
            nextGeneration[i] = convert(new Neighbors(cell.cells[cell.lengthOfCell - 1], cell.cells[i], cell.cells[i+1]));
        
        if(i == cell.lengthOfCell - 1)
            nextGeneration[i] = convert(new Neighbors(cell.cells[i-1], cell.cells[i], cell.cells[0]));
        
        if (i != 0 && i !=cell.lengthOfCell - 1)
            nextGeneration[i] = convert(new Neighbors(cell.cells[i-1], cell.cells[i], cell.cells[i+1]));  
    }
    for(int i = 0; i < nextGeneration.Length; ++i)
        nextGeneration[i] = cell.automata[nextGeneration[i]];
             
    return nextGeneration;           
    }   


    static void Main(string[] args){

        int aut = int.Parse(ReadLine());
        int numberOfGens = int.Parse(ReadLine());
        string a = ReadLine();

        int [] result = new int[a.Length];
        int k = 0;
        foreach(char i in a){
            if (i == '-')
                result[k] = 0;
            if (i == 'x')
                result[k] = 1;
            k += 1;
        }

       Cell d = new Cell(aut, result);
       int [] b = new int [d.lengthOfCell];
       b = createANewGeneration(d);

        for(int i = 0; i < numberOfGens; ++i){
            result = createANewGeneration(d);

            if(i < 20){
                for(int j = 0; j < d.lengthOfCell; ++j){
                    if(createANewGeneration(d)[j] == 0)
                        Write("-");
                    if(createANewGeneration(d)[j] == 1)
                        Write("x");
            }
            WriteLine();
            }

            if(i == 20 && numberOfGens != 40)
                WriteLine("...");

            if(i >= numberOfGens - 20 && i > 19 ){
                for(int j = 0; j < d.lengthOfCell; ++j){
                    if(createANewGeneration(d)[j] == 0)
                        Write("-");
                    if(createANewGeneration(d)[j] == 1)
                        Write("x");
            }
            WriteLine();
            }
             d = new Cell(aut, result);
        }
    }
}
