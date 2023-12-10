public class Day7{
    public struct Set{
        public string cards;
        public int bids;
        public ushort kind;
    }
    public static void Start(string[] input){
        int size = input.Length;
        Set[] cards = new Set[size];
        Parallel.For(0,size,
            (i, loop) => {
                string charArr = input[i];
                string set = charArr[0..5];
                cards[i] = new Set{
                cards = set,
                bids = Convert.ToUInt16(charArr[6..]),
                kind = GetKind(MaxJoker(set)),
                };
            }
        );
        //parse good
        MergeSort(cards);

        long total = 0;
        Parallel.For<long>(0,size, () => 0,
            (i, loop, incr) => {
                incr += (i + 1) * cards[i].bids;
                return incr;
            },
            incr => Interlocked.Add(ref total, incr)
        );
        for(int i = 0; i < size; i++){
            Console.WriteLine(cards[i].cards + " => rank: " + (i + 1) + ", bid = " + cards[i].bids);
        }
        Console.WriteLine("Sum of Ranks: " + total);
    } 
    public static string MaxJoker(string s){
        short jokers = 0;
        foreach(char c in s){
            if(c == 'J'){
                jokers++;
            }
        }
        if(jokers == 0){
            return s;
        }
        char[] result = s.ToCharArray();
        Dictionary<char,ushort> pairs = new();
        for(int i = 0; i < s.Length; i++){
            if(!pairs.TryAdd(s[i], 1)){
                pairs[s[i]]++;
            }
        }
        char set = 'A';
        ushort largest = 0;
        foreach(var c in pairs){
            if(c.Key != 'J' && c.Value > largest){
                set = c.Key;
                largest = c.Value;
            }
        }
        for(int i = 0; i < s.Length; i++){
            if(s[i] == 'J'){
                result[i] = set;
            }
        }
        return new string(result);
    }
    public static ushort GetKind(string s){
        s = MaxJoker(s);
        Dictionary<char,ushort> pairs = new();
        for(int i = 0; i < s.Length; i++){
            if(!pairs.TryAdd(s[i], 1)){
                pairs[s[i]]++;
            }
        }
        ushort threeKind = 0;
        ushort twoPairs = 0;
        foreach(var (card,num) in pairs){
            switch(num){
                case 5:
                    return 6;
                case 4:
                    return 5;
                case 3:
                    threeKind++;
                    break;
                case 2:
                    twoPairs++;
                    break;
                default:
                    break;
            }
        }
        if(threeKind == 1){
            if(twoPairs == 1){
                return 4;
            }
            return 3;
        }else if(twoPairs == 2){
            return 2;
        }else if(twoPairs == 1){
            return 1;
        }
        return 0;
    }
    public static ushort CardValue(char c){
        if(c >= '2' && c <= '9'){
            return (ushort) (c - '0');
        }
        return c switch
        {
            'T' => 10,
            'J' => 1,//11 for normal mode
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => 0,
        };
    }
    private static bool SetLessOrEqual(Set i, Set j){ //if i <= j
        if(i.kind < j.kind){
            return true;
        }else if(i.kind > j.kind){
            return false;
        }
        for(int a = 0; a < i.cards.Length; a++){
            if(CardValue(i.cards[a]) > CardValue(j.cards[a])){
                return false;
            } else if(CardValue(i.cards[a]) < CardValue(j.cards[a])){
                return true;
            }
        }
        return true;
        
    }
    private static void MergeSort(Set[] a){
        uint n = (uint) a.Length;
        Set[] b = new Set[n];
        a.CopyTo(b,0);
        MergeTopDownSplit(b,0,n,a);
    }
    private static void MergeTopDownSplit(Set[] b, uint lower, uint upper, Set[] a){
        if(upper - lower < 2){
            return;
        }
        uint middle = (lower + upper)/ 2;
        MergeTopDownSplit(a,lower,middle,b);
        MergeTopDownSplit(a,middle,upper,b);
        MergeTopDown(b,lower,middle,upper,a);
    }
    private static void MergeTopDown(Set[] a, uint lower, uint middle, uint upper,Set[] b){
        uint i = lower, j = middle;
        for(uint k = lower; k < upper;k++){
            if(i < middle && (j >= upper || SetLessOrEqual(a[i], a[j]))){
                b[k] = a[i++];
                //i++;
            }else{
                b[k] = a[j++];
                //j++;
            }
        }
    }
}