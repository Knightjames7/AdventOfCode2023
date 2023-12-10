public class Day6{
    public static void Day6Race(string[] inputs){
        string[] time = NormalizeWhiteSpace(inputs[0][10..]);
        string[] distance = NormalizeWhiteSpace(inputs[1][10..]);
        //int[,] array = new int[time.Length,2];
        ulong sum = 1;
        for(int i = 0; i < time.Length; i++){
            sum *= Day6Thread(Convert.ToInt64(time[i].Trim()),Convert.ToInt64(distance[i].Trim()));
        }
        Console.WriteLine(sum);
    }
    public static string[] NormalizeWhiteSpace(string input){
        if(input is null || input == ""){
            return Array.Empty<string>();
        }
        List<string> strings = new();
        string working = "";
        foreach(char c in input.ToCharArray()){
            if(char.IsWhiteSpace(c)){
                if(working.Length > 0){
                    strings.Add(working);
                    working = "";
                }
            }else{
                working += c;
            }
        }
        if(working.Length > 0){
            strings.Add(working);
        }
        return strings.ToArray();
    }
    public static ulong Day6Thread(long time, long distance){
        ulong totalSub = 0;
        Parallel.For<ulong>(0,time, () => 0,
            (j, loop, incr) => {
                long dis = j * (time - j);
                if(dis > distance){
                    incr++;
                }
                return incr;
            },
            incr => Interlocked.Add(ref totalSub, incr)
        );
        return totalSub;
    }
}