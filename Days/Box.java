package Days;

public class Box {// for Day 15
    private int value;
    private String id;
    public Box(String s){
        if(s.charAt(s.length()-1) == '-'){
            id = s.substring(0,s.length()-1);
            value = -1;
        }else{
            id = s.substring(0,s.length()-2);
            value = s.charAt(s.length()-1) - '0';
        }
    }
    public int getValue(){
        return value;
    }
    public String getID(){
        return id;
    }
    public boolean compareID(String other){
        return other.equals(id);
    }
    public boolean setBox(Box other){
        if(other.compareID(id)){
            value = other.getValue();
            return true;
        }
        return false;
    }
    public String toString(){
        return "[" + id + " " + value + "]";
    }
}
