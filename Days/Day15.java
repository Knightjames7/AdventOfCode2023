package Days;

public class Day15 {
    public static void start(String[] input){
        long total = 0;
        long total2 = 0;
        BoxList[] arrayLists = new BoxList[256];
        for(int i = 0; i < 256; i++){
            arrayLists[i] = new BoxList();
        }
        for (String line : input) {
            String[] temp = line.split(",");
            for (String hash : temp) {
                total += getHash(hash);
                Box box = new Box(hash);
                arrayLists[(int) getHash(box.getID())].addBox(box);
            }
        }
        System.out.println("The sum is: " + total);
        for(int i = 0; i < arrayLists.length; i++){
            if (arrayLists[i].getCount() > 0) {
                //System.out.println("Box " + i + ":" + arrayLists[i]);
                int slot = 1;
                for(Box box : arrayLists[i].getBoxes()){
                    long temp2 = (i+1) * slot * box.getValue();
                    total2 += temp2;
                    slot++;
                }
            }
        }
        System.out.println("The sum for part 2 is: " + total2);
    }
    public static long getHash(String string){
        long sum = 0;
        for(int i = 0; i < string.length(); i++){
            char c = string.charAt(i);
            sum += (long) c;
            sum = (sum * 17) % 256;
        }
        return sum;
    }
}
