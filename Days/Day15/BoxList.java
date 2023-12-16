package Days;

import java.util.ArrayList;

public class BoxList {
    private ArrayList<Box> listOfBoxes;
    private int count;
    public BoxList(){
        listOfBoxes = new ArrayList<Box>();
        count = 0;
    }
    public int getCount(){
        return count;
    }
    public ArrayList<Box> getBoxes(){
        return listOfBoxes;
    }
    public void addBox(Box next){
        if(next.getValue() == -1){
            for (Box box : listOfBoxes) {
                if(box.compareID(next.getID())){
                    listOfBoxes.remove(box);
                    count--;
                    return;
                }
            }
            return;
        }
        for (Box box : listOfBoxes) {
            if(box.setBox(next)){
                return;
            }
        }
        listOfBoxes.add(next);
        count++;
    
    }
    public String toString(){
        String temp = "";
        for (Box box : listOfBoxes) {
            temp += " " + box.toString();
        }
        return temp;
    }
}
