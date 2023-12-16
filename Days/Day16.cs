using System.Diagnostics;

public class Day16{
    public struct Vector2Int{
        public readonly int x;
        public readonly int y;
        public Vector2Int(int x, int y){
            this.x = x;
            this.y = y;
        }
        public static Vector2Int operator + (Vector2Int one, Vector2Int two){
            return new(one.x + two.x, one.y + two.y);
        }
    }
    public enum Dir{
        up,
        right,
        down,
        left,
    }
    public static Vector2Int GetChangeInPos(Dir dir){
        return dir switch
        {
            Dir.up => new(0, -1),
            Dir.right => new(1, 0),
            Dir.down => new(0, 1),
            Dir.left => new(-1, 0),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public static void Start(string[] strings){
        char[,] map = new char[strings.Length,strings[0].Length];
        bool[,] energized = new bool[strings.Length,strings[0].Length];


        Parallel.For(0,strings.Length,
            (i, loop) =>{
                string temp = strings[i];
                for(int j = 0; j < temp.Length; j++){
                    map[i,j] = temp[j];
                }
            }
        );

        Beam(map, energized, Dir.right, new(0,0));
        int count = 0;
        foreach(bool energy in energized){
            if(energy){
                count++;
            }
        }
        Console.WriteLine("The Energized Count is: " + count);
        int largest = 0;
        for(int i = 0; i < strings.Length;i++){
            int temp = MaxBeam(map,Dir.right,new(0,i));
            if(temp > largest){
                largest = temp;
            }
        }
        for(int i = 0; i < strings.Length;i++){
            int temp = MaxBeam(map,Dir.left,new(strings[0].Length-1,i));
            if(temp > largest){
                largest = temp;
            }
        }
        for(int i = 0; i < strings[0].Length;i++){
            int temp = MaxBeam(map,Dir.down,new(i,0));
            if(temp > largest){
                largest = temp;
            }
        }
        for(int i = 0; i < strings[0].Length;i++){
            int temp = MaxBeam(map,Dir.up,new(i,strings.Length-1));
            if(temp > largest){
                largest = temp;
            }
        }

        Console.WriteLine("The Largest possible Energized Count is: " + largest);
    }
    public static int MaxBeam(char[,] map, Dir dir, Vector2Int pos){
        bool[,] energized = new bool[map.GetLength(0),map.GetLength(1)];
        Beam(map,energized,dir,pos);
        int count = 0;
        foreach(bool energy in energized){
            if(energy){
                count++;
            }
        }
        return count;
    }
    public static void Beam(char[,] map, bool[,] energized, Dir dir, Vector2Int pos){ //[y,x]
        if(pos.x < 0 || pos.x >= map.GetLength(1) || pos.y < 0 || pos.y >= map.GetLength(0) ){
            return;
        }
        if(map[pos.y,pos.x] == '.'){
            energized[pos.y,pos.x] = true;
            Beam(map,energized,dir,pos + GetChangeInPos(dir));
        }
        else if(map[pos.y,pos.x] == '/'){
            energized[pos.y,pos.x] = true;
            Dir next = dir switch
            {
                Dir.up => Dir.right,
                Dir.right => Dir.up,
                Dir.down => Dir.left,
                Dir.left => Dir.down,
                _ => throw new ArgumentOutOfRangeException(),
            };
            Beam(map,energized,next,pos + GetChangeInPos(next));
        }
        else if(map[pos.y,pos.x] == '\\'){
            energized[pos.y,pos.x] = true;
            Dir next = dir switch
            {
                Dir.up => Dir.left,
                Dir.right => Dir.down,
                Dir.down => Dir.right,
                Dir.left => Dir.up,
                _ => throw new ArgumentOutOfRangeException(),
            };
            Beam(map,energized,next,pos + GetChangeInPos(next));
        }
        else if(map[pos.y,pos.x] == '|'){
            if(energized[pos.y,pos.x]){
                return;
            }
            energized[pos.y,pos.x] = true;
            if(dir == Dir.right || dir == Dir.left){
                Beam(map,energized,Dir.up,pos + GetChangeInPos(Dir.up));
                Beam(map,energized,Dir.down,pos + GetChangeInPos(Dir.down));
            }else{
                Beam(map,energized,dir,pos + GetChangeInPos(dir));
            }
        }
        else if(map[pos.y,pos.x] == '-'){
            if(energized[pos.y,pos.x]){
                return;
            }
            energized[pos.y,pos.x] = true;
            if(dir == Dir.down || dir == Dir.up){
                Beam(map,energized,Dir.right,pos + GetChangeInPos(Dir.right));
                Beam(map,energized,Dir.left,pos + GetChangeInPos(Dir.left));
            }else{
                Beam(map,energized,dir,pos + GetChangeInPos(dir));
            }
        }
    }
}