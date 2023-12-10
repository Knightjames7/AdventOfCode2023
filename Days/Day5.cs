public class Day5{
    private uint[] seeds;
    private uint[,] seedToSoil;
    private uint[,] soilToFertilizer;
    private uint[,] fertilizerToWater;
    private uint[,] waterToLight;
    private uint[,] lightToTemperature;
    private uint[,] temperatureToHumidity;
    private uint[,] humidityToLocation;
    public Day5(string[] input){
        string[] temp = input[0][(input[0].IndexOf(':') + 2)..].Split();
        seeds = new uint[temp.Length];
        for(ushort i = 0; i < temp.Length; i++){
            seeds[i] = Convert.ToUInt32(temp[i]);
        }
        ushort line = 1;
        seedToSoil = WaitTill(input,"seed-to-soil", ref line);
        soilToFertilizer = WaitTill(input,"soil-to-fertilizer", ref line);
        fertilizerToWater = WaitTill(input,"fertilizer-to-water", ref line);
        waterToLight = WaitTill(input,"water-to-light", ref line);
        lightToTemperature = WaitTill(input,"light-to-temperature", ref line);
        temperatureToHumidity = WaitTill(input,"temperature-to-humidity", ref line);
        humidityToLocation = WaitTill(input,"humidity-to-location", ref line);

        Console.WriteLine("Building Done!");
        Start();
    }
    private static uint[,] WaitTill(string[] input, string contain, ref ushort line){
        while(!input[line].Contains(contain)){
            line++;
        }
        line++;
        ushort startLine = line;
        while(CanContinue(input,ref line)){
            line++;
        }
        uint[,] array = new uint[(line - startLine),3];
        //Console.WriteLine(array.GetLength(0) + ", " + array.GetLength(1));
        for(ushort i = 0; i < line - startLine; i++){
            string[] row = input[i + startLine].Trim().Split(' ');
            array[i,0] = Convert.ToUInt32(row[0]);
            array[i,1] = Convert.ToUInt32(row[1]);
            array[i,2] = Convert.ToUInt32(row[2]);
        }
        return array;
    }
    private static bool CanContinue(string[] input, ref ushort line){
        if(input.Length <= line){
            return false;
        }
        return input[line] != "";
    }
    public async void Start(){
        uint lowest = uint.MaxValue;
        var tasks = new List<Task<uint>>();
        foreach(uint seed in seeds){
            tasks.Add(Task.Run(() => Day5Threaded(seed)));
        }
        Thread.Sleep(1);
        foreach(var task in tasks){
            uint value = await task;
            if(value < lowest){
                lowest = value;
            }
            //Console.WriteLine("Value: " + value);
        }
        Console.WriteLine("Done with lowest value is: " + lowest);
    }
    /*public void StartParallel(){
        uint lowest = uint.MaxValue;
        Parallel.ForEach<uint,uint>(
            seeds,



        (finalResult) => );
    }*/
    public async void StartRange(){
        uint lowest = uint.MaxValue;
        for(uint i = 0; i < seeds.Length; i += 2){ 
            uint seedStart = seeds[i];
            uint seedEnd = seedStart + seeds[i+1];
            Console.WriteLine(seedStart + ", " + seedEnd);
            uint low = seedStart;
            uint upper = seedStart + 100;
            while(low < seedEnd){
                if(upper > seedEnd){
                    upper = seedEnd;
                }
                var tasks = new List<Task<uint>>();
                for(uint j = seedStart; j < seedEnd; j++){
                    uint seed = j;
                    tasks.Add(Task.Run(() => Day5Threaded(seed)));
                }
                Thread.Sleep(1);
                foreach(var task in tasks){
                    uint value = await task;
                    if(value < lowest){
                        lowest = value;
                    }
                    //Console.WriteLine("Value: " + value);
                }
                low += 100;
                upper += 100;
                Console.Write('*');
            }
            Console.WriteLine("Current lowest value is: " + lowest);
        }
        Console.WriteLine("Done with lowest value is: " + lowest);
    }
    private uint Day5Threaded(uint seed){
        //Console.Write(seed + ", ");
        seed = FilterData(seed, seedToSoil);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, soilToFertilizer);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, fertilizerToWater);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, waterToLight);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, lightToTemperature);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, temperatureToHumidity);
        //Console.Write(seed + ", ");
        seed = FilterData(seed, humidityToLocation);

        return seed;
    }
    private static uint FilterData(uint seed, uint[,] array){
        long offset = 0;
        for(ushort i = 0; i < array.GetLength(0);i++){
            uint destination = array[i, 0];
            uint soucre = array[i,1];
            uint range = array[i,2];
            if(seed >= soucre && seed <= soucre + range -1 && range != 0){
                offset += destination - soucre;
            }
        }
        seed = (uint)(seed + offset);
        return seed;
    }
}