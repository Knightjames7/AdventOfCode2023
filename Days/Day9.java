package Days;
public class Day9 {
    public static void start(String[] input){
        long sum = 0;
        for (String string : input) {
            String[] temp = string.split(" ");
            int[] nums = new int[temp.length];
            for(int i = 0; i< temp.length; i++){
                nums[i] = Integer.parseInt(temp[i]);
            }
            sum += (long) Day9Prior(nums);
        }
        System.out.println("The sum is: " + sum);
    }
        public static int Day9Prior(int[] values){
        int[][] working = new int[values.length + 1][];
        working[0] = new int[values.length + 1];
        for(int i = 0; i < values.length;i++){
            working[0][i+1] = values[i];
        }

        for(int i = 1; i < values.length + 1; i++){
            working[i] = new int[values.length - i + 1];
            for(int j = working[i].length -1; j > 0; j--){
                int temp = working[i-1][j+1] - working[i-1][j];
                working[i][j] = temp;
            }
        }

        for(int i = working.length - 2; i >= 0; i--){
            int temp = working[i][1] - working[i+1][0];
            working[i][0] = temp;
        }

        for (int[] is : working) {
            for (int n : is) {
                System.out.print(n + " ");
            }
            System.out.println();
        }
        return working[0][0];
    }
    public static int Day9Next(int[] values){
        int[][] working = new int[values.length + 1][];
        working[0] = new int[values.length + 1];
        for(int i = 0; i < values.length;i++){
            working[0][i] = values[i];
        }

        for(int i = 1; i < values.length + 1; i++){
            working[i] = new int[values.length - i + 1];
            for(int j = working[i].length -2; j >= 0; j--){
                int temp = working[i-1][j+1] - working[i-1][j];
                working[i][j] = temp;
            }
        }

        for(int i = working.length - 2; i >= 0; i--){
            int last = working[i].length -1;
            int temp = working[i][last - 1] + working[i+1][last - 1];
            working[i][last] = temp;
        }


        for (int[] is : working) {
            for (int n : is) {
                System.out.print(n + " ");
            }
            System.out.println();
        }
        return working[0][working[0].length-1];
    }
}
